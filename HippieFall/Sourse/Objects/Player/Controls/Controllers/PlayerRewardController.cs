using System;
using Global.Data.Reward;
using Godot;
using HippieFall.Collectables;

namespace HippieFall
{
	public class PlayerRewardController : Node
	{
		public event Action<RewardData> OnRewardsDataChanged;

		public RewardController RewardController { get; private set;}

		public override void _Ready()
		{
			RewardController = new RewardController();
		}

		private void OnAreaEntered(Area area)
		{
			if (area.GetOwnerOrNull<Collectable>() is Collectable reward)
			{
				if (reward is CollectableCoin)
					RewardController.RewardData.AddCoin(new RewardCoin(1));
				else if (reward is CollectableGemcoin)
					RewardController.RewardData.AddGemcoin(new RewardGemcoin(1));
				else if (reward is CollectableChest)
					//var chest = (reward as CollectableChest).CastToRewardChest();
					//RewardController.RewardData.add_chest(RewardChest.new(chest.chest_type, chest.chest_rarity))
					RewardController.RewardData.AddChest(new RewardChest());

				OnRewardsDataChanged?.Invoke(RewardController.RewardData);
			}
		}

		public void LoseGame()
		{
			RewardController.RewardSaveLoadSystem.SaveRewards(RewardController.RewardData);
		}
	}
}
