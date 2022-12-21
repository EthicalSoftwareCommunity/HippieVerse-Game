using Godot;
using System;

namespace HippieUniverse
{
    public class SpawnCrystalIconModel
    { 
        public string Text;
        public Vector3 Scale;

        public SpawnCrystalIconModel(string text)
        {
            Text = text;
        }
    }
    public class SpawnCrystalIcon : Spatial
    {
        [Export] private NodePath _textPath;
        [Export] private NodePath _iconPath;
        private Label3D _text;
        private Sprite3D _icon;
        
        public override void _Ready()
        {
            _text = GetNode<Label3D>(_textPath);
            _icon = GetNode<Sprite3D>(_iconPath);
        }

        public void Render(SpawnCrystalIconModel model)
        {
            LookAt(GlobalTranslation + GlobalTranslation.DirectionTo(MainSphereUtilites.MainSphere.Player.GlobalTranslation), Vector3.Up );
            _text.Text = model.Text;
        }
    }
}
