using ArbanFramework.StateMachine;
namespace Views.Gun.State_Gun
{
	public class GunStepTime : State<GunView>
	{
		public GunStepTime(GunView agent, StateMachine stateMachine) : base(agent, stateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();
			agent.cdTimeShot.Restart(agent.dataConfig.stepTimeShot);
		}

		public override void LogicUpdate(float deltaTime)
		{
			base.LogicUpdate(deltaTime);
			agent.cdTimeShot.Update(deltaTime);
			if(agent.cdTimeShot.isFinished)
			{
				agent.IdleState();
			}
		}
	}
}