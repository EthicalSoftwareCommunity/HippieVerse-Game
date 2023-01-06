using Godot;
using System;
using System.Collections.Generic;
using Global.Constants;
using HippieFall.Biomes;

public class DoubleTunnel : StaticBody
{
    private Biome _cyberBiome;
    private Biome _bikerBiome = null;//= new Biome(C_BiomeTypes.BIKER);
    private Biome _hippieBiome;
    private List<Biome> _biomes;
    public override void _Ready()
    {
        _cyberBiome = new(C_BiomeTypes.CYBER);
        _hippieBiome = new(C_BiomeTypes.HIPPIE);
        _biomes = new List<Biome> { _cyberBiome, _bikerBiome, _hippieBiome };
    }
}
