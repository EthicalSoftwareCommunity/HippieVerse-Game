using Godot;

namespace HippieFall.Game
{

public class ResumeButton : Button
{
	[Export] private NodePath _pauseOverlayPath;
	private Control _pauseOverlay;
	public bool Pause { get; set; }
	
	public override void _Ready()
	{
		Pause = false;
		_pauseOverlay = GetNode<Control>(_pauseOverlayPath);
		Connect("pressed", this, nameof(ChangeResumeToTrue));

	}
	
	public void ChangeResumeToTrue()
	{
		ResumeGame(Pause);
	}

	private void ResumeGame(bool resume)
	{	
		_pauseOverlay.Visible = Pause;
		GetTree().Paused = Pause;
	}
}

}
