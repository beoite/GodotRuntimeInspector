using System.Runtime.CompilerServices;

namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public class FastNoiseImgui
    {
        private float cellularJitter = 0f;
        private float domainWarpAmplitude = 0f;
        private float domainWarpFractalGain = 0f;
        private float domainWarpFractalLacunarity = 0f;
        private int domainWarpFractalOctaves = 0;
        private float domainWarpFrequency = 0f;
        private float fractalGain = 0f;
        private float fractalLacunarity = 0f;
        private int fractalOctaves = 0;
        private float fractalPingPongStrength = 0f;
        private float fractalWeightedStrength = 0f;
        private float frequency = 0f;
        private float offsetX = 0f;
        private float offsetY = 0f;
        private float offsetZ = 0f;
        private System.Numerics.Vector3 offset = new System.Numerics.Vector3(0f, 0f, 0f);
        private int seed = 0;

        public void Update(ref Godot.FastNoiseLite fastNoise)
        {
            // CellularDistanceFunction
            if (ImGuiNET.ImGui.TreeNode(nameof(fastNoise.CellularDistanceFunction)))
            {
                if (ImGuiNET.ImGui.BeginListBox(fastNoise.CellularDistanceFunction.ToString()))
                {
                    foreach (Godot.FastNoiseLite.CellularDistanceFunctionEnum item in System.Enum.GetValues(typeof(Godot.FastNoiseLite.CellularDistanceFunctionEnum)))
                    {
                        bool selected = false;
                        if (fastNoise.CellularDistanceFunction == item)
                        {
                            selected = true;
                        }
                        if (ImGuiNET.ImGui.Selectable(item.ToString(), selected))
                        {
                            fastNoise.CellularDistanceFunction = item;
                        }
                    }
                    ImGuiNET.ImGui.EndListBox();
                }
                ImGuiNET.ImGui.TreePop();
            }

            // CellularJitter
            if (ImGuiNET.ImGui.SliderFloat(nameof(fastNoise.CellularJitter), ref cellularJitter, 0f, 1f))
            {
                fastNoise.CellularJitter = cellularJitter;
            }

            // CellularReturnType
            if (ImGuiNET.ImGui.TreeNode(nameof(fastNoise.CellularReturnType)))
            {
                if (ImGuiNET.ImGui.BeginListBox(fastNoise.CellularReturnType.ToString()))
                {
                    foreach (Godot.FastNoiseLite.CellularReturnTypeEnum item in System.Enum.GetValues(typeof(Godot.FastNoiseLite.CellularReturnTypeEnum)))
                    {
                        bool selected = false;
                        if (fastNoise.CellularReturnType == item)
                        {
                            selected = true;
                        }
                        if (ImGuiNET.ImGui.Selectable(item.ToString(), selected))
                        {
                            fastNoise.CellularReturnType = item;
                        }
                    }
                    ImGuiNET.ImGui.EndListBox();
                }
                ImGuiNET.ImGui.TreePop();
            }

            // DomainWarpAmplitude
            domainWarpAmplitude = fastNoise.DomainWarpAmplitude;
            if (ImGuiNET.ImGui.SliderFloat(nameof(fastNoise.DomainWarpAmplitude), ref domainWarpAmplitude, 0f, 360f))
            {
                fastNoise.DomainWarpAmplitude = domainWarpAmplitude;
            }

            // DomainWarpFractalGain 
            domainWarpFractalGain = fastNoise.DomainWarpFractalGain;
            if (ImGuiNET.ImGui.SliderFloat(nameof(fastNoise.DomainWarpFractalGain), ref domainWarpFractalGain, 0f, 1f))
            {
                fastNoise.DomainWarpFractalGain = domainWarpFractalGain;
            }

            // DomainWarpFractalLacunarity  
            domainWarpFractalLacunarity = fastNoise.DomainWarpFractalLacunarity;
            if (ImGuiNET.ImGui.SliderFloat(nameof(fastNoise.DomainWarpFractalLacunarity), ref domainWarpFractalLacunarity, 0f, 360f))
            {
                fastNoise.DomainWarpFractalLacunarity = domainWarpFractalLacunarity;
            }

            // DomainWarpFractalOctaves   
            domainWarpFractalOctaves = fastNoise.DomainWarpFractalOctaves;
            if (ImGuiNET.ImGui.InputInt(nameof(fastNoise.DomainWarpFractalOctaves), ref domainWarpFractalOctaves))
            {
                fastNoise.DomainWarpFractalOctaves = domainWarpFractalOctaves;
            }

            // DomainWarpFractalType    
            if (ImGuiNET.ImGui.TreeNode(nameof(fastNoise.DomainWarpFractalType)))
            {
                if (ImGuiNET.ImGui.BeginListBox(fastNoise.DomainWarpFractalType.ToString()))
                {
                    foreach (Godot.FastNoiseLite.DomainWarpFractalTypeEnum item in System.Enum.GetValues(typeof(Godot.FastNoiseLite.DomainWarpFractalTypeEnum)))
                    {
                        bool selected = false;
                        if (fastNoise.DomainWarpFractalType == item)
                        {
                            selected = true;
                        }
                        if (ImGuiNET.ImGui.Selectable(item.ToString(), selected))
                        {
                            fastNoise.DomainWarpFractalType = item;
                        }
                    }
                    ImGuiNET.ImGui.EndListBox();
                }
                ImGuiNET.ImGui.TreePop();
            }

            // DomainWarpFrequency 
            domainWarpFrequency = fastNoise.DomainWarpFrequency;
            if (ImGuiNET.ImGui.SliderFloat(nameof(fastNoise.DomainWarpFrequency), ref domainWarpFrequency, 0f, 1f))
            {
                fastNoise.DomainWarpFrequency = domainWarpFrequency;
            }

            // DomainWarpType 
            if (ImGuiNET.ImGui.TreeNode(nameof(fastNoise.DomainWarpType)))
            {
                if (ImGuiNET.ImGui.BeginListBox(fastNoise.DomainWarpType.ToString()))
                {
                    foreach (Godot.FastNoiseLite.DomainWarpTypeEnum item in System.Enum.GetValues(typeof(Godot.FastNoiseLite.DomainWarpTypeEnum)))
                    {
                        bool selected = false;
                        if (fastNoise.DomainWarpType == item)
                        {
                            selected = true;
                        }
                        if (ImGuiNET.ImGui.Selectable(item.ToString(), selected))
                        {
                            fastNoise.DomainWarpType = item;
                        }
                    }
                    ImGuiNET.ImGui.EndListBox();
                }
                ImGuiNET.ImGui.TreePop();
            }

            // FractalGain 
            fractalGain = fastNoise.FractalGain;
            if (ImGuiNET.ImGui.SliderFloat(nameof(fastNoise.FractalGain), ref fractalGain, 0f, 1f))
            {
                fastNoise.FractalGain = fractalGain;
            }

            // FractalLacunarity 
            fractalLacunarity = fastNoise.FractalLacunarity;
            if (ImGuiNET.ImGui.SliderFloat(nameof(fastNoise.FractalLacunarity), ref fractalLacunarity, 0f, 100f))
            {
                fastNoise.FractalLacunarity = fractalLacunarity;
            }

            // FractalOctaves 
            fractalOctaves = fastNoise.FractalOctaves;
            if (ImGuiNET.ImGui.InputInt(nameof(fastNoise.FractalOctaves), ref fractalOctaves))
            {
                fastNoise.FractalOctaves = fractalOctaves;
            }

            // FractalPingPongStrength 
            fractalPingPongStrength = fastNoise.FractalPingPongStrength;
            if (ImGuiNET.ImGui.SliderFloat(nameof(fastNoise.FractalPingPongStrength), ref fractalPingPongStrength, 0f, 360f))
            {
                fastNoise.FractalPingPongStrength = fractalPingPongStrength;
            }

            // FractalType 
            if (ImGuiNET.ImGui.TreeNode(nameof(fastNoise.FractalType)))
            {
                if (ImGuiNET.ImGui.BeginListBox(fastNoise.FractalType.ToString()))
                {
                    foreach (Godot.FastNoiseLite.FractalTypeEnum item in System.Enum.GetValues(typeof(Godot.FastNoiseLite.FractalTypeEnum)))
                    {
                        bool selected = false;
                        if (fastNoise.FractalType == item)
                        {
                            selected = true;
                        }
                        if (ImGuiNET.ImGui.Selectable(item.ToString(), selected))
                        {
                            fastNoise.FractalType = item;
                        }
                    }
                    ImGuiNET.ImGui.EndListBox();
                }
                ImGuiNET.ImGui.TreePop();
            }

            // FractalWeightedStrength 
            fractalWeightedStrength = fastNoise.FractalWeightedStrength;
            if (ImGuiNET.ImGui.SliderFloat(nameof(fastNoise.FractalWeightedStrength), ref fractalWeightedStrength, 0f, 1f))
            {
                fastNoise.FractalWeightedStrength = fractalWeightedStrength;
            }

            // Frequency 
            frequency = fastNoise.Frequency;
            if (ImGuiNET.ImGui.SliderFloat(nameof(fastNoise.Frequency), ref frequency, 0f, 1f))
            {
                fastNoise.Frequency = frequency;
            }

            // NoiseType 
            if (ImGuiNET.ImGui.TreeNode(nameof(fastNoise.NoiseType)))
            {
                if (ImGuiNET.ImGui.BeginListBox(fastNoise.NoiseType.ToString()))
                {
                    foreach (Godot.FastNoiseLite.NoiseTypeEnum item in System.Enum.GetValues(typeof(Godot.FastNoiseLite.NoiseTypeEnum)))
                    {
                        bool selected = false;
                        if (fastNoise.NoiseType == item)
                        {
                            selected = true;
                        }
                        if (ImGuiNET.ImGui.Selectable(item.ToString(), selected))
                        {
                            fastNoise.NoiseType = item;
                        }
                    }
                    ImGuiNET.ImGui.EndListBox();
                }
                ImGuiNET.ImGui.TreePop();
            }

            // Offset 
            offsetX = fastNoise.Offset.X;
            offsetY = fastNoise.Offset.Y;
            offsetZ = fastNoise.Offset.Z;
            float offsetLimit = 1000f;
            if (ImGuiNET.ImGui.SliderFloat(nameof(offsetX), ref offsetX, -offsetLimit, offsetLimit))
            {
                offset.X = offsetX;
            }
            if (ImGuiNET.ImGui.SliderFloat(nameof(offsetY), ref offsetY, -offsetLimit, offsetLimit))
            {
                offset.Y = offsetY;
            }
            if (ImGuiNET.ImGui.SliderFloat(nameof(offsetZ), ref offsetZ, -offsetLimit, offsetLimit))
            {
                offset.Z = offsetZ;
            }
            fastNoise.Offset = new Godot.Vector3(offset.X, offset.Y, offset.Z);

            // Seed 
            seed = fastNoise.Seed;
            if (ImGuiNET.ImGui.InputInt(nameof(fastNoise.Seed), ref seed))
            {
                fastNoise.Seed = seed;
            }
        }
    }
}
