using Godot;

namespace HippieFall.Characters
{
    public class CharacterModel : Spatial
    {
        [Export] public NodePath MagnetPath;
        private Spatial _magnet;
        
        public Spatial Magnet
        {
            get => _magnet;
            set => _magnet.AddChild(value);
        }
       
        public override void _Ready()
        {
            _magnet = GetNode<Spatial>(MagnetPath);
        }
    }
}