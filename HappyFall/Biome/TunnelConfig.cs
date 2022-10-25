using Godot;
using System;
using System.Collections.Generic;
using HippieFall.Tunnels;

namespace HippieFall.Biomes
{
    public class TunnelConfig:Node
    {
        [Export] public List<NodePath> SpawnPointsPaths;

        public List<Spatial> SpawnPoints;
        
        public override void _Ready()
        {
            SpawnPoints = new List<Spatial>();
            for (int i = 0; i < SpawnPointsPaths.Count; i++)
            {
                SpawnPoints.Add(GetNode<Spatial>(SpawnPointsPaths[i]));
            }
        }
    }

}
