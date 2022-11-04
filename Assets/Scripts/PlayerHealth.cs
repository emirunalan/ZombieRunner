using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float playerHitPoints = 100f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamagePlayer(float damage)
    {
        playerHitPoints -= damage;
        if(playerHitPoints <= 0)
        {
            GetComponent<DeathHandler>().HandleDeath();
        }
    }


}
