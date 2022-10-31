using Godot;

namespace HippieFall.Game
{
    public class PauseButton : Button
    {
        [Export] private NodePath _pauseOverlayPath;
        private Control _pauseOverlay;
        public bool IsPaused { get; set; }
        public override void _Ready()
        {
            _pauseOverlay = GetNode<Control>(_pauseOverlayPath);
            IsPaused = false;
            Connect("pressed", this, nameof(ChangePauseState));
        }

        public void ChangePauseState()
        {
            IsPaused = !IsPaused;
            SetPause(IsPaused);
        }

        private void SetPause(bool pause)
        {
            _pauseOverlay.Visible = pause;
            GetTree().Paused = pause;
        }
    }
}
