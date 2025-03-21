﻿namespace GodotRuntimeInspector.Scripts
{
    public static class Flags
    {
        public static ImGuiNET.ImGuiWindowFlags WindowFlags()
        {
            ImGuiNET.ImGuiWindowFlags flags = new ImGuiNET.ImGuiWindowFlags();
            flags |= ImGuiNET.ImGuiWindowFlags.None;
            return flags;
        }
        public static ImGuiNET.ImGuiTableFlags TableFlags()
        {
            ImGuiNET.ImGuiTableFlags flags = new ImGuiNET.ImGuiTableFlags();
            flags |= ImGuiNET.ImGuiTableFlags.Resizable;
            flags |= ImGuiNET.ImGuiTableFlags.Borders;
            flags |= ImGuiNET.ImGuiTableFlags.Sortable;
            flags |= ImGuiNET.ImGuiTableFlags.SortMulti;
            flags |= ImGuiNET.ImGuiTableFlags.NoPadOuterX;
            flags |= ImGuiNET.ImGuiTableFlags.NoPadInnerX;
            flags |= ImGuiNET.ImGuiTableFlags.ScrollX;
            flags |= ImGuiNET.ImGuiTableFlags.ScrollY;
            return flags;
        }
        public static ImGuiNET.ImGuiTableColumnFlags TableColumnFlags()
        {
            ImGuiNET.ImGuiTableColumnFlags flags = new ImGuiNET.ImGuiTableColumnFlags();
            flags |= ImGuiNET.ImGuiTableColumnFlags.WidthStretch;
            return flags;
        }
        public static ImGuiNET.ImGuiTableRowFlags TableRowFlags()
        {
            ImGuiNET.ImGuiTableRowFlags flags = new ImGuiNET.ImGuiTableRowFlags();
            return flags;
        }
        public static ImGuiNET.ImGuiTableRowFlags NoneTableRowFlags()
        {
            ImGuiNET.ImGuiTableRowFlags flags = new ImGuiNET.ImGuiTableRowFlags();
            flags |= ImGuiNET.ImGuiTableRowFlags.None;
            return flags;
        }
        public static ImGuiNET.ImGuiInputTextFlags InputTextFlags()
        {
            ImGuiNET.ImGuiInputTextFlags flags = new ImGuiNET.ImGuiInputTextFlags();
            flags |= ImGuiNET.ImGuiInputTextFlags.AllowTabInput;
            flags |= ImGuiNET.ImGuiInputTextFlags.EnterReturnsTrue;
            return flags;
        }
        public static ImGuiNET.ImGuiTreeNodeFlags TreeNodeFlags()
        {
            ImGuiNET.ImGuiTreeNodeFlags flags = new ImGuiNET.ImGuiTreeNodeFlags();
            flags |= ImGuiNET.ImGuiTreeNodeFlags.SpanFullWidth;
            flags |= ImGuiNET.ImGuiTreeNodeFlags.OpenOnArrow;
            flags |= ImGuiNET.ImGuiTreeNodeFlags.OpenOnDoubleClick;
            return flags;
        }
    }
}
