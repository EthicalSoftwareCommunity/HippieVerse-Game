using System.Collections.Generic;
using Godot;
using HippieFall.Tunnels;

namespace HippieFall.Biomes
{
    abstract class BiomeConfig
    {
        public List<string> TunnelsObstacle { get; protected set; }
        public string Gate { get; protected set; }
        public string Tunnel { get; protected set; }
    }
}