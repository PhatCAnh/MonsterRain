using System;
using _App.Scripts.Controllers;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
namespace MR.CharacterState
{
	public class InGameView : View<GameApp>
	{
		[SerializeField] private float _timeCooldownSpawnEnemy;
		
		private Cooldown _cdSpawnEnemy;

		private EnemyController enemyController => Singleton<EnemyController>.instance;

		protected override void OnViewInit()
		{
			base.OnViewInit();
			_cdSpawnEnemy = new Cooldown();
			_cdSpawnEnemy.Restart(_timeCooldownSpawnEnemy);
		}

		private void Update()
		{
			var time = Time.deltaTime;
			HandleLogicUpdate(time);
		}

		private void HandleLogicUpdate(float deltaTime)
		{
			_cdSpawnEnemy.Update(deltaTime);
			if(_cdSpawnEnemy.isFinished)
			{
				_cdSpawnEnemy.Restart(_timeCooldownSpawnEnemy);
				enemyController.SpawnEnemy();
			}
		}
	}
}