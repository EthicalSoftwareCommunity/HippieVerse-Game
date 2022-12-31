using Godot;
using System;
using System.Collections.Generic;
using Global;

public class VisibleOneOfList : Node
{
    [Export] private List<NodePath> _listPath;
    private List<Spatial> _list;
    public override void _Ready()
    {
        _list = new();
        foreach (var path in _listPath)
        {
            _list.Add(GetNode<Spatial>(path));
        }
        _list[Utilities.GetRandomNumberInt(0,_list.Count)].
            Visible=true;    
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
