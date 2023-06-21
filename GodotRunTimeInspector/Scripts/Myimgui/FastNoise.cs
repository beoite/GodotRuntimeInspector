namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public static class FastNoise
    {
        public static int Update(ref Godot.FastNoiseLite fastNoise)
        {
            int init = 0;

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
                            init++;
                        }
                    }
                    ImGuiNET.ImGui.EndListBox();
                }
                ImGuiNET.ImGui.TreePop();
            }

            // CellularJitter
            float cellularJitter = fastNoise.CellularJitter;
            if (ImGuiNET.ImGui.InputFloat(nameof(fastNoise.CellularJitter), ref cellularJitter))
            {
                fastNoise.CellularJitter = cellularJitter;
                init++;
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
                            init++;
                        }
                    }
                    ImGuiNET.ImGui.EndListBox();
                }
                ImGuiNET.ImGui.TreePop();
            }

            // DomainWarpAmplitude
            float domainWarpAmplitude = fastNoise.DomainWarpAmplitude;
            if (ImGuiNET.ImGui.InputFloat(nameof(fastNoise.DomainWarpAmplitude), ref domainWarpAmplitude))
            {
                fastNoise.DomainWarpAmplitude = domainWarpAmplitude;
                init++;
            }

            // DomainWarpFractalGain 
            float domainWarpFractalGain = fastNoise.DomainWarpFractalGain;
            if (ImGuiNET.ImGui.InputFloat(nameof(fastNoise.DomainWarpFractalGain), ref domainWarpFractalGain))
            {
                fastNoise.DomainWarpFractalGain = domainWarpFractalGain;
                init++;
            }

            // DomainWarpFractalLacunarity  
            float domainWarpFractalLacunarity = fastNoise.DomainWarpFractalLacunarity;
            if (ImGuiNET.ImGui.InputFloat(nameof(fastNoise.DomainWarpFractalLacunarity), ref domainWarpFractalLacunarity))
            {
                fastNoise.DomainWarpFractalLacunarity = domainWarpFractalLacunarity;
                init++;
            }

            // DomainWarpFractalOctaves   
            int domainWarpFractalOctaves = fastNoise.DomainWarpFractalOctaves;
            if (ImGuiNET.ImGui.InputInt(nameof(fastNoise.DomainWarpFractalOctaves), ref domainWarpFractalOctaves))
            {
                fastNoise.DomainWarpFractalOctaves = domainWarpFractalOctaves;
                init++;
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
                            init++;
                        }
                    }
                    ImGuiNET.ImGui.EndListBox();
                }
                ImGuiNET.ImGui.TreePop();
            }

            // DomainWarpFrequency 
            float domainWarpFrequency = fastNoise.DomainWarpFrequency;
            if (ImGuiNET.ImGui.InputFloat(nameof(fastNoise.DomainWarpFrequency), ref domainWarpFrequency))
            {
                fastNoise.DomainWarpFrequency = domainWarpFrequency;
                init++;
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
                            init++;
                        }
                    }
                    ImGuiNET.ImGui.EndListBox();
                }
                ImGuiNET.ImGui.TreePop();
            }

            // FractalGain 
            float fractalGain = fastNoise.FractalGain;
            if (ImGuiNET.ImGui.InputFloat(nameof(fastNoise.FractalGain), ref fractalGain))
            {
                fastNoise.FractalGain = fractalGain;
                init++;
            }

            // FractalLacunarity 
            float fractalLacunarity = fastNoise.FractalLacunarity;
            if (ImGuiNET.ImGui.InputFloat(nameof(fastNoise.FractalLacunarity), ref fractalLacunarity))
            {
                fastNoise.FractalLacunarity = fractalLacunarity;
                init++;
            }

            // FractalOctaves 
            int fractalOctaves = fastNoise.FractalOctaves;
            if (ImGuiNET.ImGui.InputInt(nameof(fastNoise.FractalOctaves), ref fractalOctaves))
            {
                fastNoise.FractalOctaves = fractalOctaves;
                init++;
            }

            // FractalPingPongStrength 
            float fractalPingPongStrength = fastNoise.FractalPingPongStrength;
            if (ImGuiNET.ImGui.InputFloat(nameof(fastNoise.FractalPingPongStrength), ref fractalPingPongStrength))
            {
                fastNoise.FractalPingPongStrength = fractalPingPongStrength;
                init++;
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
                            init++;
                        }
                    }
                    ImGuiNET.ImGui.EndListBox();
                }
                ImGuiNET.ImGui.TreePop();
            }

            // FractalWeightedStrength 
            float fractalWeightedStrength = fastNoise.FractalWeightedStrength;
            if (ImGuiNET.ImGui.InputFloat(nameof(fastNoise.FractalWeightedStrength), ref fractalWeightedStrength))
            {
                fastNoise.FractalWeightedStrength = fractalWeightedStrength;
                init++;
            }

            // Frequency 
            float frequency = fastNoise.Frequency;
            if (ImGuiNET.ImGui.InputFloat(nameof(fastNoise.Frequency), ref frequency))
            {
                fastNoise.Frequency = frequency;
                init++;
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
                            init++;
                        }
                    }
                    ImGuiNET.ImGui.EndListBox();
                }
                ImGuiNET.ImGui.TreePop();
            }

            // Offset 
            System.Numerics.Vector3 offset = new System.Numerics.Vector3(fastNoise.Offset.X, fastNoise.Offset.Y, fastNoise.Offset.Z);
            if (ImGuiNET.ImGui.InputFloat3(nameof(fastNoise.Offset), ref offset))
            {
                fastNoise.Offset = new Godot.Vector3(offset.X, offset.Y, offset.Z);
                init++;
            }

            // Seed 
            int seed = fastNoise.Seed;
            if (ImGuiNET.ImGui.InputInt(nameof(fastNoise.Seed), ref seed))
            {
                fastNoise.Seed = seed;
                init++;
            }

            return init;
        }
    }
}
