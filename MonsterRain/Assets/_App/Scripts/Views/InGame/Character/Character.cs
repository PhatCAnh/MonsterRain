using System;
using ArbanFramework;
using ArbanFramework.Config;
using ArbanFramework.MVC;
using MR.CharacterState;
using UnityEngine;
using UnityEngine.Serialization;
using StateMachine = ArbanFramework.StateMachine.StateMachine;

namespace MR
{
	public class Character : ObjectRPG
	{
		public Rigidbody2D myRigid;

		public Animator animator;
		
		public CharacterModel model { get; private set; }

		public CharacterStat stat { get; private set; }

		[SerializeField] private Transform _skin;
		
		[SerializeField] private Transform _groundCheck;
		
		[SerializeField] private Vector3 _target => new Vector3(10, 10, 0);
		
		public LayerMask whatIsGround;
		
		[SerializeField] private Transform _gun;
		[SerializeField] private float _gunDistance = 1.2f;
		
		public bool isGrounded;
		private bool _facingRight = true;
		public bool isMove => _stateMachine.currentState == _moveSM;

		public float speedMul { get; set; } = 1;
		public Vector2 idleDirection { get; private set; } = Vector2.down;

		public Vector2 moveDirection
		{
			get => _direction;
			set {
				if(value != Vector2.zero)
				{
					idleDirection = value;
				}
				_direction = value;
			}
		}

		private Vector2 _direction = Vector2.zero;

		private StateMachine _stateMachine;
		private CharacterIdle _idleSM;
		private CharacterMove _moveSM;

		private GameController _gameController => Singleton<GameController>.instance;

		protected override void OnViewInit()
		{
			base.OnViewInit();
			if(_stateMachine == null)
			{
				_stateMachine = new StateMachine();
				_idleSM = new CharacterIdle(this, _stateMachine);
				_moveSM = new CharacterMove(this, _stateMachine);
				_stateMachine.Init(_idleSM);
			}
			else
			{
				IdleState();
			}
		}

		public void Init(CharacterModel model)
		{
			this.model = model;
		}

		private void Update()
		{
			// if(_gameController.isStop) return;
			var time = Time.deltaTime;
			_stateMachine.currentState.LogicUpdate(time);
			// if(isDoingSomething) return;
			
			Vector3 direction = _target - transform.position;

			_gun.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			_gun.position = transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(_gunDistance, 0, 0);

			HandlePhysicUpdate();
		}

		private void FixedUpdate()
		{
			if(_gameController.isStop) return;

			CheckGround();

			_stateMachine.currentState.PhysicUpdate(Time.fixedTime);
		}

		public void Controlled(float deltaTime, Vector2 moveForce)
		{
			moveDirection = moveForce;
		}

		private void HandlePhysicUpdate()
		{

			if(moveDirection == Vector2.zero)
				IdleState();
			else
				MoveState();

			SetAnimation(moveDirection, idleDirection);
		}

		private void SetAnimation(Vector2 dir, Vector2 idleDirection)
		{
			//animator.SetFloat("SpeedMul", speedMul);
			var speed = dir.normalized.magnitude;
			animator.SetFloat("Speed", speed);
			// animator.SetFloat("Horizontal", idleDirection.x);
			// animator.SetFloat("Vertical", idleDirection.y);
			Debug.Log(dir.x);
			_skin.transform.localScale = new Vector2(dir.x > 0 ? 1 : -1, 1);
			// if(_facingRight && dir.x < 0)
			// {
			// 	_skin.transform.localScale = new Vector2(-1, 1);
			// 	_facingRight = false;
			// }
			// else if(!_facingRight && dir.x > 0)
			// {
			// 	_skin.transform.localScale = new Vector2(1, 1);
			// 	_facingRight = true;
			// }
		}

		private void CheckGround()
		{
			isGrounded = Physics2D.OverlapCircle(_groundCheck.position, 0.3f, whatIsGround);
		}

		#region State Machine Method

		public void IdleState() => _stateMachine.ChangeState(_idleSM);

		public void MoveState()
		{
			if(isMove) return;
			_stateMachine.ChangeState(_moveSM);
		}

		#endregion
	}
}