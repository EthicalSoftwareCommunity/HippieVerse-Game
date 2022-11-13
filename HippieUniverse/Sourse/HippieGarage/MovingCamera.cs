using System;
using System.Collections.Generic;
using Godot;

public class MovingCamera : Camera
{
    public class CameraCharacteristics
    {
        public float Far;
        public float Fov;
        public float Near;
        public float Size;
        public Vector3 GlobalTranslation;
        public Vector3 GlobalRotation;
        public CameraCharacteristics(Camera camera)
        {
            Far = camera.Far;
            Fov = camera.Fov;
            Near = camera.Near;
            Size = camera.Size;
            GlobalTranslation = new Vector3(camera.GlobalTranslation);
            GlobalRotation = new Vector3(camera.GlobalRotation);
        }
    }
    [Export] private float _movingTime = 2f;
    [Export] private List<NodePath> _toMoveCameraPath;
    private List<CameraCharacteristics> _toMoveCamera = new List<CameraCharacteristics>();
    private CameraCharacteristics _startCamera;
    private CameraCharacteristics _endCamera;
    private int _cameraCount = 0;
    private float _currentTime;
    private bool _needToMove = false;
    private float _distance;
    public override void _Ready()
    {
        SetPhysicsProcess(false);
        _toMoveCamera.Add(new CameraCharacteristics(this));
        foreach (var nodePath in _toMoveCameraPath)
        {
            _toMoveCamera.Add(new CameraCharacteristics(GetNode<Camera>(nodePath)));
        }
        _startCamera = (_toMoveCamera[_cameraCount]);
        _endCamera = (_toMoveCamera[_cameraCount+1]);
    }

    private void MoveToCamera()
    {
        _currentTime = 0;
        _needToMove = true;
        SetPhysicsProcess(true);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouse mouse)
        {
            if (mouse.IsPressed() && _needToMove == false)
                MoveToCamera();
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        if (_movingTime - _currentTime >= 0 && _needToMove)
        {
            _distance = _endCamera.GlobalTranslation.DistanceTo(GlobalTranslation);
            _distance /= 15;
            if(_distance>2)
                _currentTime += delta*(_distance); 
            else _currentTime += delta;
            ChangeCharacteristics(_endCamera, _currentTime / _movingTime);
        }
        else
        {
            _needToMove = false;
            SetPhysicsProcess(false);
            _cameraCount++;
            _startCamera = _toMoveCamera[_cameraCount];
            if (_cameraCount + 1 == _toMoveCamera.Count)
                _cameraCount = -1;
            _endCamera = _toMoveCamera[_cameraCount+1];
        }
    }

    private void ChangeCharacteristics(CameraCharacteristics camera, float coefficient)
    {
        Far = _startCamera.Far + (camera.Far - _startCamera.Far)*coefficient;
        Fov = _startCamera.Fov + (camera.Fov - _startCamera.Fov)*coefficient;
        Near = _startCamera.Near + (camera.Near - _startCamera.Near)*coefficient;
        Size = _startCamera.Size + (camera.Size - _startCamera.Size)*coefficient;
        GlobalTranslation = _startCamera.GlobalTranslation + (camera.GlobalTranslation - _startCamera.GlobalTranslation) * coefficient;
        GlobalRotation = _startCamera.GlobalRotation + (camera.GlobalRotation - _startCamera.GlobalRotation) * coefficient;
    }
    
}
