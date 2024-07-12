using _App.Scripts.Models;
using ArbanFramework.MVC;

public class ModelManager : ModelManagerBase
{
	public CharacterModel characterModel;
	//Models
	public SettingModel settingModel;
	
	public EnemyModel enemyModel;
	
	public MapModel MapModel;
	public void Init()
	{
		//Register
		RegisterModel(out settingModel);
		RegisterModel(out characterModel);
		RegisterModel(out enemyModel);
		RegisterModel(out MapModel);
		LoadData();
	}
}