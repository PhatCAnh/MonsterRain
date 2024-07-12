using ArbanFramework;
using ArbanFramework.MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MR;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Threading;
using _App.Scripts;
using _App.Scripts.Controllers;
using _App.Scripts.Datas;
using _App.Scripts.Enums;
using _App.Scripts.Models;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using UnityEngine.Serialization;
using Views.Gun;

public class GameController : Controller<GameApp>
{
	[SerializeField] private Character _characterPrefab;
	public bool isStop => isEndGame || isStopGame;

	public bool isStopGame;

	public bool isEndGame;

	public CharacterController characterController => Singleton<CharacterController>.instance;

	private void Awake()
	{
		Singleton<GameController>.Set(this);
	}
	protected override void OnDestroy()
	{
		base.OnDestroy();
		Singleton<GameController>.Unset(this);
	}

	private void Start()
	{
		//StartGame();
	}

	public void ChangeScene(string nameScene, [CanBeNull] Action callback)
	{
		var load = SceneManager.LoadSceneAsync(nameScene, LoadSceneMode.Single);
		load.completed += o =>
		{
			callback?.Invoke();
		};
	}

	public void LoadMap()
	{
		app.resourceManager.ShowPopup(PopupType.Main);

		var character = Instantiate(app.resourceManager.GetCharacter(CharacterId.Main)).GetComponent<Character>();

		var gunData = GetDataGun(GunId.Heavy);
		
		var gun = Instantiate(gunData.prefab).GetComponent<GunView>();
		
		character.Init(new CharacterModel(2, new GunUsedData(gunData.id, gunData.dataConfig.ammoTotal, gunData.dataConfig.magazine)), gun);
		
		characterController.character = character;
		
		gun.Init(gunData.dataConfig);
		
		Singleton<MapController>.instance.SpawnMap(MapId.Fall);
	}
	
	public void StartGame()
	{
		ChangeScene(GameConst.nameScene_Game, LoadMap);
	}
	
	//chuyen qua gunController
	public GunData GetDataGun(GunId id)
	{
		var go = app.resourceManager.GetGun(id);
		var data = app.configs.gunDataConfig.GetData(id);

		return new GunData(id, go, data);
	}

	public bool CheckDistancePlayer(Vector2 pos, float size)
	{
		var character = characterController.character;

		return GameLogic.CalculateDistance(character.transform.position, pos) < (size + character.size) * (size + character.size);
	}
}