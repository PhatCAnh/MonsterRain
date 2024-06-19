using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{

    [SerializeField] BoxCollider2D cd;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private float cooldown;
    private float timer;


    private int shushiCreated;
    private int shushiMilestone = 10;
    
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            timer = cooldown;
            shushiCreated++;

            if (shushiCreated > shushiMilestone && cooldown > .5f)
            {
                shushiMilestone += 10;
                cooldown -= .3f;
            }

           
        }
    }
}
