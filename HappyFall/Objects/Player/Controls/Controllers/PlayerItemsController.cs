using System;
using System.Collections.Generic;
using Global.Constants;
using Global.Data.CharacterSystem;
using Global.Data;
using Global.Data.EffectSystem;
using Godot;
using HippieFall.Characters;
using HippieFall.Game;

namespace HippieFall.Items
{
    public class ItemObject
    {
        private Config _config;

        public Config Config
        {
            get => _config;
            set
            {
                _config = value;
                if (Item is IEffectable item)
                    item.ChangeConfigData(value);
            }
        }

        public Item Item { get; }

        public ItemObject(Item item, Config config)
        {
            Item = item;
            _config = config;
        }
    }

    public class PlayerItemsController : Node, IEffectable
    {
        public List<ItemObject> ItemObjects { get; private set; }
        public ItemConfig _itemConfig;

        private CharacterModel _characterModel;
        private MagnetConfig _magnet;

        public override void _Ready()
        {
            _magnet = new MagnetConfig();
            GetNode("/root").GetChild(0).Connect(
                nameof(GameController.GameIsReady), this,nameof(Init));
        }

        public void Init(GameController game)
        {
            _characterModel = game.Player.Character.CharacterModel;
            
            ItemObjects = new List<ItemObject>();
            Item item = GD.Load<PackedScene>(C_PlayerItemsPath.MAGNET).Instance<Magnet>();
            SetObjectModelByType(nameof(Magnet), item);
            ItemObjects.Add(new ItemObject(item, GetConfigByType(item)));
        }

        public void ChangeConfigData(Config config)
        {
            ItemConfig itemConfig = ((PlayerConfig)config).ItemConfig;
            foreach (ItemObject itemObject in ItemObjects)
                itemObject.Config = itemConfig;
        }

        private Config GetConfigByType(Item item)
        {
            Config config = null;
            if (item is Magnet)
                config = new MagnetConfig(_magnet);
            return config;
        }

        private void SetObjectModelByType(String type, Item obj)
        {
            switch (type)
            {
              case nameof(Magnet) : _characterModel.Magnet = obj; break;
            }
        }
    }
}