# GodotRuntimeInspector

<p>
View field, property and method information for scripts attached to Nodes in the godot game engine.
</p>

<p>
    I made this to learn about Godot Engine [1] and Dear ImGui [2]. The Scene GodotRuntimeInspector/GodotRuntimeInspector.tscn has a demo, or you can get a windows build here, https://lackendara.itch.io/godotruntimeinspector
    The files here are a c# godot project, if you want to use it:
</p>

<ul>
    <li>
    Create a C# solution (if you haven't already) for your project from, Project > Tools > C# > Create C# solution.
    </li>
    <li>
    Open the solution and allow unsafe code in your .csproj:  
    ``` 
    < AllowUnsafeBlocks> true </AllowUnsafeBlocks > 
    ``` 
    , and install ImGui.NET [3] with NuGet.
    </li>
    <li>
    Install imgui-godot [4], (copy the addons folder to your project).
    </li>
    <li>
    Enable the imgui-godot plugin in Godot, Project > Project Settings > Plugins.
    </li>
    <li>
    Copy the GodotRuntimeInspector folder from this repository to your project
    </li>
    <li>
    Add GodotRuntimeInspector/Scripts/GodotRunTimeInspector.cs to a node in your Scene. 
</ul>

<p>[1] https://github.com/godotengine/godot </p>
<p>[2] https://github.com/ocornut/imgui </p>
<p>[3] https://github.com/ImGuiNET/ImGui.NET</p>
<p>[4] https://github.com/pkdawson/imgui-godot </p>

<img src="Untitled.png"
     alt="Screenshot"
     title="Screenshot"
/>