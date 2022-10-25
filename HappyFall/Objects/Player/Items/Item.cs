using System;
using Godot;

namespace HippieFall.Items
{
    public abstract class Item : Spatial
    {
        private event Action<String,Item> OnItemActivateStateChanged;
    }
}