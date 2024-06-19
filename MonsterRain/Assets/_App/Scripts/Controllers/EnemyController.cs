using System.Collections.Generic;
using System.Linq;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
namespace _App.Scripts.Controllers
{
	public class EnemyController : Controller<GameApp>
	{
		[SerializeField] private GameObject _enemyPrefab;
		
		[SerializeField] private Sprite[] targetSprites;
		
		private List<EnemyView> _listEnemyInGame = new List<EnemyView>();

		private void Awake()
		{
			Singleton<EnemyController>.Set(this);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			Singleton<EnemyController>.Unset(this);
		}

		public EnemyView SpawnEnemy()
		{
			var newTarget = Instantiate(_enemyPrefab);

			float randomX = Random.Range(-15, 15);

			newTarget.transform.position = new Vector3(randomX, transform.position.y);
			int randomIndexSprite = Random.Range(0, targetSprites.Length);
			newTarget.GetComponent<SpriteRenderer>().sprite = targetSprites[randomIndexSprite];
			var enemy = newTarget.GetComponent<EnemyView>();
			_listEnemyInGame.Add(enemy);
			
			return enemy;
		}

		public EnemyView CheckTouchEnemy(Vector3 pos, float size = 0)
		{
			foreach(var enemy in _listEnemyInGame.ToList())
			{
				if(Vector2.Distance(pos, enemy.transform.position) < enemy.size + size)
				{
					Debug.Log("touched");
					return enemy;
				}
			}
			return null;
		}

		public void EnemyDie(EnemyView enemy, bool selfDie = false)
		{
			if(!selfDie)
			{
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
	}
}