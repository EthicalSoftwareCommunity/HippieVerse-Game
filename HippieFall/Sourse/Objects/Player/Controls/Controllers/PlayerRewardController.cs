using System;
using Global.Data.Reward;
using Godot;
using HippieFall.Collectables;

namespace HippieFall
{
	public class PlayerRewardController : Node
	{
		public event Action<RewardData> OnRewardsDataChanged;

		private RewardController _rewardController;

		public override void _Ready()
		{
			_rewardController = new RewardController();
		}

		private void OnAreaEntered(Area area)
		{
			if (area.GetOwnerOrNull<Collectable>() is Collectable reward)
			{
				if (reward is CollectableCoin)
					_rewardController.RewardData.AddCoin(new RewardCoin(1));
				else if (reward is CollectableGemcoin)
					_rewardController.RewardData.AddGemcoin(new RewardGemcoin(1));
				else if (reward is CollectableChest)
					//var chest = (reward as CollectableChest).CastToRewardChest();
					//_rewardController.RewardData.add_chest(RewardChest.new(chest.chest_type, chest.chest_rarity))
					_rewardController.RewardData.AddChest(new RewardChest());

				OnRewardsDataChanged?.Invoke(_rewardController.RewardData);
			}
		}

		public void LoseGame()
		{
			_rewardController.RewardSaveLoadSystem.SaveRewards(_rewardController.RewardData);
		}
	}
}
