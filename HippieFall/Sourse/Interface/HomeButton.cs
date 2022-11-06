using Godot;

namespace HippieFall.Game
{
	public class HomeButton : Button
	{
		[Export] private PackedScene _scene;
		
		public override void _Ready()
		{
			Connect("pressed", this, nameof(ChangeSceneToMain));
		}

		public void ChangeSceneToMain()
		{	
			GetTree().ChangeScene(_scene.ResourcePath);
		}
	}
}
