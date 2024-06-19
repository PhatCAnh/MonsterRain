using System;
using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Controllers;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;

public class EnemyView : View<GameApp>
{
    public float size;
    
    //public bool isAlive => model.currentHealthPoint > 0;
    
    private EnemyController enemyController => Singleton<EnemyController>.instance;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, size);
    }
    
    public virtual void Die(bool selfDie = false)
    {
        //isAlive = true;
        //monsCollider.isTrigger = true;
        //animator.SetBool("Dead", isDead);
        enemyController.EnemyDie(this, selfDie);
    }
}
