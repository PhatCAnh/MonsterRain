using ArbanFramework.StateMachine;
using UnityEngine;

namespace MR.CharacterState
{
	public class CharacterMove : State<Character>
	{
		public CharacterMove(Character agent, StateMachine stateMachine) : base( agent, stateMachine )
		{
		}

		public override void PhysicUpdate(float fixedDeltaTime)
		{
			base.PhysicUpdate(fixedDeltaTime);
			var directionUnit = agent.moveDirection.normalized;
			Move(directionUnit, Time.fixedDeltaTime);
			agent.animator.SetFloat("Speed", directionUnit.magnitude);
		}

		private void Move(Vector2 dir, float deltaTime)
		{
			//fix it
			var movement = agent.model.moveSpeed  * GameConst.MOVE_SPEED_ANIMATION_RATIO * deltaTime * agent.speedMul * dir;
			var newPosition = agent.myRigid.position + movement;
			agent.myRigid.MovePosition(newPosition);
			agent.skin.transform.localScale = new Vector2(dir.x > 0 ? 1 : -1, 1);
		}
	}
}