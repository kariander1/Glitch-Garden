using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 200f;
  
    public bool isDying = false;
    // Start is called before the first frame updat
    public bool DealDamange(float damage)
    {
        if (!isDying)
        {
            Attacker at = GetComponent<Attacker>();
            if (at)
                at.HandleHit();
            Defender def = GetComponent<Defender>();
            if (def)
                def.HandleHit();
            this.health -= damage;
            if (health <= 0)
            {
                HandleDeath();
                return true;
            }
            return false;
        }
        return true;
    }

    private void HandleDeath()
    {
        isDying = true;
        Attacker at = GetComponent<Attacker>();
        if (at)
            at.HandleDeath();
        Defender def = GetComponent<Defender>();
        if (def)
            def.HandleDeath();

    }

  
}
