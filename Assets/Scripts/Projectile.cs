using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float damage=100;
    [SerializeField] float velocity=3f;
    [SerializeField] float angularVelocity = 2f;
    [SerializeField] float ricochet = 0.2f;
    [SerializeField] bool destroyUponContact = true;
    [SerializeField] public float slowFactor = 0f;
    [SerializeField] public float slowPeriod = 5f;
    [SerializeField] GameObject HitVFX;
    [SerializeField] AudioClip ShootSFX;
    [SerializeField] AudioClip HitSFX;
    bool isEnemy=false;


    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(this.ShootSFX, transform.position);
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        body.velocity =Vector2.right* this.velocity;
        body.angularVelocity =  -this.angularVelocity;

        if(isEnemy)
        {
            body.velocity *= -1;
            body.angularVelocity *= -1;
        }
    }
  
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Attacker  attacker= otherCollider.GetComponent<Attacker>();
        if (attacker)
        {
          
            Health health = otherCollider.GetComponent<Health>();
            if (health && !health.isDying)
            {
                AudioSource.PlayClipAtPoint(this.HitSFX, transform.position);
                TriggerHitVFX();
                if (!health.DealDamange(this.damage)) // Ricochet
                {
                    otherCollider.transform.Translate(ricochet*(1- attacker.getRichochetResistance()), 0, 0);
                }
                if (slowFactor > 0 && slowPeriod >0)
                {
                    attacker.slow(slowFactor,slowPeriod);
                }
                if (destroyUponContact)
                    Destroy(gameObject);
            }
        }
    }
    private void TriggerHitVFX()
    {
        if (HitVFX)
        {
            GameObject vfx = Instantiate(HitVFX, transform.position, Quaternion.identity);
            vfx.transform.Translate(new Vector3(0, 0, -1));
            Destroy(vfx, 1f);
        }
    }
}
