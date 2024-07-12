using System;
using System.Collections.Generic;
using System.Linq;
using _App.Scripts.Enums;
using ArbanFramework;
using ArbanFramework.MVC;
using Unity.Mathematics;
using UnityEngine;
namespace _App.Scripts.Controllers
{
	public class BulletController : Controller<GameApp>
	{
		private List<Bullet> _listBulletInGame;
		
		private void Awake()
		{
			Singleton<BulletController>.Set(this);
		}
		protected override void OnDestroy()
		{
			base.OnDestroy();
			Singleton<BulletController>.Unset(this);
		}

		private void Start()
		{
			_listBulletInGame = new List<Bullet>();
		}

		private void Update()
		{
			var time = Time.deltaTime;
			foreach (var bullet in _listBulletInGame.ToList())
			{
				bullet.Moving(time);
				if (bullet.CheckTouch())
				{
					RemoveBullet(bullet);
				}
			}
		}

		public void SpawnBullet(BulletId bulletId, Vector2 firePoint, Vector3 direction, float speed, int atk)
		{
			var bullet = Instantiate(app.resourceManager.GetBullet(bulletId), firePoint, quaternion.identity)
				.GetComponent<Bullet>();
			bullet.Init(direction, speed, atk);
			_listBulletInGame.Add(bullet);
		}

		public void RemoveBullet(Bullet bullet)
		{
			_listBulletInGame.Remove(bullet);
			Destroy(bullet.gameObject);
		}
	}
}