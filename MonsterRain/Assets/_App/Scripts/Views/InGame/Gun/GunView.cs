using System;
using _App.Scripts.Controllers;
using _App.Scripts.Datas;
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
		
		private GunUsedData _gunUsedData;

		private GunDataConfig _dataConfig;

		private Cooldown _cdTimeShot;

		private Character _origin;

		protected BulletController bulletController => Singleton<BulletController>.instance;

		protected override void OnViewInit()
		{
			base.OnViewInit();
			_cdTimeShot = new Cooldown(_dataConfig.stepTimeShot);
		}

		public void Init(GunDataConfig dataConfig, Character character)
		{
			_origin = character;
			_dataConfig = dataConfig;
			_gunUsedData = character.model.mainGun;
			transform.SetParent(character.transform);
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
				if(!_origin.model.Shot()) return;
				bulletController.SpawnBullet(_bulletId, _firePoint.transform.position, direction, _dataConfig.bulletSpeed, _dataConfig.atk);
				_cdTimeShot.Restart(_dataConfig.stepTimeShot);
			}
		}
	}
}