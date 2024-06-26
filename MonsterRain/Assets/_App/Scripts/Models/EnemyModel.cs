using ArbanFramework.MVC;
namespace _App.Scripts.Models
{
	public class EnemyModel : Model<GameApp>
	{
		public static EventTypeBase dataChangedEvent = new EventTypeBase(nameof(EnemyModel) + ".dataChanged");
		public EnemyModel() : base(dataChangedEvent)
		{
		}

		public EnemyModel(float moveSpeed, int healthPoint) : base(dataChangedEvent)
		{
			this.moveSpeed = moveSpeed;
			this.healthPoint = healthPoint;
			this.maxPoint = healthPoint;
		}

		private float _moveSpeed;
		
		private int _healthPoint;
		
		private int _maxPoint;


		public float moveSpeed
		{
			get => _moveSpeed;
			set
			{
				if (!moveSpeed.Equals(value))
				{
					_moveSpeed = value;
					RaiseDataChanged(nameof(moveSpeed));
				}
			}
		}
		

		public int healthPoint
		{
			get => _healthPoint;
			set
			{
				if (!healthPoint.Equals(value))
				{
					_healthPoint = value;
					RaiseDataChanged(nameof(healthPoint));
				}
			}
		}
		
		public int maxPoint
		{
			get => _maxPoint;
			set
			{
				if (!maxPoint.Equals(value))
				{
					_maxPoint = value;
				}
			}
		}
	}
}