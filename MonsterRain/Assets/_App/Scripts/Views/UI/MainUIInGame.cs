using _App.Scripts.Models;
using ArbanFramework;
using ArbanFramework.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUIInGame : View<GameApp>, IPopup
{
    [SerializeField] private TextMeshProUGUI _txtNumberAmmo, _txtNumberMagazine, _txtHealthPoint;

    [SerializeField] private Image _imgGun;
    
    [SerializeField] private Animator _animGun;

    [SerializeField] private Slider _sldHealthBar;

    private void Awake()
    {
        Singleton<MainUIInGame>.Set(this);
    }
    
    protected override void OnDestroy()
    {
        base.OnDestroy();
        Singleton<MainUIInGame>.Unset(this);
    }
    
    protected override void OnViewInit()
    {
        base.OnViewInit();
        
        AddDataBinding("fieldCharacterModel-Shot", _txtNumberAmmo, (control, e) =>
        {
            var model = app.models.characterModel.mainGun;
            control.text = $"{model.currentAmmo} / {model.maxAmmo}";
            _txtNumberMagazine.text = $"{model.currentAmmoInMagazine}";
        }, new DataChangedValue(CharacterModel.dataChangedEvent, "mainGun-Shot", app.models.characterModel));
        
        /*var model = app.models.MapModel;

        _sldHealthBar.maxValue = model.maxHp;
        
        AddDataBinding("fieldMapModel-CurrentHp", _sldHealthBar, (control, e) =>
        {
            control.value = model.currentHp;
            _txtHealthPoint.text = $"{model.currentHp} / {model.maxHp}";
        }, new DataChangedValue(MapModel.dataChangedEvent, nameof(MapModel.currentHp), app.models.MapModel));*/
    }

    public void Shot()
    {
        _animGun.SetTrigger("Shot");
    }

    public void Open()
    {
        
    }
    public void Close()
    {
    }
}
