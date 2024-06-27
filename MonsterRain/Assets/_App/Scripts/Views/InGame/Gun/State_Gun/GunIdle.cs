using ArbanFramework.StateMachine;
using Views.Gun;
namespace _App.Scripts.Views
{
	public class GunIdle: State<GunView>
	{
		public GunIdle(GunView agent, StateMachine stateMachine) : base(agent, stateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();
		}
	}
}