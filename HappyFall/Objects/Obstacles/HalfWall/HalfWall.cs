using Godot;
using System;
using Global;
using HippieFall.Tunnels;

namespace HippieFall.Tunnels
{
    class HalfWall : Obstacle
    {
        public override void _Ready()
        {
            RotateY(Utilities.GetRandomNumberFloat(-10,10));
        }
    }

}