using Godot;

namespace HippieFall.Game
{
	public class Re_SpawnButton : Button
	{
		[Export] private NodePath _re_spawnOverlayPath;
		[Export] private bool _isPaused; //Set toggle on editor
		private Control _re_spawnOverlay;
		public override void _Ready()
		{
			_re_spawnOverlay = GetNode<Control>(_re_spawnOverlayPath);
			Connect("pressed", this, nameof(SetPause));
			
		}

		private void SetPause()
		{	
			_re_spawnOverlay.Visible = _isPaused;
			GetTree().Paused = _isPaused;
			
		}
	}
	
}

