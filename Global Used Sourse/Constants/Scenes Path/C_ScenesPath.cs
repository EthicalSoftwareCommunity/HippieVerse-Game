namespace Global.Constants
{
    public class C_ScenesPath
    {
        //Scenes;
        public const string MAIN_SPHERE = "res://HippieUniverse/Sourse/MainSphere/MainSphere.tscn";
        public const string CHOOSE_CHARACTER = "res://HippieUniverse/Sourse/ChooseCharacter/ChooseCharacter.tscn";
        public const string HIPPIE_FALL = "res://HippieFall/Sourse/HippieFall.tscn";
        public const string HIPPIE_GARAGE = "res://HippieUniverse/Sourse/HippieGarage/HippieGarage.tscn";
        
        public enum Scenes
        {
            HippieFall,
            ChooseCharacter,
            MainScene,
            HippieGarage
        }
        public static string GetScenePathByEnum(Scenes scene)
        {
            switch (scene)
            {
                case Scenes.HippieFall: return C_ScenesPath.HIPPIE_FALL;
                case Scenes.ChooseCharacter:return C_ScenesPath.CHOOSE_CHARACTER;
                case Scenes.MainScene:return C_ScenesPath.MAIN_SPHERE;
                case Scenes.HippieGarage:return C_ScenesPath.HIPPIE_GARAGE;
            }
            return "";
        }
    }
}