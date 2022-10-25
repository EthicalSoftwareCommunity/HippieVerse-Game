
using Global;
using Global.Constants;
using Godot;
using Newtonsoft.Json;

namespace HippieUniverse
{
	class SelectCharacter : InteractionController
	{
		private File _file;
		private Directory _directory;
		private string _pathToSaveFolder = C_SaveFolderFile.FOLDER_CHARACTER_SAVES;
		private string _pathToSaveFile = C_SaveFolderFile.FILE_CHARACTER;
		
		public override void _Ready()
		{
			_file = new File();
			_directory = new Directory();
			if (!_directory.DirExists(_pathToSaveFolder))
			{
				_directory.MakeDir(_pathToSaveFolder);
			}
		}

		public override void HoldTouchInteract()
		{
			base.HoldTouchInteract();
			SaveChosenCharacter();
			GoToMainScene();
		}

		private void SaveChosenCharacter()
		{
			SaveCharacterData character = new SaveCharacterData
			{
				name = GetParent().Name
			};
			_file.Open(_pathToSaveFile, File.ModeFlags.Write);
			_file.StoreString(JsonConvert.SerializeObject(character));
			_file.Close();
		}

		private void GoToMainScene()
		{
			GetTree().ChangeScene(C_ScenesPath.MAIN_SPHERE);
		}

	}

	public class SaveCharacterData
	{
		public string name;
	}

}

