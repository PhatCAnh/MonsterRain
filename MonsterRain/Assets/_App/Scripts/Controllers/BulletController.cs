using _App.Scripts.Enums;
using ArbanFramework;
using ArbanFramework.MVC;
using Unity.Mathematics;
using UnityEngine;
namespace _App.Scripts.Controllers
{
	public class BulletController : Controller<GameApp>
	{
		private void Awake()
		{
			Singleton<BulletController>.Set(this);
		}
		protected override void OnDestroy()
		{
			base.OnDestroy();
			Singleton<BulletController>.Unset(this);
		}

		public void SpawnBullet(BulletId bulletId, Vector2 firePoint, Vector3 direction, float speed, int atk)
		{
			Instantiate(app.resourceManager.GetBullet(bulletId), firePoint, quaternion.identity)
				.GetComponent<Bullet>().Init(direction, speed, atk);
		}
	}
}