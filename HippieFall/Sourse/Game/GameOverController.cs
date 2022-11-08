using System.ComponentModel;
using Global;
using Global.Data.Reward;
using Godot;
using HippieFall;
using HippieFall.Game;

namespace HippieUniverse.HippieFall.Sourse.Interface
{
    public class GameOverController : Node
    {
        [Export] private NodePath _gameOverScreenPath;
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
        }

        private void ShowGameOverScreen()
        {
            _gameOverScreen.Show(_rewardData, paymentGemcoinCount);
            Utilities.SetNodePause(_player, needPauseChildren: true);
            Utilities.SetNodePause(_levelController);
        }

        private void ContinueGame()
        {
            SpendGemCoins();
            _gameOverScreen.Hide();
            Utilities.SetNodePause(_player, isNodeNeedToPause:false, needPauseChildren: true);
            Utilities.SetNodePause(_levelController, isNodeNeedToPause:false);
            paymentGemcoinCount *= 2;
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
    }
}
