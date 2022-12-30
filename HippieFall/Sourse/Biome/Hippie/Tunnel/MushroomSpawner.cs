using Godot;
using System;
using System.Collections.Generic;
using Global;
using HippieFall.Game;

namespace HippieFall.Biomes
{
    public class MushroomSpawner : Node
    {
        [Export] private List<PackedScene> _mushrooms; 
        [Export] private List<NodePath> _pointsPath;
        private List<Spatial> _points;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _points = new();
            foreach (var path in _pointsPath)
            {
                _points.Add(GetNode<Spatial>(path));
            }
            _points[Utilities.GetRandomNumberInt(0,_points.Count)].AddChild(_mushrooms[Utilities.GetRandomNumberInt(0,_mushrooms.Count)].Instance().Duplicate());    
        }
    }
}
