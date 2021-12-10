using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int enemyHealth = 20;
    public GameObject enemy;

    /*public void Damage(int DamageAmount)
    {
        enemyHealth -= DamageAmount;
    }*/

    void Update()
    {
        if (enemyHealth <= 0 )
        {
            Destroy(enemy);
        }
    }

    
}
