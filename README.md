# GodotRuntimeInspector

<p>
View field, property and method information for scripts attached to Nodes in the Godot game engine.
</p>

<p>
    The files here are a c# godot project, if you want to use it:
</p>

<ul>
    <li>
    Create a C# solution (if you haven't already) for your project from, Project > Tools > C# > Create C# solution.
    </li>
    <li>
    Open the solution and allow unsafe code in your .csproj:  
    &lt;AllowUnsafeBlocks&gt;true&lt;/AllowUnsafeBlocks&gt;
    , and install ImGui.NET [3] with NuGet.
    </li>
    <li>
    Install imgui-godot [4], (copy the addons folder to your project).
    </li>
    <li>
    Enable the imgui-godot plugin in Godot, Project > Project Settings > Plugins.
    </li>
    <li>
    Copy addons/GodotRuntimeInspector from this repository to your project
    </li>
    <li>
    Drag the RuntimeInspector.tscn Node to your scene and run.
    </li>
</ul>

<p>[1] https://github.com/godotengine/godot </p>
<p>[2] https://github.com/ocornut/imgui </p>
<p>[3] https://github.com/ImGuiNET/ImGui.NET</p>
<p>[4] https://github.com/pkdawson/imgui-godot </p>

<img src="Untitled.png"
     alt="Screenshot"
     title="Screenshot"
/>