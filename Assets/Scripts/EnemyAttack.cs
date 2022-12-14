using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    
    [SerializeField] Transform target;
    [SerializeField] float damage = 40f;
    [SerializeField] PlayerHealth player;
    
    
    void Start()
    {
        
    }

    public void AttackHitEvent()
    {
        if(target == null) return;
        player.TakeDamagePlayer(damage);
        player.GetComponent<DisplayDamage>().ShowDamageImpact();
    }


}
