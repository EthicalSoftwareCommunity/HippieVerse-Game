namespace Global.Constants
{
    public class C_SaveFolderFile
    {
        private const string USER_SAVES = "user://";
        public const string FOLDER_CHARACTER_SAVES = USER_SAVES + "character_saves/";
        public const string FILE_CHARACTER = FOLDER_CHARACTER_SAVES + "choosen_character.json";

        public const string FOLDER_PLAYER_HIGH_SCORE = USER_SAVES + "game_saves/";
        public const string FILE_PLAYER_HIGH_SCORE = FOLDER_PLAYER_HIGH_SCORE + "game_HippieFall.json";
    }
}