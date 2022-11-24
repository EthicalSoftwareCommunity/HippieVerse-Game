using Godot;
using System;
using System.Collections.Generic;
using Global;

public class GemCoinSpawner : Node
{
    [Export] private PackedScene _crystal;
    [Export] private List<NodePath> _spawnPointsPaths;
    private List<Spatial> _spawnPoints;
    private bool _isSpawned;
    public override void _Ready()
    {
        _spawnPoints = new();
        foreach (var path in _spawnPointsPaths)
            _spawnPoints.Add(GetNode<Spatial>(path));
        
        SpawnCrystals();
    }

    private void SpawnCrystals()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            int number = Utilities.GetRandomNumberInt(0, 10);
            int checkNumber = Utilities.GetRandomNumberInt(0, 10);
            Spawn(spawnPoint);
            if (number == checkNumber)
            {
                
                GD.Print(spawnPoint.Name);
                _isSpawned = true;
            }
        }

        if (_isSpawned == false)
            Spawn(_spawnPoints[Utilities.GetRandomNumberInt(0, _spawnPoints.Count-1)]);
    }

    private void Spawn(Spatial spawnPoint)
    {
        spawnPoint.AddChild(_crystal.Instance());
    }
}
