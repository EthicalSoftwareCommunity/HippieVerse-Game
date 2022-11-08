using Godot;
using Global.Constants;
using Global.Data.Reward;
using HippieUniverse;
using Newtonsoft.Json;

namespace HappieUniverse
{
	public class MainSphere : Spatial
	{
		private GameInterface Interface;
		private string _pathToCharacterSave = C_SaveFolderFile.FILE_CHARACTER;
		private Node _character;
		public override void _Ready()
		{
			_character = GetNode("World/Character");
			Interface = GetNode<GameInterface>("Interface");
			LoadCharacter();
			LoadRewards();
		}

		private void LoadCharacter()
		{
			File file = new File();
			if(file.FileExists(_pathToCharacterSave) == false)
			{
				file.Open(_pathToCharacterSave, File.ModeFlags.Write);
			}
			var error = file.Open(_pathToCharacterSave, File.ModeFlags.ReadWrite);
			if (error == Error.Ok)
			{
				SaveCharacterData saveData = JsonConvert.DeserializeObject<SaveCharacterData>(file.GetAsText());
				if (saveData != null)
				{
					PackedScene loadCharacter = (PackedScene)ResourceLoader.Load(C_ObjectPath.CHARACTER_MODELS + saveData.name + ".tscn");
					if (loadCharacter != null)
					{
						StaticBody character = (StaticBody)loadCharacter.Instance();
						_character.AddChild(character);
						character.Translation = new Vector3(0, 0, 0);
					}
				}
			}
			file.Close();
		}

		private void LoadRewards()
		{
			RewardController rewardController = new RewardController();
			Interface.RewardRenderer.Render(rewardController.RewardSaveLoadSystem.LoadRewards());
		}
	}
}
