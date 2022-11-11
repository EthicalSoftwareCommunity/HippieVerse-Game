namespace Global.Data.Reward
{
	public class RewardController
	{
		public RewardController()
		{
			RewardData = new RewardData();
			RewardSaveLoadSystem = new RewardSaveLoadSystem();
		}

		public RewardData RewardData { get; }

		public RewardSaveLoadSystem RewardSaveLoadSystem { get; }
	}
}
