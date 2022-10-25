using System.Collections.Generic;
using Global.Constants;

namespace HippieFall.Biomes
{
    class BiomeBikerConfig : BiomeConfig
    {
        public BiomeBikerConfig()
        {
            TunnelsObstacle = new List<string> { C_TunnelObstaclePath.CIRCLE_MOVING_BIKER };
            Gate = "res://HappyFall/Biome/Biker/Gate/Gate.tscn";
            Tunnel = "res://HappyFall/Biome/Biker/Tunnel/tunnel_biker.tscn";
        }
    }
}