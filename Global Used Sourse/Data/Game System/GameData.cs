using Newtonsoft.Json;

namespace Global.Data.GameSystem;

public class GameData
{
    [JsonProperty]
    public float HighScore { get; set; } = 0;
    
    public GameData()
    {
        
    }
}