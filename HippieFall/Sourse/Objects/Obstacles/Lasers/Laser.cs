using Godot;
using System;
using HippieFall.Tunnels;

public class Laser : Obstacle
{
    [Export] private NodePath modelPath;
    public CSGBox Model { get; private set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Model = GetNode<CSGBox>(modelPath);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
