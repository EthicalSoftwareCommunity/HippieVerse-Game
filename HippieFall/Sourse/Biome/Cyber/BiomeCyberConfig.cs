using System.Collections.Generic;
using Global.Constants;

namespace HippieFall.Biomes
{
    class BiomeCyberConfig : BiomeConfig
    {
        public BiomeCyberConfig()
        {       
            TunnelsObstacle = new List<string>
            {
                C_TunnelObstaclePath.CYBER_CIRCLE_MOVING,
                C_TunnelObstaclePath.CYBER_PERFORATED_WALL,
                C_TunnelObstaclePath.CYBER_HALF_WALL,
                C_TunnelObstaclePath.CYBER_FAN,
                C_TunnelObstaclePath.CYBER_LASER_FASTENERS
            };  
            Gate = "res://HippieFall/Sourse/Biome/Cyber/Gate/Gate.tscn";
            Tunnel = "res://HippieFall/Sourse/Biome/Cyber/Tunnel/tunnel_cyber.tscn";
        }
    }
}