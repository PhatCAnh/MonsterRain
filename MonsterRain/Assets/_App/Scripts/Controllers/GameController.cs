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
using _App.Scripts.Enums;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using UnityEngine.Serialization;

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
		Instantiate(app.resourceManager.GetMap(MapId.Fall));
		characterController.character = Instantiate(app.resourceManager.GetCharacter(CharacterId.Main)).GetComponent<Character>();
		characterController.character.Init(new CharacterModel(2));

		app.resourceManager.ShowPopup(PopupType.Main);
	}
	
	public void StartGame()
	{
		ChangeScene(GameConst.nameScene_Game, LoadMap);
	}
}