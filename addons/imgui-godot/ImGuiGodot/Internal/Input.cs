using Godot;
using ImGuiNET;
using System;
using CursorShape = Godot.DisplayServer.CursorShape;

namespace ImGuiGodot.Internal;

internal static partial class Input
{
    internal static SubViewport CurrentSubViewport { get; set; }
    internal static System.Numerics.Vector2 CurrentSubViewportPos { get; set; }
    private static Vector2 _mouseWheel = Vector2.Zero;
    private static ImGuiMouseCursor _currentCursor = ImGuiMouseCursor.None;

    public static void Update(ImGuiIOPtr io)
    {
        if (io.ConfigFlags.HasFlag(ImGuiConfigFlags.ViewportsEnable))
        {
            var mousePos = DisplayServer.MouseGetPosition();
            io.AddMousePosEvent(mousePos.X, mousePos.Y);

            if (io.WantSetMousePos)
            {
                // TODO: get current focused window
            }
        }
        else
        {
            if (io.WantSetMousePos)
                Godot.Input.WarpMouse(new(io.MousePos.X, io.MousePos.Y));
        }

        // scrolling works better if we allow no more than one event per frame
        if (_mouseWheel != Vector2.Zero)
        {
            io.AddMouseWheelEvent(_mouseWheel.X, _mouseWheel.Y);
            _mouseWheel = Vector2.Zero;
        }

        if (io.WantCaptureMouse && !io.ConfigFlags.HasFlag(ImGuiConfigFlags.NoMouseCursorChange))
        {
            var newCursor = ImGui.GetMouseCursor();
            if (newCursor != _currentCursor)
            {
                DisplayServer.CursorSetShape(ConvertCursorShape(newCursor));
                _currentCursor = newCursor;
            }
        }
        else
        {
            _currentCursor = ImGuiMouseCursor.None;
        }

        CurrentSubViewport = null;
    }

    public static bool ProcessInput(InputEvent evt, Window window)
    {
        var io = ImGui.GetIO();
        bool viewportsEnable = io.ConfigFlags.HasFlag(ImGuiConfigFlags.ViewportsEnable);

        var windowPos = Vector2I.Zero;
        if (viewportsEnable)
            windowPos = window.Position;

        if (CurrentSubViewport != null)
        {
            var vpEvent = evt.Duplicate() as InputEvent;
            if (vpEvent is InputEventMouse mouseEvent)
            {
                mouseEvent.Position = new Vector2(windowPos.X + mouseEvent.GlobalPosition.X - CurrentSubViewportPos.X,
                    windowPos.Y + mouseEvent.GlobalPosition.Y - CurrentSubViewportPos.Y)
                    .Clamp(Vector2.Zero, CurrentSubViewport.Size);
            }
            CurrentSubViewport.PushInput(vpEvent, true);
            if (!CurrentSubViewport.IsInputHandled())
            {
                CurrentSubViewport.PushUnhandledInput(vpEvent, true);
            }
        }

        bool consumed = false;

        if (evt is InputEventMouseMotion mm)
        {
            if (!viewportsEnable)
                io.AddMousePosEvent(mm.GlobalPosition.X, mm.GlobalPosition.Y);
            consumed = io.WantCaptureMouse;
        }
        else if (evt is InputEventMouseButton mb)
        {
            switch (mb.ButtonIndex)
            {
                case MouseButton.Left:
                    io.AddMouseButtonEvent((int)ImGuiMouseButton.Left, mb.Pressed);
#if GODOT_WINDOWS
                    // if the left mouse button is released, the mouse almost certainly should not be captured
                    if (viewportsEnable && !mb.Pressed)
                        Viewports.MouseCaptureWorkaround();
#endif
                    break;
                case MouseButton.Right:
                    io.AddMouseButtonEvent((int)ImGuiMouseButton.Right, mb.Pressed);
                    break;
                case MouseButton.Middle:
                    io.AddMouseButtonEvent((int)ImGuiMouseButton.Middle, mb.Pressed);
                    break;
                case MouseButton.Xbutton1:
                    io.AddMouseButtonEvent((int)ImGuiMouseButton.Middle + 1, mb.Pressed);
                    break;
                case MouseButton.Xbutton2:
                    io.AddMouseButtonEvent((int)ImGuiMouseButton.Middle + 2, mb.Pressed);
                    break;
                case MouseButton.WheelUp:
                    _mouseWheel.Y = mb.Factor;
                    break;
                case MouseButton.WheelDown:
                    _mouseWheel.Y = -mb.Factor;
                    break;
                case MouseButton.WheelLeft:
                    _mouseWheel.X = -mb.Factor;
                    break;
                case MouseButton.WheelRight:
                    _mouseWheel.X = mb.Factor;
                    break;
            };
            consumed = io.WantCaptureMouse;
        }
        else if (evt is InputEventKey k)
        {
            UpdateKeyMods(io);
            ImGuiKey igk = ConvertKey(k.Keycode);
            if (igk != ImGuiKey.None)
            {
                io.AddKeyEvent(igk, k.Pressed);

                if (k.Pressed && k.Unicode != 0 && io.WantTextInput)
                {
                    io.AddInputCharacter((uint)k.Unicode);
                }
            }
            consumed = io.WantCaptureKeyboard || io.WantTextInput;
        }
        else if (evt is InputEventPanGesture pg)
        {
            _mouseWheel = new(-pg.Delta.X, -pg.Delta.Y);
            consumed = io.WantCaptureMouse;
        }
        else if (io.ConfigFlags.HasFlag(ImGuiConfigFlags.NavEnableGamepad))
        {
            if (evt is InputEventJoypadButton jb)
            {
                ImGuiKey igk = ConvertJoyButton(jb.ButtonIndex);
                if (igk != ImGuiKey.None)
                {
                    io.AddKeyEvent(igk, jb.Pressed);
                    consumed = true;
                }
            }
            else if (evt is InputEventJoypadMotion jm)
            {
                bool pressed = true;
                float v = jm.AxisValue;
                if (Math.Abs(v) < ImGuiGD.JoyAxisDeadZone)
                {
                    v = 0f;
                    pressed = false;
                }
                switch (jm.Axis)
                {
                    case JoyAxis.LeftX:
                        io.AddKeyAnalogEvent(ImGuiKey.GamepadLStickRight, pressed, v);
                        break;
                    case JoyAxis.LeftY:
                        io.AddKeyAnalogEvent(ImGuiKey.GamepadLStickDown, pressed, v);
                        break;
                    case JoyAxis.RightX:
                        io.AddKeyAnalogEvent(ImGuiKey.GamepadRStickRight, pressed, v);
                        break;
                    case JoyAxis.RightY:
                        io.AddKeyAnalogEvent(ImGuiKey.GamepadRStickDown, pressed, v);
                        break;
                    case JoyAxis.TriggerLeft:
                        io.AddKeyAnalogEvent(ImGuiKey.GamepadL2, pressed, v);
                        break;
                    case JoyAxis.TriggerRight:
                        io.AddKeyAnalogEvent(ImGuiKey.GamepadR2, pressed, v);
                        break;
                };
                consumed = true;
            }
        }

        return consumed;
    }

    public static void ProcessNotification(long what)
    {
        switch (what)
        {
            case MainLoop.NotificationApplicationFocusIn:
                ImGui.GetIO().AddFocusEvent(true);
                break;
            case MainLoop.NotificationApplicationFocusOut:
                ImGui.GetIO().AddFocusEvent(false);
                break;
        };
    }

    private static void UpdateKeyMods(ImGuiIOPtr io)
    {
        io.AddKeyEvent(ImGuiKey.ModCtrl, Godot.Input.IsKeyPressed(Key.Ctrl));
        io.AddKeyEvent(ImGuiKey.ModShift, Godot.Input.IsKeyPressed(Key.Shift));
        io.AddKeyEvent(ImGuiKey.ModAlt, Godot.Input.IsKeyPressed(Key.Alt));
        io.AddKeyEvent(ImGuiKey.ModSuper, Godot.Input.IsKeyPressed(Key.Meta));
    }

    private static CursorShape ConvertCursorShape(ImGuiMouseCursor cur) => cur switch
    {
        ImGuiMouseCursor.Arrow => CursorShape.Arrow,
        ImGuiMouseCursor.TextInput => CursorShape.Ibeam,
        ImGuiMouseCursor.ResizeAll => CursorShape.Move,
        ImGuiMouseCursor.ResizeNS => CursorShape.Vsize,
        ImGuiMouseCursor.ResizeEW => CursorShape.Hsize,
        ImGuiMouseCursor.ResizeNESW => CursorShape.Bdiagsize,
        ImGuiMouseCursor.ResizeNWSE => CursorShape.Fdiagsize,
        ImGuiMouseCursor.Hand => CursorShape.PointingHand,
        ImGuiMouseCursor.NotAllowed => CursorShape.Forbidden,
        _ => CursorShape.Arrow,
    };

    public static ImGuiKey ConvertJoyButton(JoyButton btn) => btn switch
    {
        JoyButton.Start => ImGuiKey.GamepadStart,
        JoyButton.Back => ImGuiKey.GamepadBack,
        JoyButton.Y => ImGuiKey.GamepadFaceUp,
        JoyButton.A => ImGuiGD.JoyButtonSwapAB ? ImGuiKey.GamepadFaceRight : ImGuiKey.GamepadFaceDown,
        JoyButton.X => ImGuiKey.GamepadFaceLeft,
        JoyButton.B => ImGuiGD.JoyButtonSwapAB ? ImGuiKey.GamepadFaceDown : ImGuiKey.GamepadFaceRight,
        JoyButton.DpadUp => ImGuiKey.GamepadDpadUp,
        JoyButton.DpadDown => ImGuiKey.GamepadDpadDown,
        JoyButton.DpadLeft => ImGuiKey.GamepadDpadLeft,
        JoyButton.DpadRight => ImGuiKey.GamepadDpadRight,
        JoyButton.LeftShoulder => ImGuiKey.GamepadL1,
        JoyButton.RightShoulder => ImGuiKey.GamepadR1,
        JoyButton.LeftStick => ImGuiKey.GamepadL3,
        JoyButton.RightStick => ImGuiKey.GamepadR3,
        _ => ImGuiKey.None
    };

    public static ImGuiKey ConvertKey(Key k) => k switch
    {
        Key.Tab => ImGuiKey.Tab,
        Key.Left => ImGuiKey.LeftArrow,
        Key.Right => ImGuiKey.RightArrow,
        Key.Up => ImGuiKey.UpArrow,
        Key.Down => ImGuiKey.DownArrow,
        Key.Pageup => ImGuiKey.PageUp,
        Key.Pagedown => ImGuiKey.PageDown,
        Key.Home => ImGuiKey.Home,
        Key.End => ImGuiKey.End,
        Key.Insert => ImGuiKey.Insert,
        Key.Delete => ImGuiKey.Delete,
        Key.Backspace => ImGuiKey.Backspace,
        Key.Space => ImGuiKey.Space,
        Key.Enter => ImGuiKey.Enter,
        Key.Escape => ImGuiKey.Escape,
        Key.Ctrl => ImGuiKey.LeftCtrl,
        Key.Shift => ImGuiKey.LeftShift,
        Key.Alt => ImGuiKey.LeftAlt,
        Key.Meta => ImGuiKey.LeftSuper,
        Key.Menu => ImGuiKey.Menu,
        Key.Key0 => ImGuiKey._0,
        Key.Key1 => ImGuiKey._1,
        Key.Key2 => ImGuiKey._2,
        Key.Key3 => ImGuiKey._3,
        Key.Key4 => ImGuiKey._4,
        Key.Key5 => ImGuiKey._5,
        Key.Key6 => ImGuiKey._6,
        Key.Key7 => ImGuiKey._7,
        Key.Key8 => ImGuiKey._8,
        Key.Key9 => ImGuiKey._9,
        Key.Apostrophe => ImGuiKey.Apostrophe,
        Key.Comma => ImGuiKey.Comma,
        Key.Minus => ImGuiKey.Minus,
        Key.Period => ImGuiKey.Period,
        Key.Slash => ImGuiKey.Slash,
        Key.Semicolon => ImGuiKey.Semicolon,
        Key.Equal => ImGuiKey.Equal,
        Key.Bracketleft => ImGuiKey.LeftBracket,
        Key.Backslash => ImGuiKey.Backslash,
        Key.Bracketright => ImGuiKey.RightBracket,
        Key.Quoteleft => ImGuiKey.GraveAccent,
        Key.Capslock => ImGuiKey.CapsLock,
        Key.Scrolllock => ImGuiKey.ScrollLock,
        Key.Numlock => ImGuiKey.NumLock,
        Key.Print => ImGuiKey.PrintScreen,
        Key.Pause => ImGuiKey.Pause,
        Key.Kp0 => ImGuiKey.Keypad0,
        Key.Kp1 => ImGuiKey.Keypad1,
        Key.Kp2 => ImGuiKey.Keypad2,
        Key.Kp3 => ImGuiKey.Keypad3,
        Key.Kp4 => ImGuiKey.Keypad4,
        Key.Kp5 => ImGuiKey.Keypad5,
        Key.Kp6 => ImGuiKey.Keypad6,
        Key.Kp7 => ImGuiKey.Keypad7,
        Key.Kp8 => ImGuiKey.Keypad8,
        Key.Kp9 => ImGuiKey.Keypad9,
        Key.KpPeriod => ImGuiKey.KeypadDecimal,
        Key.KpDivide => ImGuiKey.KeypadDivide,
        Key.KpMultiply => ImGuiKey.KeypadMultiply,
        Key.KpSubtract => ImGuiKey.KeypadSubtract,
        Key.KpAdd => ImGuiKey.KeypadAdd,
        Key.KpEnter => ImGuiKey.KeypadEnter,
        Key.A => ImGuiKey.A,
        Key.B => ImGuiKey.B,
        Key.C => ImGuiKey.C,
        Key.D => ImGuiKey.D,
        Key.E => ImGuiKey.E,
        Key.F => ImGuiKey.F,
        Key.G => ImGuiKey.G,
        Key.H => ImGuiKey.H,
        Key.I => ImGuiKey.I,
        Key.J => ImGuiKey.J,
        Key.K => ImGuiKey.K,
        Key.L => ImGuiKey.L,
        Key.M => ImGuiKey.M,
        Key.N => ImGuiKey.N,
        Key.O => ImGuiKey.O,
        Key.P => ImGuiKey.P,
        Key.Q => ImGuiKey.Q,
        Key.R => ImGuiKey.R,
        Key.S => ImGuiKey.S,
        Key.T => ImGuiKey.T,
        Key.U => ImGuiKey.U,
        Key.V => ImGuiKey.V,
        Key.W => ImGuiKey.W,
        Key.X => ImGuiKey.X,
        Key.Y => ImGuiKey.Y,
        Key.Z => ImGuiKey.Z,
        Key.F1 => ImGuiKey.F1,
        Key.F2 => ImGuiKey.F2,
        Key.F3 => ImGuiKey.F3,
        Key.F4 => ImGuiKey.F4,
        Key.F5 => ImGuiKey.F5,
        Key.F6 => ImGuiKey.F6,
        Key.F7 => ImGuiKey.F7,
        Key.F8 => ImGuiKey.F8,
        Key.F9 => ImGuiKey.F9,
        Key.F10 => ImGuiKey.F10,
        Key.F11 => ImGuiKey.F11,
        Key.F12 => ImGuiKey.F12,
        _ => ImGuiKey.None
    };
}
