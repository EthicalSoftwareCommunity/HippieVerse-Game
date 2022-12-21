using Godot;
using Global.Constants;
using HippieUniverse.Objects.Settings;
using Newtonsoft.Json;
using Array = Godot.Collections.Array;

public class SettingsUI : Control
{
    [Export] private NodePath _musicSliderPath;
    [Export] private NodePath _soundSliderPath;
    [Export] private NodePath _sensitivitySliderPath;
    [Export] private NodePath _speedSliderPath;
    [Export] private NodePath _musicCheckButtonPath;
    [Export] private NodePath _soundsCheckButtonPath;
    [Export] private NodePath _saveSettingsButtonPath;
    [Export] private NodePath _closeSettingsButtonPath;
    [Export] private NodePath _JoystickPositionButtonPath;

    private HSlider _musicSlider;
    private HSlider _soundsSlider;
    private HSlider _sensitivitySlider;
    private HSlider _speedSlider;
    private CheckButton _musicCheckButton;
    private CheckButton _soundsCheckButton;
    private Button _saveSettingsButton;
    private Button _closeSettingsButton;
    private CheckButton _JoystickPositionButton;

    private SettingsUIConfig _config;
    private File _file;

    public SettingsUI()
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
        _musicSlider = GetNode<HSlider>(_musicSliderPath);
        _soundsSlider = GetNode<HSlider>(_soundSliderPath);
        _sensitivitySlider = GetNode<HSlider>(_sensitivitySliderPath);
        _speedSlider = GetNode<HSlider>(_speedSliderPath);
        _musicCheckButton = GetNode<CheckButton>(_musicCheckButtonPath);
        _soundsCheckButton = GetNode<CheckButton>(_soundsCheckButtonPath);
        _saveSettingsButton = GetNode<Button>(_saveSettingsButtonPath);
        _closeSettingsButton = GetNode<Button>(_closeSettingsButtonPath);
        _JoystickPositionButton = GetNode<CheckButton>(_JoystickPositionButtonPath);

        _musicCheckButton.Connect("toggled", this,
            nameof(ChangeSliderValue), new Array(_musicSlider));
        _soundsCheckButton.Connect("toggled", this,
            nameof(ChangeSliderValue), new Array(_soundsSlider));
        _saveSettingsButton.Connect("pressed", this, nameof(SaveSettings));
        _closeSettingsButton.Connect("pressed", this, nameof(OpenSettings), new Array(false));

        _config = LoadSettings();
        SetSettingsValues();

    }

    private void ChangeSliderValue(bool buttonPressed, HSlider slider)
    {
        if (slider == _musicSlider)
        {
            if (buttonPressed) _musicSlider.Value = _musicSlider.MinValue;
            else _musicSlider.Value = _config.MusicSliderValue;
            _musicSlider.Editable = !buttonPressed;
        }

        if (slider == _soundsSlider)
        {
            if (buttonPressed) _soundsSlider.Value = _soundsSlider.MinValue;
            else _soundsSlider.Value = _config.SoundsSliderValue;
            _soundsSlider.Editable = !buttonPressed;
        }
    }

    private void SaveSettings()
    {
        _config.MusicSliderValue = (float)_musicSlider.Value;
        _config.SoundsSliderValue = (float)_soundsSlider.Value;
        _config.SensitivitySliderValue = (float)_sensitivitySlider.Value;
        _config.SpeedSliderValue = (float)_speedSlider.Value;
        _config.MusicCheckButtonValue = _musicCheckButton.Pressed;
        _config.SoundsCheckButtonValue = _soundsCheckButton.Pressed;
        _config.JoystickPositionButton = _JoystickPositionButton.Pressed ? SettingsUIConfig.JoystickPositions.right : SettingsUIConfig.JoystickPositions.left;
    
        _file.Open(C_SaveFolderFile.FILE_HIPPIE_UNIVERSE_SETTINGS, File.ModeFlags.Write);
        _file.StoreString(JsonConvert.SerializeObject(_config));
        _file.Close();
    }

    public void OpenSettings(bool isOpen)
    {
        Visible = isOpen;
    }
    public SettingsUIConfig LoadSettings()
    {
        _file.Open(C_SaveFolderFile.FILE_HIPPIE_UNIVERSE_SETTINGS, File.ModeFlags.Read);
        SettingsUIConfig data = JsonConvert.DeserializeObject<SettingsUIConfig>(_file.GetAsText());
        _file.Close();
        return data ?? new SettingsUIConfig();
    }

    private void SetSettingsValues()
    {
        _musicSlider.Value = _config.MusicSliderValue;
        _soundsSlider.Value = _config.SoundsSliderValue;
        _sensitivitySlider.Value = _config.SensitivitySliderValue;
        _speedSlider.Value = _config.SpeedSliderValue;
        _musicCheckButton.Pressed = _config.MusicCheckButtonValue;
        _soundsCheckButton.Pressed = _config.SoundsCheckButtonValue;
        _JoystickPositionButton.Pressed = _config.JoystickPositionButton == SettingsUIConfig.JoystickPositions.right ? true : false;
    }

}