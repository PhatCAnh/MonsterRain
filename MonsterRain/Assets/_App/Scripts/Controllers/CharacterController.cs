using System;
using ArbanFramework;
using ArbanFramework.MVC;
using MR;
using UnityEngine;
using Cooldown = ArbanFramework.Cooldown;

public class CharacterController : Controller<GameApp>
{
	public Character character => _gameController.character;
	private GameController _gameController => Singleton<GameController>.instance;

	private void Awake()
	{
		Singleton<CharacterController>.Set(this);
	}

	private void Update()
	{
		if(character == null || _gameController.isStop) return;

		ControlMove(Time.deltaTime);
	}
	private void ControlMove(float deltaTime)
	{
		var posX = Input.GetAxisRaw("Horizontal");
		var position = new Vector2(posX, 0);
		character.Controlled(deltaTime, position);
	}
}