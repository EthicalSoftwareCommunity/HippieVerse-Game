using System.ComponentModel;
using Global;
using Global.Data.Reward;
using Godot;
using HippieFall;
using HippieFall.Game;
using Global.Constants;
using Global.Data.GameSystem;
using Newtonsoft.Json;


namespace HippieFall.Game.Interface
{
	public class GameOverController : Node
	{
		[Export] private NodePath _gameOverScreenPath;
		[Export] public C_ScenesPath.Scenes _restartGamePath;

		private GameOverScreen _gameOverScreen;
		private LevelController _levelController;
		private Player _player;

		private RewardData _rewardData = new RewardController().RewardSaveLoadSystem.LoadRewards();
		private int paymentGemcoinCount = 1;

		public override void _Ready()
		{
			_gameOverScreen = GetNode<GameOverScreen>(_gameOverScreenPath);
			GetNode("/root").GetChild(0).Connect(nameof(GameController.GameIsReady), 
				this, nameof(Init));
		}

		private void Init(GameController game)
		{
			game.Player.OnPlayerEndedGame += ShowGameOverScreen;
			_levelController = game.Level;
			_player = game.Player;
			
			_gameOverScreen.OnGameContinue += ContinueGame;
			_gameOverScreen.OnGameOver += GameOver;
			_gameOverScreen.OnGameReplay += ReplayGame;
		}

		private void ShowGameOverScreen()
		{
			HippieFallUtilities.PauseGame();
			_gameOverScreen.Show(_rewardData, paymentGemcoinCount, _levelController.Deep, CheckForHighScore());
		}

		private void ContinueGame()
		{
			SpendGemCoins();
			HippieFallUtilities.ResumeGame();
			HippieFallUtilities.Game.Level.Spawner.RemoveTunnel(2);
			_gameOverScreen.Hide();
			paymentGemcoinCount *= 2;
		}

		private bool CheckForHighScore()
		{
			File file = new File();
			Directory directory = new Directory();
			if (directory.DirExists(C_SaveFolderFile.FOLDER_PLAYER_HIGH_SCORE) == false)
				directory.MakeDir(C_SaveFolderFile.FOLDER_PLAYER_HIGH_SCORE);
			if(file.FileExists(C_SaveFolderFile.FILE_PLAYER_HIGH_SCORE) == false)
			{
				file.Open(C_SaveFolderFile.FILE_PLAYER_HIGH_SCORE, File.ModeFlags.Write);
				file.Close();
			}
			file.Open(C_SaveFolderFile.FILE_PLAYER_HIGH_SCORE, File.ModeFlags.ReadWrite);
			GameData data = JsonConvert.DeserializeObject<GameData>(file.GetAsText()) ?? new GameData();
			if (data.HighScore < _levelController.Deep)
			{
				data.HighScore = _levelController.Deep;
				file.StoreString(JsonConvert.SerializeObject(data));
				file.Close();
				return true;
			}
			file.Close();
			return false;
		}
		private void GameOver()
		{
			new RewardController().RewardSaveLoadSystem.SaveRewards(_player.PlayerRewardController.RewardController.RewardData);
		}
		private void SpendGemCoins()
		{
			_rewardData.Gemcoin.Count -= paymentGemcoinCount;
			new RewardController().RewardSaveLoadSystem.SaveRewards(_rewardData, true);
		}
		private void ReplayGame()
		{
			new RewardController().RewardSaveLoadSystem.SaveRewards(_player.PlayerRewardController.RewardController.RewardData);
			HippieFallUtilities.ReplayGame(this,C_ScenesPath.GetScenePathByEnum(_restartGamePath));
		}
	}
}
