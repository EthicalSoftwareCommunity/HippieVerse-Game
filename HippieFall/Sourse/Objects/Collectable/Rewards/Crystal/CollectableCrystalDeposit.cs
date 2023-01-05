using Godot;
using System;
using Global.Data;

namespace HippieFall.Collectables
{
    public class CollectableCrystalDeposit : CollectableCrystal
    {
        private AnimationPlayer _animationPlayer;

        public override void _Ready()
        {
            _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        }

        private void OnAreaEntered(Area area)
        {
            if (area.GetOwnerOrNull<Player>() != null)
            {
                _animationPlayer.Play("To different sides");
            }
        }
        
        public override void Destroy()
        {
            //Animation Destroy
        }
        public override void ChangeConfigData(Config config)
        {
            Config = (CollectableCrystalDepositConfig)config; 
        }
    }
}
