using Godot;
using System;
using System.Globalization;
using Global.Data.Reward;

public class GameOverScreen : Control
{
	[Export] private NodePath _continueForFeeButtonPath;
	[Export] private NodePath _goHomeButtonPath;
	[Export] private NodePath _gameCoinsCountLabelPath;
	[Export] private NodePath _paymentGameCoinsCountPath;
	[Export] private NodePath _replayButtonPath;
	[Export] private NodePath _levelScoreLabelPath;
	[Export] private NodePath _highScoreTextLabelPath;
	private Button _continueForFeeButton;
	private Button _homeButton;
	private Button _replayButton;
	private Label _gameCoinsCountLabel;
	private Label _levelScoreLabel;
	private Label _paymentGameCoinsCount;
	private Label _highScoreTextLabel;
	public event Action OnGameContinue;
	public event Action OnGameOver;
	public event Action OnGameReplay;

	public override void _Ready()
	{
		_continueForFeeButton = GetNode<Button>(_continueForFeeButtonPath);
		_homeButton = GetNode<Button>(_goHomeButtonPath);
		_replayButton =  GetNode<Button>(_replayButtonPath);
		_gameCoinsCountLabel = GetNode<Label>(_gameCoinsCountLabelPath);
		_levelScoreLabel = GetNode<Label>(_levelScoreLabelPath);
		_paymentGameCoinsCount = GetNode<Label>(_paymentGameCoinsCountPath);
		_highScoreTextLabel = GetNode<Label>(_highScoreTextLabelPath);
		
		_continueForFeeButton.Connect("pressed", this, nameof(ContinueGame));
		_homeButton.Connect("pressed", this, nameof(GameOver));
		_replayButton.Connect("pressed", this, nameof(ReplayGame));
	}

	public void Show(RewardData rewardData, int paymentCount, float levelControllerDeep, bool isHighScore)
	{
		Visible = true;
		_continueForFeeButton.Visible = true;
		_gameCoinsCountLabel.Text = rewardData.Crystal.Count.ToString();
		_paymentGameCoinsCount.Text = paymentCount.ToString();
		_levelScoreLabel.Text = levelControllerDeep.ToString(CultureInfo.InvariantCulture);
		if (isHighScore)
		{
			_highScoreTextLabel.Visible = true;
		}
		if (rewardData.Crystal.Count < paymentCount)
		{
			_continueForFeeButton.Visible = false;
		}
	}

	private void ContinueGame() {OnGameContinue?.Invoke();}
	private void GameOver() {OnGameOver?.Invoke();}
	private void ReplayGame() {OnGameReplay?.Invoke();}
}
