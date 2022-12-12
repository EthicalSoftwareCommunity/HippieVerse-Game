using Global.Data;

namespace HippieUniverse.Objects.Settings;

public class SettingsUIConfig:Config
{
    public float MusicSliderValue=100f;
    public float SoundsSliderValue=100f;
    public float SensitivitySliderValue=0.5f;
    public float SpeedSliderValue=20f;

    public bool MusicCheckButtonValue = false;

    public bool SoundsCheckButtonValue = false;

    public SettingsUIConfig()
    {
    }
    public SettingsUIConfig(SettingsUIConfig config)
    {
        MusicSliderValue = config.MusicSliderValue;
        SoundsSliderValue = config.SoundsSliderValue;
        SensitivitySliderValue = config.SensitivitySliderValue;
        SpeedSliderValue = config.SpeedSliderValue;
        MusicCheckButtonValue = config.MusicCheckButtonValue;
        SoundsCheckButtonValue = config.SoundsCheckButtonValue;
    }
}