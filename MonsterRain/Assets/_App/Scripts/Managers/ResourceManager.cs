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
    
    [SerializeField] private GameObject _heavyBullet;
    
    private Dictionary<BulletId, GameObject> _poolBulletDic;
    
    
    [SerializeField] private GameObject _cheeringHand;
    
    [SerializeField] private GameObject _HeavyGun;
    
    private Dictionary<GunId, GameObject> _poolGunDic;


    [SerializeField] private GameObject _sandEgg;
    
    private Dictionary<EnemyId, GameObject> _enemyDic;
    
    
    [SerializeField] private GameObject _magazineItemDrop;
    
    private Dictionary<ItemDropId, GameObject> _itemDropDic;
    

    [SerializeField] private GameObject _mainUIInGame;
    
    [SerializeField] private GameObject _mainUIOutGame;

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

        RegisterPopup(PopupType.MainInGame, _mainUIInGame);
        RegisterPopup(PopupType.MainOutGame, _mainUIOutGame);
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
        
        _poolBulletDic = new Dictionary<BulletId, GameObject>()
        {
            {BulletId.NormalBullet, _normalBullet },
            {BulletId.HeavyBullet, _heavyBullet },
        };
        
        _poolGunDic = new Dictionary<GunId, GameObject>()
        {
            {GunId.CheeringHand, _cheeringHand },
            {GunId.Heavy, _HeavyGun },
        };
        
        _itemDropDic = new Dictionary<ItemDropId, GameObject>()
        {
            {ItemDropId.Magazine, _magazineItemDrop },
        };
        
        _itemDropDic = new Dictionary<ItemDropId, GameObject>()
        {
            {ItemDropId.Magazine, _magazineItemDrop },
        };
        
        _enemyDic = new Dictionary<EnemyId, GameObject>()
        {
            {EnemyId.SandEgg, _sandEgg },
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
        return _poolBulletDic[bulletId];
    }
    
    public GameObject GetGun(GunId gunId)
    {
        return _poolGunDic[gunId];
    } 
    
    public GameObject GetItemDrop(ItemDropId itemDropId)
    {
        return _itemDropDic[itemDropId];
    }  
    
    public GameObject GetEnemy(EnemyId enemyId)
    {
        return _enemyDic[enemyId];
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

