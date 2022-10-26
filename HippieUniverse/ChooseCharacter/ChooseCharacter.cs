using Global;
using Godot;

namespace HippieUniverse
{
	public class ChooseCharacter:StaticBody
	{
		private Node _characters;
		private Node _controller;

		public override void _Ready()
		{
			_characters = GetNode("Characters");
			_controller = GetNode("InteractionController");
			SetControllerOnCharacters();
		}

		private void SetControllerOnCharacters()
		{
			var childs = _characters.GetChildren();
			foreach (Node child in childs)
			{
				child.AddChild(_controller.Duplicate());
			}

			_controller.QueueFree();
		}
	}
}