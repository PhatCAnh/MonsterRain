using _App.Scripts.Enums;
using MR;
using UnityEngine;
namespace _App.Scripts.Datas
{
	public class GunData
	{
		public GunId id;
		public GameObject prefab;
		public GunDataConfig dataConfig;

		public GunData(GunId id, GameObject prefab, GunDataConfig dataConfig)
		{
			this.id = id;
			this.prefab = prefab;
			this.dataConfig = dataConfig;
		}
	}
	
	public class GunUsedData
	{
		public GunId id;
		public int currentAmmo;
		public int maxAmmo;

		public GunUsedData(GunId id, int maxAmmo)
		{
			this.id = id;
			this.maxAmmo = maxAmmo;
			currentAmmo = this.maxAmmo;
		}
	}
}