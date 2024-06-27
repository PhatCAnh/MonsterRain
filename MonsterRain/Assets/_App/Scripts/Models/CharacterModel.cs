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
            if(mainGun.currentAmmoInMagazine <= 0)
            {
                
            }

            mainGun.currentAmmo -= 1;

            mainGun.currentAmmoInMagazine -= 1;

            RaiseDataChanged("mainGun-Shot");

            return true;
        }

        public void AddAmmo(int quantity)
        {
            //_mainGun.currentAmmo = mainGun.currentAmmo + quantity > mainGun.maxAmmo ? mainGun.maxAmmo : mainGun.currentAmmo + quantity;
            RaiseDataChanged("mainGun-Shot");
        }

        public bool ReloadAmmo()
        {
            if(mainGun.currentAmmo > 0)
            {
                var number = mainGun.currentAmmo > mainGun.magazine ? mainGun.magazine : mainGun.currentAmmo;
            
                mainGun.currentAmmoInMagazine = number;

                return true;
            }
            return false;
        }
    }
}