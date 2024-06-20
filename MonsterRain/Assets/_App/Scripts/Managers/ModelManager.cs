using ArbanFramework.MVC;
using MR;

public class ModelManager : ModelManagerBase
{
	public CharacterModel characterModel;
	//Models
	public SettingModel settingModel;

	public GunModel gunModel;
	public void Init()
	{
		//Register
		RegisterModel(out settingModel);
		RegisterModel(out characterModel);
		RegisterModel(out gunModel);
		LoadData();
	}
}