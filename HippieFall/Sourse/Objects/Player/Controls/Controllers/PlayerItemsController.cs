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
        private ShieldConfig _shield;

        public override void _Ready()
        {
            _magnet = new MagnetConfig();
            _shield = new ShieldConfig();
            GetNode("/root").GetChild(0).Connect(
                nameof(GameController.GameIsReady), this,nameof(Init));
        }

        public void Init(GameController game)
        {
            _characterModel = game.Player.Character.CharacterModel;
            _itemConfig = new ItemConfig();
            ItemObjects = new List<ItemObject>();
            Item magnet = GD.Load<PackedScene>(C_PlayerItemsPath.MAGNET).Instance<Magnet>();
            Item shield = GD.Load<PackedScene>(C_PlayerItemsPath.SHIELD).Instance<Shield>();
            SetObjectModelByType(nameof(Magnet), magnet);
            SetObjectModelByType(nameof(Shield), shield);
            ItemObjects.Add(new ItemObject(magnet, GetConfigByType(magnet)));
            ItemObjects.Add(new ItemObject(shield, GetConfigByType(shield)));
        }

        public void ChangeConfigData(Config config)
        {
            _itemConfig = ((PlayerConfig)config).ItemConfig;
            foreach (ItemObject itemObject in ItemObjects)
                itemObject.Config = _itemConfig;
        }

        private Config GetConfigByType(Item item)
        {
            Config config = null;
            if (item is Magnet)
                config = new MagnetConfig(_magnet);
            if (item is Shield)
                config = new ShieldConfig(_shield);
            return config;
        }

        private void SetObjectModelByType(String type, Item obj)
        {
            switch (type)
            {
              case nameof(Magnet) : _characterModel.Magnet = obj; break;
              case nameof(Shield) : _characterModel.Shield = obj; break;
            }
        }
    }
}