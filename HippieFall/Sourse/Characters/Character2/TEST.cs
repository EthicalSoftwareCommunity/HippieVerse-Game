using Godot;
using System.Collections.Generic;
using Array = Godot.Collections.Array;

public class TEST : Spatial
{
    [Export] private List<NodePath> _arrayBonedPath;
    private List<PhysicalBone> _bones;
    private Skeleton _skeleton;
    public override void _Ready()
    {
        _bones = new ();
        _skeleton = GetNode<Skeleton>("Skeleton");
        foreach (var bonePath in _arrayBonedPath)
        {
            _bones.Add(GetNode<PhysicalBone>(bonePath));
        }
        //_skeleton.PhysicalBonesStartSimulation(new Array(_bones));
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
