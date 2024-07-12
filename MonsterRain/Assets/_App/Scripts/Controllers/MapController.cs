using System.Collections.Generic;
using _App.Scripts.Enums;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;

namespace _App.Scripts.Controllers
{
    public class MapController: Controller<GameApp>
    {
        [SerializeField] private float _timeCooldownSpawnEnemy;
		
        private Cooldown _cdSpawnEnemy;
        
        private EnemyController enemyController => Singleton<EnemyController>.instance;

        private MapView _map;
        
        private void Awake()
        {
            Singleton<MapController>.Set(this);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            Singleton<MapController>.Unset(this);
        }

        private void Start()
        {
            _cdSpawnEnemy = new Cooldown();
            _cdSpawnEnemy.Restart(_timeCooldownSpawnEnemy);
        }

        private void Update()
        {
            var time = Time.deltaTime;
            HandleLogicUpdate(time);
        }

        private void HandleLogicUpdate(float deltaTime)
        {
            _cdSpawnEnemy.Update(deltaTime);
            if(_cdSpawnEnemy.isFinished)
            {
                _cdSpawnEnemy.Restart(_timeCooldownSpawnEnemy);
                enemyController.SpawnEnemy(EnemyId.SandEgg);
            }
        }

        public void SpawnMap(MapId id)
        {
            var mapModel = new MapModel(app.configs.MapDataConfig.GetData(id).healthPoint);
		
            _map = Instantiate(app.resourceManager.GetMap(id)).GetComponent<MapView>();
		
            _map.Init(mapModel);
        }

        public void TouchEnemy(EnemyView enemyView)
        {
            _map.model.currentHp -= 1;
        }
    }
}