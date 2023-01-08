using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Global.Constants;
using Global.Data;
using Global.Data.Reward;
using Global.GameSystem;
using Godot.Collections;
using HippieFall.Bot;
using HippieFall.Game;
using HippieFall.Tunnels;
using Newtonsoft.Json;
using Array = Godot.Collections.Array;
using Timer = System.Timers.Timer;

public class BotAnalys : Node
{
    private ObstaclesController _obstacles;
    private Obstacle CurrentObstacle; 
    [Export] private NodePath botPath; 
    private Bot _bot;
    private float _rayCastLenght = -8;
    private int _rayCastCount = 100;
    private bool[,] _rayCastMatrix;
    private float[,] _rayCastWeightMatrix;
    public override void _Ready()
    {
        _bot = GetNode<Bot>(botPath);
        _rayCastMatrix = new bool[_rayCastCount,_rayCastCount];
       // HippieFallUtilities.ConnectFeedbackAfterGameReadiness(this);
        //SetNearestObstacle();
        Timer timer = new Timer(1000f);
        timer.Elapsed += UpdateMatrix;
        timer.Start();
    }
    private void Init()
    {
        _obstacles = HippieFallUtilities.Game.Level.Spawner.ObstaclesController;
   
    }

    public override void _PhysicsProcess(float delta)
    {
        /*if (CurrentObstacle.Translation.y < _bot.Translation.y)
        {
            SetNearestObstacle();
        }*/
    }

    private float radius = 3f;
    private void UpdateMatrix(object sender, ElapsedEventArgs e)
    {
        _rayCastMatrix = new bool[_rayCastCount, _rayCastCount];
        var spaceState = _bot.GetWorld().DirectSpaceState;
        for (int i = 1; i < _rayCastCount; i++)
        {
            for (int j = 1; j < _rayCastCount; j++)
            {
                Vector3 position = new Vector3(-radius + (float)i/_rayCastCount*radius*2, _bot.Translation.y, radius - (float)j/_rayCastCount*radius*2);
                _rayCastMatrix[i, j] = spaceState.IntersectRay(position,
                    new Vector3(-radius + (float)i/_rayCastCount*radius*2, _rayCastLenght, radius - (float)j/_rayCastCount*radius*2),
                    new Array() { _bot }, collideWithAreas:true).Count > 0;
            }
        }
       
        _rayCastWeightMatrix = new float[_rayCastCount*10, _rayCastCount*10];
        for (int i = 0; i < _rayCastCount*10; i++)
        {
            for (int j = 0; j < _rayCastCount*10; j++)
            {
                if (_rayCastMatrix[i, j])
                {
                    AddBoxWeightOnCell(_rayCastWeightMatrix, i*10, j*10);
                }
            }
        }
        for (int i = 0; i < _rayCastCount*10; i++)
        for (int j = 0; j < _rayCastCount*10; j++)
                if (_rayCastWeightMatrix[i, j] < 0) 
                    _rayCastWeightMatrix[i, j] = 0;
        
        _file = new File();
        _file.Open("user://matrixBool.txt", File.ModeFlags.Write);
        string str = "";
        for (int i = 0; i < _rayCastCount*10; i++)
        {
            for (int j = 0; j < _rayCastCount*10; j++)
            {
                char ch = _rayCastWeightMatrix[i, j] > 0 ? '-' : '|';
                str += ch + "\t";
            }
            str += "\n";
        }
        _file.StoreString(str);
        _file.Close();
        
    }

    private File _file; 

    private void AddBoxWeightOnCell(float[,] rayCastWeightMatrix, int i, int j)
    {
        Point center = new Point(4, 4);
        for (int k = 0; k < 7; k++)
        {
            for (int l = 0; l < 7; l++)
            {
                Point cell = new Point(k, l);
                try
                {
                    if (i == k-3+i && j ==  l-3+j)
                        rayCastWeightMatrix[i, j] += 1;
                    else if (CanWriteValue(k - 3 + i, l - 3 + j))
                    {
                        if (center.GetDistance(cell) == 0 || center.GetDistance(cell) == 1)
                            rayCastWeightMatrix[k - 3 + i, l - 3 + j] += 1;
                        else
                        {
                            rayCastWeightMatrix[k - 3 + i, l - 3 + j] += 1 / center.GetDistance(cell) - 0.2f;
                        }
                    }
                }
                catch (Exception e)
                {
                    GD.Print(e);
                }
                finally
                {
                    //do what you want to complete with this function.
                }
            }
        }
      
    }

    private bool CanWriteValue(int i, int j)
    {
        if(i>=0 && i <_rayCastCount
           && j>=0 && j <_rayCastCount)
            return true;
        return false;
    }
    private void SetNearestObstacle()
    {
        foreach (ObjectEffectController.NodeObject nodeObject in _obstacles.NodeObjects)
        {
            if (nodeObject.Node is Obstacle obstacle)
            {
                var position = obstacle.Translation;
                if (position.y > _bot.Translation.y)
                    continue;
                CurrentObstacle = obstacle;
                return;
            }
        }
    }

    private void SetObstacleType()
    {
        /*if (CurrentObstacle is Fan fan)
            CurrentObstacle = fan;
        if (CurrentObstacle is Saw saw)
            CurrentObstacle = saw;
        if (CurrentObstacle is  Gate gate)
            CurrentObstacle = gate;
        if (CurrentObstacle is HalfWall halfWall)
            CurrentObstacle = halfWall;
        if (CurrentObstacle is HiddenTrap hiddenTap)
            CurrentObstacle = hiddenTap;
        if (CurrentObstacle is PerforatedWall gate)
            CurrentObstacle = gate;*/

    }
    internal class Point
    {
        public float x;
        public float y;

        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float GetDistance(Point point)
        {
            double a = (double)(point.x - x);
            double b = (double)(point.y - y);
            float result = (float)Math.Sqrt(a * a + b * b);
            return result;
        }
    }
}
