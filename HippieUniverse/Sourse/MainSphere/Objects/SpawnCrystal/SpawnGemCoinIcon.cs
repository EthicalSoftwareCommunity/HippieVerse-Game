using Godot;
using System;

namespace HippieUniverse
{
    public class SpawnGemCoinIconModel
    { 
        public string Text;
        public Vector3 Scale;

        public SpawnGemCoinIconModel(string text)
        {
            Text = text;
        }
    }
    public class SpawnGemCoinIcon : Spatial
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

        public void Render(SpawnGemCoinIconModel model)
        {
            LookAt(GlobalTranslation + GlobalTranslation.DirectionTo(MainSphereUtilites.MainSphere.Player.GlobalTranslation), Vector3.Up );
            //RotateZ(Mathf.Deg2Rad(180));
           // RotationDegrees += new Vector3(0,90,0);
            _text.Text = model.Text;
            var scale = Transform.basis.Scale *
                        MainSphereUtilites.MainSphere.Player.GlobalTranslation.DistanceTo(GlobalTranslation);
            //GlobalScale(Transform.basis.Scale * MainSphereUtilites.MainSphere.Player.GlobalTranslation.DirectionTo(GlobalTranslation));
        }
    }
}
