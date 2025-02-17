using _App.Scripts.Controllers;
using _App.Scripts.Enums;
using _App.Scripts.Models;
using ArbanFramework;
using ArbanFramework.StateMachine;
using MR;
using MR.CharacterState;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Views.Gun;

public class Character : ObjectRPG
{
	public Rigidbody2D myRigid;

	public Animator animator;

	public CharacterModel model => app.models.characterModel;

	public CharacterStat stat { get; private set; }

	public Transform skin;

	public float size;

	[SerializeField] private Transform _groundCheck;

	[SerializeField] private EnemyView _target;

	public LayerMask whatIsGround;

	public GunView gun;
	
	[SerializeField] private float _gunDistance = 1.2f;

	public bool isGrounded;
	private bool _facingRight = true;
	public bool isMove => _stateMachine.currentState == _moveSM;

	public bool isHaveAmmo => model.mainGun.currentAmmo > 0;

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

	private Vector3 _directionTarget;

	private GameController _gameController => Singleton<GameController>.instance;
	private EnemyController _enemyController => Singleton<EnemyController>.instance;

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
		
		gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(_directionTarget.y, _directionTarget.x) * Mathf.Rad2Deg));

		float angle = Mathf.Atan2(_directionTarget.y, _directionTarget.x) * Mathf.Rad2Deg;
		gun.transform.position = transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(_gunDistance, 0, 0);
	}

	public void Init(CharacterModel model, GunView gun)
	{
		app.models.characterModel = model;
		this.gun = gun;
	}

	private void Update()
	{
		// if(_gameController.isStop) return;
		var time = Time.deltaTime;
		_stateMachine.currentState.LogicUpdate(time);
		// if(isDoingSomething) return;
		
		_target = _enemyController.GetNearestEnemy(transform.position);
		if (_target is not null)
		{
			_directionTarget = _target.transform.position - transform.position;

			gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(_directionTarget.y, _directionTarget.x) * Mathf.Rad2Deg));

			float angle = Mathf.Atan2(_directionTarget.y, _directionTarget.x) * Mathf.Rad2Deg;
			gun.transform.position = transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(_gunDistance, 0, 0);
			
			if(Input.GetMouseButton(0))
			{
				Shot();
			}
		}

		HandlePhysicUpdate();
	}

	private void Shot()
	{
		gun.Shot(_directionTarget);
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
	}

	private void CheckGround()
	{
		isGrounded = Physics2D.OverlapCircle(_groundCheck.position, 0.3f, whatIsGround);
	}
	
	protected void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, size);
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