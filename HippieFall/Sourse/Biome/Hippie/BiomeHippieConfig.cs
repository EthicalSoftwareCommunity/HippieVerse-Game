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
                C_TunnelObstaclePath.CYBER_CIRCLE_MOVING,
                C_TunnelObstaclePath.CYBER_PERFORATED_WALL,
                C_TunnelObstaclePath.CYBER_HALF_WALL,
                C_TunnelObstaclePath.CYBER_FAN,
                C_TunnelObstaclePath.CYBER_LASER_FASTENERS,
                C_TunnelObstaclePath.CYBER_HIDDEN_TRAP
            };  
            Gate = "res://HippieFall/Sourse/Biome/Hippie/Gate/Gate.tscn";
            Tunnel = "res://HippieFall/Sourse/Biome/Hippie/Tunnel/hippie_tunnel.tscn";
        }
    }
}