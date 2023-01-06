using System.Collections.Generic;
using System.Linq;
using Global;
using Global.Constants;
using Godot;
using HippieFall.Biomes;
using HippieFall.Collectables;
using HippieFall.Game;
using HippieFall.Tunnels;

namespace HippieFall
{
	public class TunnelSpawner : Node
	{
		[Export] private NodePath _collectableControllerPath;
		[Export] private NodePath _obstaclesControllerPath;
		[Export] public Vector3 TunnelsOffset = new(0, -7.938f * 2, 0); //Change to tunnel Size
		[Export] private int _cashTunnelsSize = 15;
		[Export] private int _countTunnels = 10;
		[Export] public Vector3 xOffset = new(0, 0, 0); //Change to tunnel Size

		public bool IsNeedToSpawnObstacles { get; set; } = false;
		public bool IsNeedToSpawnCollectables { get; set; } = false;
		public int CountSpawnTunnels { get; set; } = 2;
		private CollectableSpawner CollectableSpawner => _collectableController.CollectableSpawner;
		private CollectableController _collectableController;
		private ObstaclesController _obstaclesController;
		private Biome _cyberBiome;
		private Biome _bikerBiome = null;//= new Biome(C_BiomeTypes.BIKER);
		private Biome _hippieBiome;
		private List<Biome> _biomes;
		private List<Spatial> _cashTunnels;
		private List<PackedScene> _obstacleOrder;
		private List<PackedScene> _obstacleTemplates;
		public bool IsNeedToSpawnTunnels = true;  
		
		public Biome CurrentBiome { get; private set; }

		public List<Tunnel> Tunnels { get; private set; }

		public override void _Ready()
		{
			Tunnels = new List<Tunnel>();
			_obstacleTemplates = new List<PackedScene>();
			_obstacleOrder = new List<PackedScene>();
			_cashTunnels = new List<Spatial>();
			_cyberBiome = new(C_BiomeTypes.CYBER);
			_hippieBiome = new(C_BiomeTypes.HIPPIE);
			_biomes = new List<Biome> { _cyberBiome, _bikerBiome, _hippieBiome };
			_obstaclesController = GetNode<ObstaclesController>(_obstaclesControllerPath);
			_collectableController = GetNode<CollectableController>(_collectableControllerPath);
			SetNewBiome();
			StartLevel();
			HippieFallUtilities.ConnectFeedbackAfterGameReadiness(this);
		}

		private void SpawnDoubleTunnel()
		{
			IsNeedToSpawnTunnels = false;
		}
		private void Init()
		{
			_collectableController.Init();
			_obstaclesController.Init();
		}
		private void SetNewBiome()
		{
			if (Utilities.GetRandomNumberInt(1,3) == 1)
			{
				SpawnDoubleTunnel();
			}
			else
			{
				ResetBiome();
				List<string> uniqueBiomes = new List<string>(C_BiomeTypes.BIOME_NAMES);
				if(CurrentBiome != null)
					uniqueBiomes.Remove(CurrentBiome.BiomeName);
				string biomeName = uniqueBiomes[Utilities.GetRandomNumberInt(0, uniqueBiomes.Count)];
			
				switch (biomeName)
				{
					case C_BiomeTypes.CYBER: CurrentBiome = _cyberBiome; break;
					case C_BiomeTypes.HIPPIE: CurrentBiome = _hippieBiome; break;
					default: CurrentBiome = _hippieBiome; break;
				}
				LoadObstacles();
				FillOrder();
			}
		}

		private void ResetBiome()
		{
			_obstacleTemplates.Clear();
			_obstacleOrder.Clear();
		}

		public void StartGame()
		{
			IsNeedToSpawnCollectables = true;
			IsNeedToSpawnObstacles = true;
			CountSpawnTunnels = _countTunnels;
			SetNewBiome();
			StartLevel();
		}
		
		private void StartLevel()
		{
			int count = 0;
			Vector3 position;
			if (Tunnels.Count != 0)
				position = Tunnels.Last().Translation + TunnelsOffset;
			else 
				position = TunnelsOffset + xOffset;
			while (count <= CountSpawnTunnels)
			{
				SpawnTunnel(position);
				position += TunnelsOffset;
				count += 1;
			}
		}
		
		private void FillOrder()
		{
			_obstacleOrder.Add(CurrentBiome.Gate);
			for (var i = 0; i < CountSpawnTunnels-1; i++)
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
			if(IsNeedToSpawnTunnels == false)
				return;
			Tunnel tunnel = (Tunnel)CurrentBiome.Tunnel.Duplicate();
			Tunnels.Add(tunnel);
			AddChild(tunnel);

			//settings tunnel
			tunnel.Translation = position;

			tunnel.Config = CurrentBiome.TunnelMesh.Instance<TunnelConfig>();
			
			if (IsNeedToSpawnObstacles)
			{
				Obstacle obstacle = (Obstacle)_obstacleOrder.First().Instance();
				_obstacleOrder.Remove(_obstacleOrder.First());
				tunnel.ObstacleMesh = obstacle;
				_obstaclesController.AddNode(obstacle, obstacle.Config);
			}
			else _obstacleOrder.Remove(_obstacleOrder.First());

			if (IsNeedToSpawnCollectables)
			{
				if (CollectableSpawner.GetSpawnCollectable() is Collectable collectable)
					tunnel.AddChild(collectable);
			}
			
			if (_obstacleOrder.Count == 0) 
				SetNewBiome();
		}

		public void RemoveTunnel(int countAtStart)
		{
			for (int i = 0; i < countAtStart; i++)
			{
				foreach(Obstacle obstacle in Tunnels[i].GetNode<Spatial>("Obstacles").GetChildren())
				{
					obstacle.Destroy();
				}
			}
		}
		public void CashRemoveTunnel(Tunnel tunnel = null)
		{
			_cashTunnels.Add(Tunnels.First());
			Tunnels.Remove(Tunnels.First());
			if (_cashTunnels.Count > _cashTunnelsSize)
			{
				foreach (var cash in _cashTunnels)
				{
					foreach (Obstacle obstacle in cash.GetNode<Spatial>("Obstacles").GetChildren()  )
					{
						obstacle.Destroy();
					}
					cash.QueueFree();
				}
				_cashTunnels.Clear();
			}
		}
	}
}
