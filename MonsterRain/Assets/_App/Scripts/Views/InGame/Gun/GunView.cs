using System;
using _App.Scripts.Controllers;
using _App.Scripts.Enums;
using ArbanFramework;
using ArbanFramework.MVC;
using MR;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
namespace Views.Gun
{
	public class GunView : View<GameApp>
	{
		[SerializeField] private Transform _firePoint;

		[SerializeField] private BulletId _bulletId;

		[SerializeField] private float _bulletSpeed;

		[SerializeField] private float _stepTimeShot;

		public GunModel model => app.models.gunModel;

		private Cooldown _cdTimeShot;

		protected BulletController bulletController => Singleton<BulletController>.instance;

		protected override void OnViewInit()
		{
			base.OnViewInit();
			app.models.gunModel = new GunModel(15);
			_cdTimeShot = new Cooldown(_stepTimeShot);
		}

		private void Update()
		{
			var time = Time.deltaTime;
			
			_cdTimeShot.Update(time);
		}

		public virtual void Shot(Vector3 direction)
		{
			if(_cdTimeShot.isFinished)
			{
				bulletController.SpawnBullet(_bulletId, _firePoint.transform.position, direction, _bulletSpeed);
				model.currentAmmo -= 1;
				_cdTimeShot.Restart(_stepTimeShot);
			}
		}
	}
}