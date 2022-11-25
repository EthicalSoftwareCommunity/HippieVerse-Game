using System;
using System.Collections.Generic;
using Global.Data;
using Global.Data.EffectSystem;
using Godot;
using HippieFall.Tunnels;
using Array = Godot.Collections.Array;

namespace Global.GameSystem
{
    public abstract class ObjectEffectController : Node
    {
        public class NodeObject
        {
            private Config _config;
            public Config Config
            {
                get => _config;
                set
                {
                    _config = value;
                    if(Node is IEffectable node)
                        node.ChangeConfigData(value);
                }
            }
            public Node Node  { get; set; }
        }

        public event Action<Config> OnConfigChanged;
        public List<NodeObject> NodeObjects { get; set; } = new();
        public List<Config> Configs { get; private set; } = new();
        public EffectsController EffectController { get; private set; }
        public Config Config { get; set; }

        public ObjectEffectController(Effect.EffectsTarget effectsTarget)
        {
            EffectController = new EffectsController(effectsTarget);
            EffectController.DynamicEffectAdded += ApplyDynamicEffects;
            EffectController.ConstantEffectAdded += ApplyConstantEffect;
        }

        public override void _Ready()
        {
            ApplyDynamicEffects();
        }

        //Add dependencies from GameController
        //Connect events
        protected abstract void Init();
        
        //Set if checking node for needle *Class* 
        //Return new *Class*Config()
        protected abstract Config GetConfigByType(Node node);

        private void ApplyDynamicEffects()
        {
            foreach (var nodeObject in NodeObjects)
                nodeObject.Config = EffectController.
                    ApplyEffectsOnConfig(GetConfigByType(nodeObject.Node));
            OnConfigChanged?.Invoke(Config);
        }

        private void ApplyConstantEffect(Effect effect)
        {
            foreach (var config in Configs)
                effect.Apply(config);
            ApplyDynamicEffects();
        }
        
        public void AddNode(Node node)
        {
            node.Connect("tree_exited", this, nameof(RemoveObstacle), new Array(node));
            NodeObject nodeObject = new NodeObject
            {
                Node = node,
                Config = EffectController.ApplyEffectsOnConfig(GetConfigByType(node))
            };
            NodeObjects.Add(nodeObject);
        }
        
        public void RemoveObstacle(Node node)
        {
            foreach (var nodeObject in NodeObjects)
                if (nodeObject.Node == node)
                {
                    NodeObjects.Remove(nodeObject);
                    return;
                }
        }
        
    }
}