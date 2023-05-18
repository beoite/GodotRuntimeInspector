# GodotRuntimeInspector

<p>
A runtime inspector for the godot game engine.
</p>

<p>
I made this to learn about Godot Engine [1] and Dear ImGui [2].

This is currently a c# godot project, but I'll try to get it working as an addon so its eaiser to import, but for now, if you want to use it:

-) Create a C# solution (if you haven't already) from Project > Tools > C# > Create C# solution.

-) Open the solution and allow unsafe code, and install ImGui.NET [3] with NuGet.

-) Install imgui-godot [4], (copy the addons folder to your project).

-) Enable the imgui-godot plugin in Godot, Project > Project Settings > Plugins.

-) Copy the GodotRunTimeInspector folder to your project

-) Add GodotRunTimeInspector/Scripts/GodotRunTimeInspector.cs to a node in your Scene.

The Scene GodotRunTimeInspector/GodotRuntimeInspector.tscn has a demo.

</p>

<p>
[1] https://github.com/godotengine/godot
[2] https://github.com/ocornut/imgui 
[3] https://github.com/ImGuiNET/ImGui.NET
[4] https://github.com/pkdawson/imgui-godot
</p>

<img src="https://i.imgur.com/XXBmZXB.png"
     alt="Screenshot"
     title="Screenshot"
/>