using ArbanFramework.StateMachine;
namespace Views.Gun.State_Gun
{
	public class GunReload : State<GunView>
	{
		public GunReload(GunView agent, StateMachine stateMachine) : base(agent, stateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();
			if(!agent.origin.isHaveAmmo)
			{
				agent.IdleState();
				return;
			}
			agent.cdTimeReload.Restart(agent.dataConfig.timeLoad);
		}

		public override void LogicUpdate(float deltaTime)
		{
			base.LogicUpdate(deltaTime);
			var cd = agent.cdTimeReload;
			cd.Update(deltaTime);
			if(cd.isFinished)
			{
				agent.IdleState();
			}
		}

		public override void Exit()
		{
			base.Exit();
			agent.origin.model.ReloadAmmo();
		}
	}
}