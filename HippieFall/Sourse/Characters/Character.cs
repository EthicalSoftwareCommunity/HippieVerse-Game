using Godot;
using HippieFall.Game;

namespace HippieFall.Characters
{
    public class Character:Global.Data.CharacterSystem.Character
    {
        [Export] protected NodePath _characterModelPath;
        public CharacterModel CharacterModel; 
        public override void _Ready()
        {
            base._Ready();
            CharacterModel = GetNode<CharacterModel>(_characterModelPath);
            GetNode("/root").GetChild(0).Connect(nameof(GameController.GameIsReady), this, nameof(SetDependencies));
        }
        
        public void SetDependencies(GameController game)
        {
            AddCharacterInterfaceOnMainInterface(game);
        }
        private void AddCharacterInterfaceOnMainInterface(GameController game)
        {
            RemoveChild(_characterInterface);
            game.GameInterface.BottomUI.AddChild(_characterInterface);
        }
    }
}