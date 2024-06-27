using System;
using _App.Scripts.Enums;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using MR.CharacterState;
using UnityEngine;

namespace _App.Scripts.Views
{
    public class ItemDrop : View<GameApp>
    {
        public SpriteRenderer skin;

        public ItemDropId type;
        
        public float size;

        private Cooldown _cd;

        private GameController gameController => Singleton<GameController>.instance;

        protected override void OnViewInit()
        {
            base.OnViewInit();
            _cd = new Cooldown(5);
        }

        private void FixedUpdate()
        {
            if(gameController.CheckDistancePlayer(transform.position, size))
            {
                Collected();
                return;
            }
            _cd.Update(Time.deltaTime);
            if(_cd.isFinished)
            {
                skin.DOColor(Color.clear, 2).OnComplete(() => { Destroy(gameObject); });
            }
        }

        protected virtual void Collected()
        {
            gameController.characterController.character.model.AddAmmo(10);
            skin.DOKill();
            Destroy(gameObject);
        }
        
        protected void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, size);
        }
    }
}
