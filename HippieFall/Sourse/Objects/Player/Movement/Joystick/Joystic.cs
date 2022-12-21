using Global.Constants;
using Godot;
using HippieUniverse.Objects.Settings;
using Newtonsoft.Json;
using HippieFall.Game;
using Global.Data.CharacterSystem;

namespace HippieFall
{
    public class Joystic : TouchScreenButton
    {
        [Export] NodePath _parentJoysticNodePath;
        [Export] NodePath _parentJoysticNodeControlAreaPath;
        [Export] NodePath _rightPositionForJoystickPath;

        private readonly int _boundary = 64;
        private readonly Vector2 _radius = new Vector2(28, 28);
        private readonly float _returnAccel = 20f;
        private readonly float _threshold = 5;
        private int _onGoingDrag = -1;
        private bool _canDraging = false;
        private SettingsUIConfig _config;
        private File _file;
        private GameInterface _Interface { get; set; }
        private Sprite _parentJoysticNode;
        private Control _parentJoysticNodeControlArea;
        public CharacterInterface _characterInterface;
        public AbilityButtonsController AbilityButtonsController { get; private set; }

        public Joystic()
        {
            _file = new File();
            Directory directory = new Directory();
            if (directory.DirExists(C_SaveFolderFile.SETTINGS_SAVES) == false)
                directory.MakeDir(C_SaveFolderFile.SETTINGS_SAVES);
            if (directory.DirExists(C_SaveFolderFile.FOLDER_HIPPIE_UNIVERSE_SETTINGS) == false)
                directory.MakeDir(C_SaveFolderFile.FOLDER_HIPPIE_UNIVERSE_SETTINGS);
            if (_file.FileExists(C_SaveFolderFile.FILE_HIPPIE_UNIVERSE_SETTINGS) == false)
            {
                _file.Open(C_SaveFolderFile.FILE_HIPPIE_UNIVERSE_SETTINGS, File.ModeFlags.Write);
                _file.Close();
            }

        }


        public override void _Ready()
        {
            _config = LoadSettings();

            _parentJoysticNode = GetNode<Sprite>(_parentJoysticNodePath);
            _parentJoysticNodeControlArea = GetNode<Control>(_parentJoysticNodeControlAreaPath);


            _parentJoysticNode.Position = new Vector2(900, 200);
            _parentJoysticNodeControlArea.SetPosition(new Vector2(900, 200));

            _characterInterface.Connect("Ready", this, "Init");

        }
        private void Init()
        {
            AbilityButtonsController = _characterInterface.AbilityButtonsController;
            AbilityButtonsController.SetPosition(new Vector2(20, 200));
        }


        public override void _Process(float delta)
        {
            if (_onGoingDrag == -1)
            {
                Vector2 differencePosition = new Vector2(0, 0) - _radius - Position;
                Position += differencePosition * _returnAccel * delta;
            }
        }

        private void _on_Control_gui_input(InputEvent @event)
        {
            if (@event is InputEventScreenTouch inputEvent)
            {
                GD.Print("GUI");
                if (inputEvent.Pressed)
                    _canDraging = true;
            }
        }
        public override void _Input(InputEvent @event)
        {
            if (_canDraging)
            {
                if (@event is InputEventScreenDrag)
                {
                    InputEventScreenDrag eventDrag = (InputEventScreenDrag)@event;
                    if (eventDrag.Index != _onGoingDrag && _onGoingDrag != -1) return;
                    Vector2 parentGlobalPosition = new Vector2(GetParent<Node2D>().Position.x,
                        GetParent<Node2D>().Position.y);
                    float eventDistantionFromCenter = (eventDrag.Position - parentGlobalPosition).Length();

                    if (eventDistantionFromCenter >= _boundary * GlobalScale.x || eventDrag.Index == _onGoingDrag)
                    {
                        GlobalPosition = eventDrag.Position - _radius * GlobalScale;

                        if (GetButtonPosition().Length() > _boundary)
                            Position = GetButtonPosition().Normalized() * _boundary - _radius;
                        _onGoingDrag = eventDrag.Index;
                    }
                }

                if (@event is InputEventScreenTouch && !@event.IsPressed() &&
                    ((InputEventScreenTouch)@event).Index == _onGoingDrag)
                {
                    _onGoingDrag = -1;
                    _canDraging = false;
                    Position = Vector2.Zero;
                }
            }
        }

        public Vector2 GetValue()
        {
            if (GetButtonPosition().Length() > _threshold)
                return GetButtonPosition().Normalized();
            return new Vector2(0, 0);
        }

        public Vector2 GetButtonPosition()
        {
            return Position + _radius;
        }

        public SettingsUIConfig LoadSettings()
        {
            _file.Open(C_SaveFolderFile.FILE_HIPPIE_UNIVERSE_SETTINGS, File.ModeFlags.Read);
            SettingsUIConfig data = JsonConvert.DeserializeObject<SettingsUIConfig>(_file.GetAsText());
            _file.Close();
            return data ?? new SettingsUIConfig();
        }

    }
}