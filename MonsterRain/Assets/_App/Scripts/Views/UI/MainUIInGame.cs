using System.Collections;
using System.Collections.Generic;
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
        
        AddDataBinding("fieldDataPlayerModel-limitQuantityItemEquip", _txtNumberAmmo, (control, e) =>
        {
            var data = app.models.gunModel;
            control.text = $"{data.currentAmmo} / {data.maxAmmo}";
        }, new DataChangedValue(GunModel.dataChangedEvent, nameof(GunModel.currentAmmo), app.models.gunModel));
    }
    public void Open()
    {
        
    }
    public void Close()
    {
    }
}
