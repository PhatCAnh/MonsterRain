using System;
using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Controllers;
using _App.Scripts.Models;
using ArbanFramework;
using ArbanFramework.MVC;
using TMPro;
using UnityEngine;

public class EnemyView : View<GameApp>
{
    public TextMeshProUGUI txtHpValue;
    
    public float size;
    
    public EnemyModel model { get; set; }

    public bool isAlive
    {
        get {
            return model.healthPoint > 0;
        }
    }

    //public bool isAlive => model.currentHealthPoint > 0;
    
    
    
    private EnemyController enemyController => Singleton<EnemyController>.instance;

    protected override void OnViewInit()
    {
        base.OnViewInit();
        RegisterListener();
    }

    public void Init(EnemyModel model)
    {
        this.model = model;

    }
    
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, size);
    }
    
    public void TakeDamage(int damage)
    {
        /* if(!IsAlive) return;
        damage -= Convert.ToInt32(damage * DamageReductionByArmor());
        var currentShield = model.shield;
        model.shield -= damage;
        damage = Mathf.RoundToInt(damage - currentShield);
        if(damage < 0) damage = 0;
        */
        model.healthPoint -= damage;

        //GameObject text = Singleton<PoolController>.instance.GetObject(ItemPrefab.TextPopup, transform.position);
        //text.GetComponent<TextPopup>().Create(damage, TextPopupType.Red);

        if(!isAlive)
        {
            Die();
        }
    }
    
    public virtual void Die(bool selfDie = false)
    {
        //isAlive = true;
        //monsCollider.isTrigger = true;
        //animator.SetBool("Dead", isDead);
        enemyController.EnemyDie(this, selfDie);
    }

    private void RegisterListener()
    {
        AddDataBinding("EnemyModel-healthPointField", txtHpValue, (control, e) =>
        {
            control.text = $"{model.healthPoint}";
        }, new DataChangedValue(EnemyModel.dataChangedEvent, nameof(model.healthPoint), model));
    }
}
