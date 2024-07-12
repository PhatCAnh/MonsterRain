using System;
using System.Collections.Generic;
using System.Linq;
using _App.Scripts.Enums;
using _App.Scripts.Models;
using ArbanFramework;
using ArbanFramework.MVC;
using MR;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _App.Scripts.Controllers
{
	public class EnemyController : Controller<GameApp>
	{
		private List<EnemyView> _listEnemyInGame = new List<EnemyView>();

		private MapController mapController => Singleton<MapController>.instance;

		private void Awake()
		{
			Singleton<EnemyController>.Set(this);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			Singleton<EnemyController>.Unset(this);
		}

		private void FixedUpdate()
		{
			var time = Time.deltaTime;
			foreach (var enemy in _listEnemyInGame.ToList())
			{
				enemy.Fall(time);
				if (enemy.CheckTouchBase())
				{
					mapController.TouchEnemy(enemy);
				}
			}
		}

		public void SpawnEnemy(EnemyId id)
		{
			var data = GetDataEnemy(id);

			var view = data.Item1;

			var dataConfig = data.Item2;

			view.transform.position = new Vector3(Random.Range(-15, 15), 10);

			var model = new EnemyModel(dataConfig.moveSpeed, dataConfig.healthPoint);
			
			view.Init(model);
			
			_listEnemyInGame.Add(view);
		}

		public EnemyView GetNearestEnemy(Vector2 position)
		{
			EnemyView enemyView = null;
			float distance = float.MaxValue;
			
			foreach(var enemy in _listEnemyInGame)
			{
				var dis = GameLogic.CalculateDistance(position, enemy.transform.position);
				if(dis < distance)
				{
					enemyView = enemy;
					distance = dis;
				}
			}
			return enemyView;
		}

		public EnemyView CheckTouchEnemy(Vector3 pos, float size = 0)
		{
			foreach(var enemy in _listEnemyInGame.ToList())
			{
				if(Vector2.Distance(pos, enemy.transform.position) < enemy.size + size)
				{
					return enemy;
				}
			}
			return null;
		}

		public void EnemyDie(EnemyView enemy, bool selfDie = false)
		{
			if(!selfDie)
			{
				Instantiate(app.resourceManager.GetItemDrop(ItemDropId.Magazine), enemy.transform.position, Quaternion.identity);
				// map.model.monsterKilled++;
				// var gem = poolController.GetObject(ItemPrefab.GemExp, enemy.transform.position);
				// gem.GetComponent<DropItem>().Init(enemy.stat.exp.BaseValue, RandomDropItem());
			}

			_listEnemyInGame.Remove(enemy);
			Destroy(enemy.gameObject);
			// if(enemy.wave != null)
			// {
			// 	enemy.wave.monsterInWave.Remove(enemy);
			// }
		}

		private (EnemyView, EnemyDataConfig) GetDataEnemy(EnemyId id)
		{
			var prefab = Instantiate(app.resourceManager.GetEnemy(id)).GetComponent<EnemyView>();
			
			var dataConfig = app.configs.enemyDataConfig.GetData(id);

			return (prefab, dataConfig);
		}
	}
}