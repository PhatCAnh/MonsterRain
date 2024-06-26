using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Datas;
using ArbanFramework.MVC;
using UnityEngine;

namespace _App.Scripts.Models
{
    public class CharacterModel : Model<GameApp>
    {
        public static EventTypeBase dataChangedEvent = new EventTypeBase(nameof(CharacterModel) + ".dataChanged");

        public CharacterModel(EventTypeBase eventType) : base(dataChangedEvent)
        {
        }

        public CharacterModel() : base(dataChangedEvent)
        {
        }

        public CharacterModel(float moveSpeed, GunUsedData mainGun) : base(dataChangedEvent)
        {
            this.moveSpeed = moveSpeed;
            this.mainGun = mainGun;
        }

        private float _moveSpeed;

        private GunUsedData _mainGun;

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
        
        public GunUsedData mainGun
        {
            get => _mainGun;
            set
            {
                if (mainGun != value)
                {
                    _mainGun = value;
                    RaiseDataChanged(nameof(mainGun));
                }
            }
        }

        public bool Shot()
        {
            if(mainGun.currentAmmo <= 0) return false;

            mainGun.currentAmmo -= 1;
            
            Debug.Log($"Current ammo: {mainGun.currentAmmo}");

            RaiseDataChanged("mainGun-Shot");

            return true;
        }
    }
}