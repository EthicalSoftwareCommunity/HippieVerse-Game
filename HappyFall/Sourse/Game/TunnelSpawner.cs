using Godot;
using System.Linq;
using System.Collections.Generic;
using Global;
using Global.Constants;
using HippieFall.Biomes;
using HippieFall.Collectables;
using HippieFall.Tunnels;

namespace HippieFall
{
    class TunnelSpawner : Spatial
    {
        [Export] public NodePath _collectableControllerPath;
        [Export] public Vector3 TunnelsOffset = new Vector3(0, -7.938f * 2, 0); //Change to tunnel Size
        [Export] private int _cashTunnelsSize = 15;
        [Export] private int _countTunnels = 10;

        private CollectableSpawner CollectableSpawner => _collectableController.CollectableSpawner;
        private CollectableController _collectableController;
        private ObstaclesController _obstaclesController;
        private readonly Biome _cyberBiome = new Biome(C_BiomeTypes.CYBER);
        private readonly Biome _bikerBiome;//= new Biome(C_BiomeTypes.BIKER);
        private List<Biome> _biomes;
        private List<Spatial> _cashTunnels;
        private List<PackedScene> _obstacleOrder;
        private List<PackedScene> _obstacleTemplates;


        public Biome CurrentBiome { get; private set; }

        public List<Tunnel> Tunnels { get; private set; }

        public override void _Ready()
        {
            Tunnels = new List<Tunnel>();
            _obstacleTemplates = new List<PackedScene>();
            _obstacleOrder = new List<PackedScene>();
            _cashTunnels = new List<Spatial>();
            _biomes = new List<Biome> { _cyberBiome, _bikerBiome };
            _obstaclesController = GetNode<ObstaclesController>("ObstaclesController");
            _collectableController = GetNode<CollectableController>("CollectableController");
            SetNewBiome();
            StartLevel();
        }
        
        private void SetNewBiome()
        {
            List<string> uniqueBiomes = new List<string>(C_BiomeTypes.BIOME_NAMES);
            if(CurrentBiome != null)
                uniqueBiomes.Remove(CurrentBiome.BiomeName);
            string biomeName = uniqueBiomes[Utilities.GetRandomNumberInt(0, uniqueBiomes.Count)];
            
            switch (biomeName)
            {
                case C_BiomeTypes.BIKER: CurrentBiome = _bikerBiome; break;
                case C_BiomeTypes.CYBER: CurrentBiome = _cyberBiome; break;
            }

            CurrentBiome = _cyberBiome; //Debug
            LoadObstacles();
            FillOrder();
        }

        private void StartLevel()
        {
            int count = 0;
            Vector3 position = TunnelsOffset;
            while (count < _countTunnels)
            {
                SpawnTunnel(position);
                position += TunnelsOffset;
                count += 1;
            }
        }
        
        private void FillOrder()
        {
            _obstacleOrder.Add(CurrentBiome.Gate);
            for (var i = 0; i < _countTunnels; i++)
                _obstacleOrder.Add(_obstacleTemplates[
                    Utilities.GetRandomNumberInt(0, _obstacleTemplates.Count)
                ]);
        }

        private void LoadObstacles()
        {
            _obstacleTemplates.Clear();
            _obstacleTemplates.AddRange(CurrentBiome.SpawnObstacles);
        }

        public void SpawnTunnel(Vector3 position)
        {
            if (_obstacleOrder.Count == 0) SetNewBiome();

            Tunnel tunnel = (Tunnel)CurrentBiome.Tunnel.Duplicate();
            Obstacle obstacle = (Obstacle)_obstacleOrder.First().Instance();
            _obstacleOrder.Remove(_obstacleOrder.First());

            Tunnels.Add(tunnel);
            AddChild(tunnel);

            //settings tunnel
            tunnel.Translation = position;

            tunnel.Config = CurrentBiome.TunnelMesh.Instance<TunnelConfig>();
            
            if (tunnel.GetType() == typeof(Gate)) return;

            tunnel.ObstacleMesh = obstacle;
            _obstaclesController.AddObstacle(obstacle);
            
            if (CollectableSpawner.GetSpawnCollectable() is Collectable collectable)
                tunnel.AddChild(collectable);
        }

        public void RemoveTunnel(Tunnel getParentOrNull)
        {
            _cashTunnels.Add(Tunnels.First());
            Tunnels.Remove(Tunnels.First());
            if (_cashTunnels.Count > _cashTunnelsSize)
            {
                foreach (var cash in _cashTunnels)
                {
                    cash.QueueFree();
                    if (cash is Obstacle obstacle) _obstaclesController.RemoveObstacle(obstacle);
                }
                _cashTunnels.Clear();
            }
        }
    }
}