using ArbanFramework;
using ArbanFramework.MVC;
using MR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _App.Scripts.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class ResourceManager : UIManagerBase<PopupType>
{
    [SerializeField] private GameObject _mainCharacter;
    
    private Dictionary<CharacterId, GameObject> _characterDic;

    [SerializeField] private GameObject _mapFall;
    
    private Dictionary<MapId, GameObject> _mapDic;
    
    [SerializeField] private GameObject _normalBullet;
    
    private Dictionary<BulletId, GameObject> _poolItemDic;
    
    [SerializeField] private GameObject _cheeringHand;
    
    private Dictionary<GunId, GameObject> _poolGun;

    //[SerializeField] private GameObject mainUIPopupPrefab;

    private void Awake()
    {
        Singleton<ResourceManager>.Set(this);
        Init();
    }

    private void OnDestroy()
    {
        Singleton<ResourceManager>.Unset(this);
    }

    public void Init()
    {
        InitItemDic();

        //RegisterPopup(PopupType.Main, mainUIPopupPrefab);
    }

    private void InitItemDic()
    {
        _characterDic = new Dictionary<CharacterId, GameObject>()
        {
            {CharacterId.Main, _mainCharacter },
        };
        
        _mapDic = new Dictionary<MapId, GameObject>()
        {
            {MapId.Fall, _mapFall },
        };
        
        _poolItemDic = new Dictionary<BulletId, GameObject>()
        {
            {BulletId.NormalBullet, _normalBullet },
        };
        
        _poolGun = new Dictionary<GunId, GameObject>()
        {
            {GunId.CheeringHand, _cheeringHand },
        };
    }    

    public GameObject GetCharacter(CharacterId characterId)
    {
        return _characterDic[characterId];
    }   
    
    public GameObject GetMap(MapId mapId)
    {
        return _mapDic[mapId];
    }  
    
    public GameObject GetBullet(BulletId bulletId)
    {
        return _poolItemDic[bulletId];
    }
    
    public GameObject GetGun(GunId gunId)
    {
        return _poolGun[gunId];
    }  

    public override GameObject ShowPopup(PopupType type, Action<GameObject> onInit = null)
    {
        var popupGo = base.ShowPopup(type, onInit);
        if (!popupGo)
            return null;
        popupGo.GetOrAddComponent<GraphicRaycaster>();
        return popupGo;
    }
}

