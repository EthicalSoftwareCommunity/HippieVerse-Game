using Godot;
using Global.Constants;
using HippieUniverse.Objects.Settings;
using Newtonsoft.Json;
using HippieFall.Game;
using System;

public class SwipeControl : TouchScreenButton
{
    private File _file;
    private SettingsUIConfig _config;
    public bool Swiping;
    public Vector2 SwipePosition;
    public Vector2 SwipeStart;

    public SwipeControl()
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
        HippieFallUtilities.ConnectFeedbackAfterGameReadiness(this);
        _config = LoadSettings();
    }

    public void Init()
    {   if(_config.SwipeControlButtonValue)
         DisableJoystick();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventScreenTouch eventMouseButton)
        {
            Swiping = true;

            if (@event.IsPressed())
            {
                SwipeStart = eventMouseButton.Position;
            }

            else
            {
                CalculateSwipeAngular(eventMouseButton.Position,(float)0.6);
            }
            

        }

    }

    private void CalculateSwipeAngular(Vector2 SwipeEnd,float delta)
    {
        Vector2 Swipe = SwipeEnd - SwipeStart;


        if (Swipe.x > 0)
            SwipePosition += new Vector2(delta*Math.Abs(Swipe.x), 0);
        else
            SwipePosition -= new Vector2(delta*Math.Abs(Swipe.x), 0);

        if (Swipe.y>0)
            SwipePosition += new Vector2(0,delta*Math.Abs(Swipe.y));
        else
            SwipePosition -= new Vector2(0,delta*Math.Abs(Swipe.y));


    }

    public SettingsUIConfig LoadSettings()
    {
        _file.Open(C_SaveFolderFile.FILE_HIPPIE_UNIVERSE_SETTINGS, File.ModeFlags.Read);
        SettingsUIConfig data = JsonConvert.DeserializeObject<SettingsUIConfig>(_file.GetAsText());
        _file.Close();
        return data ?? new SettingsUIConfig();
    }

    public void DisableJoystick()
    {
        // set the joystick to non visible 
        GetNode<Sprite>(HippieFallUtilities.Game.GameInterface.Joystic.GetParent().GetPath()).Visible = false;

    }


}
