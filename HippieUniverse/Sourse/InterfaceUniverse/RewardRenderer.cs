
using System.Runtime.Serialization;
using Global.Data.Reward;
using Godot;

namespace HippieUniverse
{
	public class RewardRenderer : HBoxContainer
	{
		private Label _coinScore;
		private Label _gemcoinScore;

		public override void _Ready()
		{
			_coinScore = GetNode<Label>("Coin/TextureRect/Label");
			_gemcoinScore = GetNode<Label>("Gemcoin/TextureRect2/Label");
		}

		public void Render(RewardData data)
		{
			_coinScore.Text = data.Coin.Count.ToString();
			_gemcoinScore.Text = data.Gemcoin.Count.ToString();
		}
	}
}
