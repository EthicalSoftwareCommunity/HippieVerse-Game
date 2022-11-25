using Global;
using Godot;

namespace HippieFall.Game
{
	public class PauseButton : Button
	{
		[Export] private NodePath _pauseOverlayPath;
		[Export] private bool _isPaused; //Set toggle on editor
		private Control _pauseOverlay;
		public override void _Ready()
		{
			_pauseOverlay = GetNode<Control>(_pauseOverlayPath);
			Connect("pressed", this, nameof(SetPause));
		}

		public void SetPause()
		{	
			_pauseOverlay.Visible = _isPaused;
			if (_isPaused)
				HippieFallUtilities.PauseGame();
			else
				HippieFallUtilities.ResumeGame();
		}
	}
}
