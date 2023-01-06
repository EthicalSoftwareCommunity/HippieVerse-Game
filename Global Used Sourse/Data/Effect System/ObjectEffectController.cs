using System;
using System.Collections.Generic;
using Global.Data;
using Global.Data.EffectSystem;
using Godot;
using HippieFall.Collectables;
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
                get => _config.Duplicate() as Config;
                set => _config = value;
            }
            
            public Config DynamicConfig
            {
                set
                { 
                    if(Node is IEffectable node)
                        node.ChangeConfigData(value);
                }
            }
            public Node Node  { get; set; }
        }
        
        public List<NodeObject> NodeObjects { get; set; } = new();
        public EffectsController EffectController { get; private set; }

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
        public abstract void Init(Node node = null, Config config = null);
        
        //Set if checking node for needle *Class* 
        //Return new *Class*Config()

        private void ApplyDynamicEffects()
        {
            foreach (var nodeObject in NodeObjects)
                nodeObject.DynamicConfig = EffectController.
                    ApplyEffectsOnConfig(nodeObject.Config);
        }

        private void ApplyConstantEffect(Effect effect)
        {
            foreach (var nodeObject in NodeObjects)
                effect.Apply(nodeObject.Config);
            ApplyDynamicEffects();
        }
        
        public void AddNode(Node node, Config config)
        {
            node.Connect("tree_exited", this, nameof(RemoveObstacle), new Array(node));
            NodeObject nodeObject = new NodeObject();
            nodeObject.Node = node;
            nodeObject.Config = config ?? new Config();
            nodeObject.DynamicConfig = EffectController.ApplyEffectsOnConfig(nodeObject.Config);
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