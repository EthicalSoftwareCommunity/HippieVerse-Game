using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Global;
using Global.Constants;
using HippieFall;
using HippieFall.Biomes;
using HippieFall.Game;
using HippieFall.Tunnels;

public class DoubleTunnel : Tunnel
{
    private Biome _cyberBiome;
    private Biome _bikerBiome = null;//= new Biome(C_BiomeTypes.BIKER);
    private Biome _hippieBiome;
    private List<Biome> _biomes;
    private Biome CurrentBiome;
    private TunnelSpawner _spawner;
    public override void _Ready()
    {
        _cyberBiome = new(C_BiomeTypes.CYBER);
        _hippieBiome = new(C_BiomeTypes.HIPPIE);
        _biomes = new List<Biome> { };
        _spawner = HippieFallUtilities.Game.Level.Spawner; 
        SetBiome();
        _spawner.SpawnTunnel(
            _spawner.Tunnels.Last().Translation 
            + _spawner.TunnelsOffset*1.5f + new Vector3(-3,0,0),
            CurrentBiome,
            (Obstacle)CurrentBiome.Gate.Instance()
            );
        SetBiome();
        _spawner.SpawnTunnel(
            _spawner.Tunnels.Last().Translation 
            + new Vector3(6,0,0),
            CurrentBiome,
            (Obstacle)CurrentBiome.Gate.Instance()
        );
    }

    private void SetChosenBiome()
    {
        _spawner.ResetBiome();
        _spawner.CurrentBiome = CurrentBiome;
        _spawner.LoadObstacles();
        _spawner.FillOrder();
        _spawner.ObstacleOrder.RemoveAt(0);
        _spawner.IsDoubleTunnelPrevious = false;
        _spawner.StartLevel();
    }

    private void OnTunnelAreaEntered(Area area, string side)
    {
        if (area.GetOwnerOrNull<Player>() is { })
        {
           // Vector3 moveOffset =  new Vector3(0, 0, 0);
            if (side == "Left")
            {
                CurrentBiome = _biomes[0];
                //moveOffset =  new Vector3(-3, 0, 0);
            }
            if (side == "Right")
            {
                CurrentBiome = _biomes[1];
               // moveOffset =  new Vector3(3, 0, 0);
            }
            /*HippieFallUtilities.Game.Player.Translation += moveOffset;
            HippieFallUtilities.Game.GameCamera.Translation += moveOffset;
            Translation += moveOffset;*/
            SetChosenBiome();
        }
    }
    private void SetBiome()
    {
        List<string> uniqueBiomes = new List<string>(C_BiomeTypes.BIOME_NAMES);
        if(CurrentBiome != null)
            uniqueBiomes.Remove(CurrentBiome.BiomeName);
        string biomeName = uniqueBiomes[Utilities.GetRandomNumberInt(0, uniqueBiomes.Count)];
		
        switch (biomeName)
        {
            case C_BiomeTypes.CYBER: CurrentBiome = _cyberBiome; break;
            case C_BiomeTypes.HIPPIE: CurrentBiome = _hippieBiome; break;
        }
        _biomes.Add(CurrentBiome);
    }
}
