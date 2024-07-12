using System;
using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Controllers;
using ArbanFramework;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform _skin;
    public float size;
    private Vector3 _direction;
    private float _speed;
    private int _atk;
    
   private Rigidbody2D rb => GetComponent<Rigidbody2D>();

   private EnemyController enemyController => Singleton<EnemyController>.instance;

   public void Moving(float deltaTime)
   {
       transform.Translate(_speed * deltaTime * _direction.normalized);
   }

   public bool CheckTouch()
   {
       var enemy = enemyController.CheckTouchEnemy(transform.position, size);
       if(enemy != null)
       {
           enemy.TakeDamage(_atk);
           return true;
       }
       return false;
   }
   
   public void Init(Vector3 direction, float speed, int atk)
   {
       this._direction = direction;
       this._speed = speed;
       this._atk = atk;
       _skin.transform.up = direction;
   }

   protected void OnDrawGizmos()
   {
       Gizmos.DrawWireSphere(transform.position, size);
   }
}
