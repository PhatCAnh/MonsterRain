using System;
using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Controllers;
using ArbanFramework;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float size;
    private Vector3 _direction;
    private float _speed;
    
   private Rigidbody2D rb => GetComponent<Rigidbody2D>();

   private EnemyController enemyController => Singleton<EnemyController>.instance;

   void Update()
   {
       //transform.right = rb.velocity;
       transform.Translate(_speed * Time.deltaTime *_direction);
   }

   private void FixedUpdate()
   {
       var enemy = enemyController.CheckTouchEnemy(transform.position, size);
       if(enemy != null)
       {
           enemy.Die();
           UIController.Instance.AddScore();
           Destroy(gameObject);
       }
   }

   public void Init(Vector3 direction, float speed)
   {
       this._direction = direction;
       this._speed = speed;
   }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if(collision.tag == "Target")
    //     {
    //         Destroy(gameObject);
    //         Destroy(collision.gameObject);
    //
    //         UIController.Instance.AddScore();
    //     }
    // }
}
