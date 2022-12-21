using Godot;
using System;
using System.Drawing;
using Global;
using Global.Data.Reward;

namespace HippieUniverse
{
    public class SpawnCrystal : InteractionController
    {
        [Export] private NodePath _spawnCoinIconPath;
        private SpawnCrystalIcon _spawnCountIcon;
        private int _maxCrystalCount = 10;
        private int _minCrystalCount = 2;
        private int _crystalCount;
        private AnimationPlayer _animationPlayer;
        public override void _Ready()
        {
            _crystalCount = Utilities.GetRandomNumberInt(_minCrystalCount, _maxCrystalCount);
            _animationPlayer = GetNode<AnimationPlayer>("../AnimationPlayer");
            _spawnCountIcon = GetNode<SpawnCrystalIcon>(_spawnCoinIconPath);
        }

        private void CollectCrystal()
        {
            _spawnCountIcon.Render(new SpawnCrystalIconModel(_crystalCount.ToString()));
            RewardData data = new RewardData()
            {
                Crystal = { Count = _crystalCount }
                
            };
            new RewardController().RewardSaveLoadSystem.SaveRewards(data);
            _animationPlayer.Play(_animationPlayer.GetAnimationList()[0]);
        }

        private void OnAnimationPlayerAnimationFinished(string animationName)
        {
            GetParent().QueueFree();
        }
        public override void HoldTouchInteract()
        {
            base.HoldTouchInteract();
            CollectCrystal();
        }
    }
}
