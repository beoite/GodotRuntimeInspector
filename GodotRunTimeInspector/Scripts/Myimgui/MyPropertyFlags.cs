using ImGuiNET;

namespace RuntimeInspector.Scripts.Myimgui
{
    public static class MyPropertyFlags
    {
        //ImGuiWindowFlags
        //None = 0
        //NoTitleBar = 1
        //    Disable title-bar
        //NoResize = 2
        //    Disable user resizing with the lower-right grip
        //NoMove = 4
        //    Disable user moving the window
        //NoScrollbar = 8
        //    Disable scrollbars(window can still scroll with mouse or programmatically)
        //NoScrollWithMouse = 16
        //    Disable user vertically scrolling with mouse wheel.On child window, mouse wheel will be forwarded to the parent unless NoScrollbar is also set.
        //NoCollapse = 32
        //    Disable user collapsing window by double-clicking on it.Also referred to as Window Menu Button (e.g.within a docking node).
        //AlwaysAutoResize = 64
        //    Resize every window to its content every frame
        //NoBackground = 128
        //    Disable drawing background color(WindowBg, etc.) and outside border.Similar as using SetNextWindowBgAlpha(0.0f).
        //NoSavedSettings = 256
        //    Never load/save settings in .ini file
        //NoMouseInputs = 512
        //    Disable catching mouse, hovering test with pass through.
        //MenuBar = 1024
        //    Has a menu-bar
        //HorizontalScrollbar = 2048
        //    Allow horizontal scrollbar to appear(off by default). You may use SetNextWindowContentSize(ImVec2(width,0.0f)); prior to calling Begin() to specify width.Read code in imgui_demo in the "Horizontal Scrolling" section.
        //NoFocusOnAppearing = 4096
        //    Disable taking focus when transitioning from hidden to visible state
        //NoBringToFrontOnFocus = 8192
        //    Disable bringing window to front when taking focus(e.g.clicking on it or programmatically giving it focus)
        //AlwaysVerticalScrollbar = 16384
        //    Always show vertical scrollbar(even if ContentSize.y<Size.y)
        //AlwaysHorizontalScrollbar = 32768
        //    Always show horizontal scrollbar(even if ContentSize.x<Size.x)
        //AlwaysUseWindowPadding = 65536
        //    Ensure child windows without border uses style.WindowPadding(ignored by default for non-bordered child windows, because more convenient)
        //NoNavInputs = 262144
        //    No gamepad/keyboard navigation within the window
        //NoNavFocus = 524288
        //    No focusing toward this window with gamepad/keyboard navigation (e.g.skipped by CTRL+TAB)
        //UnsavedDocument = 1048576
        //    Display a dot next to the title. When used in a tab/docking context, tab is selected when clicking the X + closure is not assumed (will wait for user to stop submitting the tab). Otherwise closure is assumed when pressing the X, so if you keep submitting the tab may reappear at end of tab bar.
        //NoNav = 786432
        //NoDecoration = 43
        //NoInputs = 786944
        //NavFlattened = 8388608
        //    [BETA] On child window: allow gamepad/keyboard navigation to cross over parent border to this child or between sibling child windows.
        //ChildWindow = 16777216
        //    Don't use! For internal use by BeginChild()
        //Tooltip = 33554432
        //    Don't use! For internal use by BeginTooltip()
        //Popup = 67108864
        //    Don't use! For internal use by BeginPopup()
        //Modal = 134217728
        //    Don't use! For internal use by BeginPopupModal()
        //ChildMenu = 268435456
        //    Don't use! For internal use by BeginMenu()

        public static ImGuiWindowFlags WindowFlags()
        {
            ImGuiWindowFlags windowFlags = new ImGuiWindowFlags();
            windowFlags |= ImGuiWindowFlags.NoSavedSettings;
            return windowFlags;
        }

        public static ImGuiWindowFlags HUDWindowFlags()
        {
            ImGuiWindowFlags windowFlags = new ImGuiWindowFlags();
            windowFlags |= ImGuiWindowFlags.NoTitleBar;
            windowFlags |= ImGuiWindowFlags.NoResize;
            windowFlags |= ImGuiWindowFlags.NoMove;
            windowFlags |= ImGuiWindowFlags.NoScrollbar;
            windowFlags |= ImGuiWindowFlags.NoScrollWithMouse;
            windowFlags |= ImGuiWindowFlags.NoCollapse;
            windowFlags |= ImGuiWindowFlags.AlwaysAutoResize;
            windowFlags |= ImGuiWindowFlags.NoBackground;
            windowFlags |= ImGuiWindowFlags.NoSavedSettings;
            windowFlags |= ImGuiWindowFlags.NoNavInputs;
            windowFlags |= ImGuiWindowFlags.NoNavFocus;
            windowFlags |= ImGuiWindowFlags.NoMouseInputs;
            windowFlags |= ImGuiWindowFlags.NoFocusOnAppearing;
            windowFlags |= ImGuiWindowFlags.NoBringToFrontOnFocus;
            return windowFlags;
        }

        public static ImGuiWindowFlags ContainerWindowFlags()
        {
            ImGuiWindowFlags windowFlags = new ImGuiWindowFlags();
            windowFlags |= ImGuiWindowFlags.NoSavedSettings;
            windowFlags |= ImGuiWindowFlags.NoFocusOnAppearing;
            windowFlags |= ImGuiWindowFlags.MenuBar;
            windowFlags |= ImGuiWindowFlags.NoCollapse;
            return windowFlags;
        }

        public static ImGuiWindowFlags TreeNodeWindowFlags()
        {
            ImGuiWindowFlags windowFlags = new ImGuiWindowFlags();
            windowFlags |= ImGuiWindowFlags.NoSavedSettings;
            windowFlags |= ImGuiWindowFlags.NoFocusOnAppearing;
            windowFlags |= ImGuiWindowFlags.AlwaysAutoResize;
            return windowFlags;
        }

        //ImGuiTableFlags
        //None = 0
        //Resizable = 1
        //    Enable resizing columns.
        //Reorderable = 2
        //    Enable reordering columns in header row(need calling TableSetupColumn() + TableHeadersRow() to display headers)
        //Hideable = 4
        //    Enable hiding/disabling columns in context menu.
        //Sortable = 8
        //    Enable sorting. Call TableGetSortSpecs() to obtain sort specs.Also see ImGuiTableFlags_SortMulti and ImGuiTableFlags_SortTristate.
        //NoSavedSettings = 16
        //    Disable persisting columns order, width and sort settings in the.ini file.
        //ContextMenuInBody = 32
        //    Right-click on columns body/contents will display table context menu. By default it is available in TableHeadersRow().
        //RowBg = 64
        //    Set each RowBg color with ImGuiCol_TableRowBg or ImGuiCol_TableRowBgAlt (equivalent of calling TableSetBgColor with ImGuiTableBgFlags_RowBg0 on each row manually)
        //BordersInnerH = 128
        //    Draw horizontal borders between rows.
        //BordersOuterH = 256
        //    Draw horizontal borders at the top and bottom.
        //BordersInnerV = 512
        //    Draw vertical borders between columns.
        //BordersOuterV = 1024
        //    Draw vertical borders on the left and right sides.
        //BordersH = 384
        //    Draw horizontal borders.
        //BordersV = 1536
        //    Draw vertical borders.
        //BordersInner = 640
        //    Draw inner borders.
        //BordersOuter = 1280
        //    Draw outer borders.
        //Borders = 1920
        //    Draw all borders.
        //NoBordersInBody = 2048
        //    [ALPHA] Disable vertical borders in columns Body (borders will always appear in Headers). -> May move to style
        //NoBordersInBodyUntilResize = 4096
        //    [ALPHA] Disable vertical borders in columns Body until hovered for resize(borders will always appear in Headers). -> May move to style
        //SizingFixedFit = 8192
        //    Columns default to _WidthFixed or _WidthAuto(if resizable or not resizable), matching contents width.
        //SizingFixedSame = 16384
        //    Columns default to _WidthFixed or _WidthAuto(if resizable or not resizable), matching the maximum contents width of all columns.Implicitly enable ImGuiTableFlags_NoKeepColumnsVisible.
        //SizingStretchProp = 24576
        //    Columns default to _WidthStretch with default weights proportional to each columns contents widths.
        //SizingStretchSame = 32768
        //    Columns default to _WidthStretch with default weights all equal, unless overridden by TableSetupColumn().
        //NoHostExtendX = 65536
        //    Make outer width auto-fit to columns, overriding outer_size.x value. Only available when ScrollX/ScrollY are disabled and Stretch columns are not used.
        //NoHostExtendY = 131072
        //    Make outer height stop exactly at outer_size.y (prevent auto-extending table past the limit). Only available when ScrollX/ScrollY are disabled.Data below the limit will be clipped and not visible.
        //NoKeepColumnsVisible = 262144
        //    Disable keeping column always minimally visible when ScrollX is off and table gets too small. Not recommended if columns are resizable.
        //PreciseWidths = 524288
        //    Disable distributing remainder width to stretched columns (width allocation on a 100-wide table with 3 columns: Without this flag: 33,33,34. With this flag: 33,33,33). With larger number of columns, resizing will appear to be less smooth.
        //NoClip = 1048576
        //    Disable clipping rectangle for every individual columns(reduce draw command count, items will be able to overflow into other columns). Generally incompatible with TableSetupScrollFreeze().
        //PadOuterX = 2097152
        //    Default if BordersOuterV is on.Enable outermost padding.Generally desirable if you have headers.
        //NoPadOuterX = 4194304
        //    Default if BordersOuterV is off.Disable outermost padding.
        //NoPadInnerX = 8388608
        //    Disable inner padding between columns(double inner padding if BordersOuterV is on, single inner padding if BordersOuterV is off).
        //ScrollX = 16777216
        //    Enable horizontal scrolling.Require 'outer_size' parameter of BeginTable() to specify the container size.Changes default sizing policy.Because this creates a child window, ScrollY is currently generally recommended when using ScrollX.
        //ScrollY = 33554432
        //    Enable vertical scrolling.Require 'outer_size' parameter of BeginTable() to specify the container size.
        //SortMulti = 67108864
        //    Hold shift when clicking headers to sort on multiple column.TableGetSortSpecs() may return specs where(SpecsCount > 1).
        //SortTristate = 134217728
        //    Allow no sorting, disable default sorting.TableGetSortSpecs() may return specs where(SpecsCount == 0).
        //SizingMask_ = 57344

        public static ImGuiTableFlags TableFlags()
        {
            ImGuiTableFlags tblFlags = new ImGuiTableFlags();
            tblFlags |= ImGuiTableFlags.Resizable;
            tblFlags |= ImGuiTableFlags.NoSavedSettings;
            tblFlags |= ImGuiTableFlags.Borders;
            tblFlags |= ImGuiTableFlags.RowBg;
            tblFlags |= ImGuiTableFlags.Sortable;
            tblFlags |= ImGuiTableFlags.SortMulti;
            tblFlags |= ImGuiTableFlags.NoPadOuterX;
            tblFlags |= ImGuiTableFlags.NoPadInnerX;
            tblFlags |= ImGuiTableFlags.SizingFixedFit;
            return tblFlags;
        }

        public static ImGuiTableFlags ContainerTableFlags()
        {
            ImGuiTableFlags tblFlags = new ImGuiTableFlags();
            tblFlags |= ImGuiTableFlags.Resizable;
            tblFlags |= ImGuiTableFlags.NoSavedSettings;
            tblFlags |= ImGuiTableFlags.Borders;
            tblFlags |= ImGuiTableFlags.RowBg;
            tblFlags |= ImGuiTableFlags.NoPadOuterX;
            tblFlags |= ImGuiTableFlags.NoPadInnerX;
            return tblFlags;
        }

        public static ImGuiTableFlags HUDTableFlags()
        {
            ImGuiTableFlags tblFlags = new ImGuiTableFlags();
            tblFlags |= ImGuiTableFlags.NoSavedSettings;
            tblFlags |= ImGuiTableFlags.Borders;
            tblFlags |= ImGuiTableFlags.RowBg;
            tblFlags |= ImGuiTableFlags.NoPadOuterX;
            tblFlags |= ImGuiTableFlags.NoPadInnerX;
            tblFlags |= ImGuiTableFlags.SizingFixedFit;
            return tblFlags;
        }

        //ImGuiTableColumnFlags
        //None = 0
        //Disabled = 1
        //    Overriding/master disable flag: hide column, won't show in context menu (unlike calling TableSetColumnEnabled() which manipulates the user accessible state)
        //DefaultHide = 2
        //    Default as a hidden/disabled column.
        //DefaultSort = 4
        //    Default as a sorting column.
        //WidthStretch = 8
        //    Column will stretch.Preferable with horizontal scrolling disabled (default if table sizing policy is _SizingStretchSame or _SizingStretchProp).
        //WidthFixed = 16
        //    Column will not stretch.Preferable with horizontal scrolling enabled (default if table sizing policy is _SizingFixedFit and table is resizable).
        //NoResize = 32
        //    Disable manual resizing.
        //NoReorder = 64
        //    Disable manual reordering this column, this will also prevent other columns from crossing over this column.
        //NoHide = 128
        //    Disable ability to hide/disable this column.
        //NoClip = 256
        //    Disable clipping for this column (all NoClip columns will render in a same draw command).
        //NoSort = 512
        //    Disable ability to sort on this field (even if ImGuiTableFlags_Sortable is set on the table).
        //NoSortAscending = 1024
        //    Disable ability to sort in the ascending direction.
        //NoSortDescending = 2048
        //    Disable ability to sort in the descending direction.
        //NoHeaderLabel = 4096
        //    TableHeadersRow() will not submit label for this column.Convenient for some small columns.Name will still appear in context menu.
        //NoHeaderWidth = 8192
        //    Disable header text width contribution to automatic column width.
        //PreferSortAscending = 16384
        //    Make the initial sort direction Ascending when first sorting on this column (default).
        //PreferSortDescending = 32768
        //    Make the initial sort direction Descending when first sorting on this column.
        //IndentEnable = 65536
        //    Use current Indent value when entering cell(default for column 0).
        //IndentDisable = 131072
        //    Ignore current Indent value when entering cell(default for columns > 0). Indentation changes within the cell will still be honored.
        //IsEnabled = 16777216
        //    Status: is enabled == not hidden by user/api(referred to as "Hide" in _DefaultHide and _NoHide) flags.
        //IsVisible = 33554432
        //    Status: is visible == is enabled AND not clipped by scrolling.
        //IsSorted = 67108864
        //    Status: is currently part of the sort specs
        //IsHovered = 134217728
        //    Status: is hovered by mouse
        //WidthMask_ = 24
        //IndentMask_ = 196608
        //StatusMask_ = 251658240
        //NoDirectResize_ = 1073741824
        //    [Internal] Disable user resizing this column directly (it may however we resized indirectly from its left edge)

        public static ImGuiTableColumnFlags TableColumnFlags()
        {
            ImGuiTableColumnFlags tableColumnFlags = new ImGuiTableColumnFlags();
            tableColumnFlags |= ImGuiTableColumnFlags.WidthStretch;
            return tableColumnFlags;
        }

        public static ImGuiTableColumnFlags ContainerTableColumnFlags()
        {
            ImGuiTableColumnFlags tableColumnFlags = new ImGuiTableColumnFlags();
            tableColumnFlags |= ImGuiTableColumnFlags.NoReorder;
            tableColumnFlags |= ImGuiTableColumnFlags.NoSort;
            tableColumnFlags |= ImGuiTableColumnFlags.IsVisible;
            tableColumnFlags |= ImGuiTableColumnFlags.WidthStretch;
            tableColumnFlags |= ImGuiTableColumnFlags.NoClip;
            return tableColumnFlags;
        }

        public static ImGuiTableColumnFlags HUDTableColumnFlags()
        {
            ImGuiTableColumnFlags tableColumnFlags = new ImGuiTableColumnFlags();
            tableColumnFlags |= ImGuiTableColumnFlags.WidthFixed;
            tableColumnFlags |= ImGuiTableColumnFlags.NoResize;
            tableColumnFlags |= ImGuiTableColumnFlags.NoHeaderLabel;
            return tableColumnFlags;
        }

        public static ImGuiTableRowFlags HeadersTableRowFlags()
        {
            ImGuiTableRowFlags tableRowFlags = new ImGuiTableRowFlags();
            tableRowFlags |= ImGuiTableRowFlags.Headers;
            return tableRowFlags;
        }

        public static ImGuiTableRowFlags NoneTableRowFlags()
        {
            ImGuiTableRowFlags tableRowFlags = new ImGuiTableRowFlags();
            tableRowFlags |= ImGuiTableRowFlags.None;
            return tableRowFlags;
        }

        public static ImGuiTabBarFlags ContainerTabBarFlags()
        {
            ImGuiTabBarFlags imGuiTabBarFlags = new ImGuiTabBarFlags();
            imGuiTabBarFlags |= ImGuiTabBarFlags.None;
            return imGuiTabBarFlags;
        }

        //ImGuiInputTextFlags
        //None = 0
        //CharsDecimal = 1
        //    Allow 0123456789.+ -*/
        //CharsHexadecimal = 2
        //    Allow 0123456789ABCDEFabcdef
        //CharsUppercase = 4
        //    Turn a..z into A..Z
        //CharsNoBlank = 8
        //    Filter out spaces, tabs
        //AutoSelectAll = 16
        //    Select entire text when first taking mouse focus
        //EnterReturnsTrue = 32
        //    Return 'true' when Enter is pressed(as opposed to every time the value was modified).
        //    Consider looking at the IsItemDeactivatedAfterEdit() function.
        //CallbackCompletion = 64
        //    Callback on pressing TAB(for completion handling)
        //                CallbackHistory = 128
        //    Callback on pressing Up/ Down arrows(for history handling)
        //                CallbackAlways = 256
        //    Callback on each iteration. User code may query cursor position, modify text buffer.
        //CallbackCharFilter = 512
        //    Callback on character inputs to replace or discard them.
        //    Modify 'EventChar' to replace or discard, or return 1 in callback to discard.
        //AllowTabInput = 1024
        //    Pressing TAB input a '\t' character into the text field
        //CtrlEnterForNewLine = 2048
        //    In multi-line mode, unfocus with Enter, add new line with Ctrl+Enter
        //    (default is opposite: unfocus with Ctrl + Enter, add line with Enter).
        //NoHorizontalScroll = 4096
        //    Disable following the cursor horizontally
        //AlwaysOverwrite = 8192
        //    Overwrite mode
        //ReadOnly = 16384
        //    Read - only mode
        //Password = 32768
        //    Password mode, display all characters as '*'
        //NoUndoRedo = 65536
        //    Disable undo/ redo.Note that input text owns the text data while active,
        //    if you want to provide your own undo/ redo stack you need e.g.to call ClearActiveID().
        //CharsScientific = 131072
        //    Allow 0123456789.+ -*/ eE(Scientific notation input)
        //CallbackResize = 262144
        //    Callback on buffer capacity changes request(beyond 'buf_size' parameter value),
        //    allowing the string to grow.Notify when the string wants to be resized
        //    (for string types which hold a cache of their Size).You will be provided a new BufSize in the callback and NEED to honor it.
        //    (see misc / cpp / imgui_stdlib.h for an example of using this)
        //CallbackEdit = 524288
        //    Callback on any edit(note that InputText() already returns true on edit, the callback
        //    is useful mainly to manipulate the underlying buffer while focus is active)
        //                EscapeClearsAll = 1048576
        //    Escape key clears content if not empty, and deactivate otherwise(contrast to default behavior of Escape to revert)

        public static ImGuiInputTextFlags ContainerInputTextFlags()
        {
            ImGuiInputTextFlags imGuiInputTextFlags = new ImGuiInputTextFlags();
            imGuiInputTextFlags |= ImGuiInputTextFlags.AllowTabInput;
            return imGuiInputTextFlags;
        }

        public static ImGuiTreeNodeFlags TreeNodeFlags()
        {
            ImGuiTreeNodeFlags imGuiTreeNodeFlags = new ImGuiTreeNodeFlags();
            imGuiTreeNodeFlags |= ImGuiTreeNodeFlags.SpanFullWidth;
            imGuiTreeNodeFlags |= ImGuiTreeNodeFlags.OpenOnArrow;
            imGuiTreeNodeFlags |= ImGuiTreeNodeFlags.OpenOnDoubleClick;
            return imGuiTreeNodeFlags;
        }
    }
}
