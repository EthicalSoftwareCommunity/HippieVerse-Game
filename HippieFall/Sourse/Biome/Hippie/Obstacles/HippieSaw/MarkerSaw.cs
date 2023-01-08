using Godot;
using System;

public class MarkerSaw : Spatial
{
    [Export] private NodePath _sawPath;
    private Spatial _saw;
    public override void _Ready()
    {
        _saw = GetNode<Spatial>(_sawPath);
    }

    public override void _PhysicsProcess(float delta)
    {
        Translation = new Vector3(-_saw.Translation.x, 0,  -_saw.Translation.y);
    }
}
