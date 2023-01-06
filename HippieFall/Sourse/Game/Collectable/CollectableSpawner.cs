using System;
using System.Collections.Generic;
using Global;
using Global.Constants;
using Global.Data;
using Global.Data.EffectSystem;
using Godot;
using HippieFall.Collectables;

namespace HippieFall
{
	public class CollectableSpawner : Node, IEffectable
	{
		public event Action<Collectable, Config> OnCollectableCreated; 
		public List<Collectable> CollectableItems => _collectableItems;
		
		private List<Collectable> _collectableItems;
		private List<float> _weights;
		private float _voidCollectableWeight = 200f;
		private float _summaryWeight;

		public void Init()
		{
			_collectableItems = new List<Collectable>
			{
				GD.Load<PackedScene>(C_CollectablePath.COIN).Instance<Collectable>(),
				GD.Load<PackedScene>(C_CollectablePath.CRYSTAL).Instance<Collectable>(),
				GD.Load<PackedScene>(C_CollectablePath.CRYSTAL_DEPOSIT).Instance<Collectable>(),
				GD.Load<PackedScene>(C_CollectablePath.CHEST).Instance<Collectable>(),
				GD.Load<PackedScene>(C_CollectablePath.SLOW_OBSTACLES).Instance<Collectable>(),
				GD.Load<PackedScene>(C_CollectablePath.DOUBLE_SCORE).Instance<Collectable>(),
				GD.Load<PackedScene>(C_CollectablePath.MAGNET).Instance<Collectable>(),
				GD.Load<PackedScene>(C_CollectablePath.SHIELD).Instance<Collectable>(),
			};
			ReloadSetting();
		}
		public void ReloadSetting()
		{
			_weights = new List<float>();
			_weights = GetWeights();
			_summaryWeight = GetSummaryWeight();
			_weights.Add(_voidCollectableWeight);
			_summaryWeight += _voidCollectableWeight;
		}
		
		private List<float> GetWeights()
		{
			List<float> weights = new List<float>();
			foreach (var collectable in _collectableItems)
			{
				float weight = collectable.Config.SpawnWeight;
				weights.Add(weight);
			}
			return weights;
		}

		private float GetSummaryWeight()
		{
			float sum = 0;
			foreach (var weight in _weights) sum += weight;
			return sum;
		}

		public Collectable GetSpawnCollectable()
		{
			int spawnIndex = GetSpawnIndex();
			if (spawnIndex == _weights.Count - 1) return null;

			Collectable collectable = (Collectable)_collectableItems[spawnIndex].Duplicate();

			float offsetX =
				Utilities.GetRandomNumberFloat(-collectable.Config.SpawnOffsetX, collectable.Config.SpawnOffsetX);
			float offsetZ =
				Utilities.GetRandomNumberFloat(-collectable.Config.SpawnOffsetZ, collectable.Config.SpawnOffsetZ);
			

			Vector3 position = new Vector3(offsetX, 1, offsetZ);

			collectable.RotateX(Mathf.Deg2Rad(-90));
			collectable.ScaleObjectLocal(new Vector3(0.5f, 0.5f, 0.5f));
			collectable.Translation += position;
			OnCollectableCreated?.Invoke(collectable, collectable.Config);
			return collectable;
		}

		private int GetSpawnIndex()
		{
			int randomWeight = Utilities.GetRandomNumberInt(0, (int)_summaryWeight);
			float currentWeight = 0;
			float previousWeight = 0;
			for (var i = 0; i < _weights.Count; i++)
			{
				currentWeight += _weights[i];
				if (randomWeight > previousWeight && randomWeight <= currentWeight) return i;
				previousWeight = currentWeight;
			}
			return 0;
		}

		public void ChangeConfigData(Config config)
		{
			foreach (var collectable in _collectableItems)
				collectable.ChangeConfigData(config);
		}
	}
}
