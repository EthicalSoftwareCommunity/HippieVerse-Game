using System.Collections.Generic;
using Global.Constants;

namespace HippieFall.Biomes
{
    class BiomeHippieConfig : BiomeConfig
    {
        public BiomeHippieConfig()
        {       
            TunnelsObstacle = new List<string>
            {
                C_TunnelObstaclePath.HIPPIE_SAW,
                C_TunnelObstaclePath.HIPPIE_HALF_WALL,
                C_TunnelObstaclePath.HIPPIE_PERFORATED_WALL,
                C_TunnelObstaclePath.HIPPIE_PERFORATED_WALL3TYPES,
            };  
            Gate = "res://HippieFall/Sourse/Biome/Hippie/Gate/Gate.tscn";
            Tunnel = "res://HippieFall/Sourse/Biome/Hippie/Tunnel/hippie_tunnel.tscn";
        }
    }
}