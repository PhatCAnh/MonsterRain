using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
using UnityEngine.UI;

public class MainUIOutGame : View<GameApp>, IPopup
{
    [SerializeField] private Button _btnPlay;

    private GameController gameController => Singleton<GameController>.instance;

    protected override void OnViewInit()
    {
        base.OnViewInit();
        _btnPlay.onClick.AddListener(OnClickBtnPlay);
    }

    private void OnClickBtnPlay()
    {
        gameController.StartGame();
        Close();
    }

    public void Open()
    {
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
