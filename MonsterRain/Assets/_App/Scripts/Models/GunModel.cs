using ArbanFramework.MVC;
namespace MR
{
	public class GunModel : Model<GameApp>
	{
		public static EventTypeBase dataChangedEvent = new EventTypeBase(nameof(GunModel) + ".dataChanged");

		public GunModel(EventTypeBase eventType) : base(dataChangedEvent)
		{
		}
		
		public GunModel() : base(dataChangedEvent)
		{
		}

		private int _maxAmmo;
		private int _currentAmmo;

		public GunModel(int maxAmmo) : base(dataChangedEvent)
		{
			_maxAmmo = maxAmmo;
			_currentAmmo = maxAmmo;
		}
		public int maxAmmo
		{
			get => _maxAmmo;
			set
			{
				if (!maxAmmo.Equals(value))
				{
					_maxAmmo = value;
					RaiseDataChanged(nameof(maxAmmo));
				}
			}
		}
		
		public int currentAmmo
		{
			get => _currentAmmo;
			set
			{
				if (!currentAmmo.Equals(value))
				{
					_currentAmmo = value;
					RaiseDataChanged(nameof(currentAmmo));
				}
			}
		}
	}
}