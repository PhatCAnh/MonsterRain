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
			if(!agent.origin.isHaveAmmo) agent.IdleState();
		}

		public override void Exit()
		{
			base.Exit();
			agent.origin.model.ReloadAmmo();
		}
	}
}