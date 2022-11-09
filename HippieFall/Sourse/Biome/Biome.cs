using System.Collections.Generic;
using Global.Constants;
using Godot;
using HippieFall.Tunnels;

namespace HippieFall.Biomes
{
    class Biome : Node
    {
        public Tunnel Tunnel { get; }
        public List<PackedScene> SpawnObstacles { get; }
        public PackedScene Gate { get; private set; }
        public PackedScene TunnelMesh { get; private set; }
        public string BiomeName { get; set; }

        private BiomeConfig _config;
        private List<string> _spawnObstaclesConfig;

        public Biome(string biomeName)
        {
            SpawnObstacles = new List<PackedScene>();
            BiomeName = biomeName;
            InitConfig();
            InitSpawnTunnels();
            Tunnel = GD.Load<PackedScene>(C_ObjectPath.TUNNEL).Instance<Tunnel>();
        }

        public Biome()
        {
            
        }
        
        private void InitConfig()
        {
            _config = GetConfigByBiome();
            _spawnObstaclesConfig = _config.TunnelsObstacle;
            Gate = GD.Load<PackedScene>(_config.Gate);
            TunnelMesh = GD.Load<PackedScene>(_config.Tunnel);
        }

        private BiomeConfig GetConfigByBiome()
        {
            switch (BiomeName)
            {
                case C_BiomeTypes.CYBER:
                    return new BiomeCyberConfig();
                case C_BiomeTypes.BIKER:
                    return new BiomeBikerConfig();
            }

            return null;
        }

        private void InitSpawnTunnels()
        {
            foreach (string obstacle in _spawnObstaclesConfig)
                SpawnObstacles.Add(GD.Load<PackedScene>(obstacle));
        }
    }
}