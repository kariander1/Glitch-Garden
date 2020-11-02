using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        if(!collision.GetComponent<Projectile>())
           FindObjectOfType<HealthDisplay>().takeLife();
    }

    
}
