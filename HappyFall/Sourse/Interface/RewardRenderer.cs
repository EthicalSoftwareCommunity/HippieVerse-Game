using Godot;
using Global.Data.Reward;
using HippieFall.Game;

namespace HippieFall
{
    public class RewardRenderer : HBoxContainer
    {
        private Label _coinScore;
        private Label _gemcoinScore;

        public override void _Ready()
        {
            _coinScore = GetNode<Label>("Coin/TextureRect/Label");
            _gemcoinScore = GetNode<Label>("Gemcoin/TextureRect2/Label");
            _coinScore.Text = "0";
            _gemcoinScore.Text = "0";
            GetNode("/root").GetChild(0).Connect(nameof(GameController.GameIsReady), 
                this, nameof(Init));
        }

        private void Init(GameController game)
        {
            game.Player.PlayerRewardController.OnRewardsDataChanged += Render;
        }

        private void Render(RewardData data)
        {
            _coinScore.Text = data.Coin.Count.ToString();
            _gemcoinScore.Text = data.Gemcoin.Count.ToString();
        }
    }
}