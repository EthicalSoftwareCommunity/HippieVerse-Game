using Godot;

namespace HippieFall.Characters
{
    public class CharacterModel : Spatial
    {
        [Export] public NodePath MagnetPath;
        [Export] public NodePath ShieldPath;
        private Spatial _magnet;
        private Spatial _shield;
        
        public Spatial Magnet
        {
            get => _magnet;
            set => _magnet.AddChild(value);
        }
        public Spatial Shield
        {
            get => _shield;
            set => _shield.AddChild(value);
        }
       
        public override void _Ready()
        {
            _magnet = GetNode<Spatial>(MagnetPath);
            _shield = GetNode<Spatial>(ShieldPath);
        }
    }
}