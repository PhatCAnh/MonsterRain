using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Models;
using ArbanFramework.MVC;
using MR;
using TMPro;
using UnityEngine;

public class MainUIInGame : View<GameApp>, IPopup
{
    [SerializeField] private TextMeshProUGUI _txtNumberAmmo;

    protected override void OnViewInit()
    {
        base.OnViewInit();
        
        AddDataBinding("fieldCharacterModel-Shot", _txtNumberAmmo, (control, e) =>
        {
            var model = app.models.characterModel.mainGun;
            control.text = $"{model.currentAmmo} / {model.maxAmmo}";
        }, new DataChangedValue(CharacterModel.dataChangedEvent, "mainGun-Shot", app.models.characterModel));
    }

    public void Open()
    {
        
    }
    public void Close()
    {
    }
}
