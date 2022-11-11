using Godot;
using System;
using Global.Data.Reward;

public class GameOverScreen : Control
{
	[Export] private NodePath _continueForFeeButtonPath;
	[Export] private NodePath _goHomeButtonPath;
	[Export] private NodePath _gameCoinsCountLabelPath;
	[Export] private NodePath _paymentGameCoinsCountPath;
	private Button _continueForFeeButton;
	private Button _homeButton;
	private Label _gameCoinsCountLabel;
	private Label _paymentGameCoinsCount;


	public event Action OnGameContinue;
	public event Action OnGameOver;

	public override void _Ready()
	{
		_continueForFeeButton = GetNode<Button>(_continueForFeeButtonPath);
		_homeButton = GetNode<Button>(_goHomeButtonPath);
		_gameCoinsCountLabel = GetNode<Label>(_gameCoinsCountLabelPath);
		_paymentGameCoinsCount = GetNode<Label>(_paymentGameCoinsCountPath);
		
		_continueForFeeButton.Connect("pressed", this, nameof(ContinueGame));
		_homeButton.Connect("pressed", this, nameof(GameOver));
	}

	public void Show(RewardData rewardData, int paymentCount)
	{
		Visible = true;
		_continueForFeeButton.Visible = true;
		_gameCoinsCountLabel.Text = rewardData.Gemcoin.Count.ToString();
		_paymentGameCoinsCount.Text = paymentCount.ToString();
		if (rewardData.Gemcoin.Count < paymentCount)
		{
			_continueForFeeButton.Visible = false;
			_paymentGameCoinsCount.AddColorOverride("bad", Colors.Red);
		}
	}
	private void ContinueGame() {OnGameContinue?.Invoke();}
	private void GameOver() {OnGameOver?.Invoke();}
}
