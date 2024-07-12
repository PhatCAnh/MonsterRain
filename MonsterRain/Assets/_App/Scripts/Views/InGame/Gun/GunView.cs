using System;
using _App.Scripts.Controllers;
using _App.Scripts.Datas;
using _App.Scripts.Enums;
using _App.Scripts.Views;
using ArbanFramework;
using ArbanFramework.MVC;
using ArbanFramework.StateMachine;
using MR;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Views.Gun.State_Gun;
namespace Views.Gun
{
	public class GunView : View<GameApp>
	{
		[SerializeField] private Animator _animSkin;
		
		[SerializeField] private Transform _firePoint;

		[SerializeField] private BulletId _bulletId;

		private GunUsedData _gunUsedData;

		public GunDataConfig dataConfig;

		public Cooldown cdTimeShot;
		public Cooldown cdTimeReload;

		public Character origin;

		private StateMachine _stateMachine;
		private GunIdle _idleSm;
		private GunReload _reloadSm;
		private GunStepTime _stepTimeSm;

		protected BulletController bulletController => Singleton<BulletController>.instance;
		protected CharacterController characterController => Singleton<CharacterController>.instance;

		protected override void OnViewInit()
		{
			base.OnViewInit();
			cdTimeShot = new Cooldown();
			cdTimeReload = new Cooldown();

			if(_stateMachine == null)
			{
				_stateMachine = new StateMachine();
				_idleSm = new GunIdle(this, _stateMachine);
				_reloadSm = new GunReload(this, _stateMachine);
				_stepTimeSm = new GunStepTime(this, _stateMachine);
				_stateMachine.Init(_idleSm);
			}
			else
			{
				IdleState();
			}
		}

		public void Init(GunDataConfig dataConfig)
		{
			origin = characterController.character;
			this.dataConfig = dataConfig;
			_gunUsedData = origin.model.mainGun;
			transform.SetParent(origin.transform);
		}

		private void Update()
		{
			var time = Time.deltaTime;
			_stateMachine.currentState.LogicUpdate(time);
		}

		public virtual void Shot(Vector3 direction)
		{
			if(_stateMachine.currentState != _idleSm) return;
			if(!origin.model.Shot())
			{
				if(origin.model.mainGun.currentAmmo > 0)
				{
					ReloadState();
				}
				return;
			}
			bulletController.SpawnBullet(_bulletId, _firePoint.transform.position, direction, dataConfig.bulletSpeed, dataConfig.atk);
			_animSkin.SetTrigger("Shot");
			Singleton<MainUIInGame>.instance.Shot();
			StepState();
		}

		#region State Machine Method

		public void IdleState() => _stateMachine.ChangeState(_idleSm);

		public void StepState()
		{
			_stateMachine.ChangeState(_stepTimeSm);
		}

		public void ReloadState()
		{
			_stateMachine.ChangeState(_reloadSm);
		}

		#endregion
	}
}