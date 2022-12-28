using Godot;
using System;
using Timer = System.Threading.Timer;

public class GreenSpatial : CSGBox
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Timer timer = new Timer(1000);
        timer.Change(Destroy)
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
