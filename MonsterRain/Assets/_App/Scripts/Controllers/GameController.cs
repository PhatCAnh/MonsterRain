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
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using UnityEngine.Serialization;

public class GameController : Controller<GameApp>
{
	[SerializeField] private Character _characterPrefab;
	public bool isStop => isEndGame || isStopGame;

	public bool isStopGame;

	public bool isEndGame;

	public Character character;

	private void Awake()
	{
		Singleton<GameController>.Set(this);
	}
	protected override void OnDestroy()
	{
		base.OnDestroy();
		Singleton<GameController>.Unset(this);
	}
	
	
}