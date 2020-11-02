using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] int coins = 50;
    [SerializeField] GameObject coinsDropVFX;
    [SerializeField] AudioClip coinDropSFX;
    [SerializeField] float hitEffectInSeconds = 0.2f;
    [SerializeField] float fadeOutAnimation = 1f;
    [SerializeField] float offsetX = 0.5f;
    [SerializeField] float offsetY = 0.25f;
    [SerializeField] float baseSpeed = 1f;
    [SerializeField] float damage = 20f;
    [SerializeField] float richochetResistFactor = 0f;
    GameObject currentTarget;

    private bool overrideSpeedFlag = false;
    private float overrideSpeed = 0f;
    private bool movementEnabled = true;
    private float speedFactor = 1f;
    private float speedFactorTimer = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        FindObjectOfType<LevelController>().AttackerSpawned();
    }
    private void OnDestroy()
    {
        LevelController levelController = FindObjectOfType<LevelController>();
        if (levelController)
            levelController.AttackerKilled();

            
    }
    void Start()
    {
        transform.Translate(offsetX, offsetY,0);
    }
    public float getRichochetResistance()
    {
        return this.richochetResistFactor;
    }
    public void slow(float factor, float period)
    {
        this.speedFactor = 1 - factor;
        this.speedFactorTimer = period;
    }
    // Update is called once per frame
    void alterColorFreeze()
    {
        Color toChange;
        ColorUtility.TryParseHtmlString("#5DFEFF", out toChange);

        GetComponent<SpriteRenderer>().color = toChange;
    }
    void restoreColor()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    void Update()
    {
        UpdateAnimationState();
        if (speedFactorTimer>0)
        {
            alterColorFreeze();
            speedFactorTimer -= Time.deltaTime;
            if(speedFactorTimer<=0)
            {
                this.speedFactor = 1f;
                restoreColor();
            }
                
        }

        if(movementEnabled)
        {
            if(!overrideSpeedFlag)
                transform.Translate(Vector2.left * baseSpeed * (this.speedFactor) * Time.deltaTime);
            else
                transform.Translate(Vector2.left * overrideSpeed * (this.speedFactor) * Time.deltaTime);
        }
         
    }

    private void UpdateAnimationState()
    {
        if(!currentTarget)
        {
            GetComponent<Animator>().SetBool("IsAttacking", false);
        }
    }

    public void SetMovementSpeed(float speed)
    {
        this.baseSpeed = speed;
        this.overrideSpeedFlag = false;
    }
    public void Attack(GameObject target)
    {
        GetComponent<Animator>().SetBool("IsAttacking",true);
        currentTarget = target;
    }
    public void StopAttack()
    {
        GetComponent<Animator>().SetBool("IsAttacking",false);
        currentTarget = null;
    }
    public void setMovementEnabled(int enabled)
    {
        this.movementEnabled = (enabled != 0);
        this.overrideSpeedFlag = false;
    }
    public void setMovementOverride(float speed)
    {
       if(Math.Abs(speed) >0)
        {
            this.overrideSpeed = speed;
            this.overrideSpeedFlag = true;
        }
       else
        {
            this.overrideSpeed = 0f;
            this.overrideSpeedFlag = false;
        }
       
    }
    public int GetCoins()
    {
        return this.coins;
    }
    public void HandleDeath()
    {
        FindObjectOfType<CoinDisplay>().Addcoins(this.coins);
        if (coinsDropVFX)
        {
            if(coinDropSFX)
            {
                AudioSource.PlayClipAtPoint(this.coinDropSFX, transform.position);
            }
            GameObject vfx = Instantiate(coinsDropVFX, GetComponent<Collider2D>().bounds.center, Quaternion.identity);
          //  vfx.GetComponent<ParticleSystem>().maxParticles = this.coins/10;
            vfx.transform.Translate(new Vector3(0, 0, -2));
            Destroy(vfx, 1f);
        }
        StartCoroutine(FadeImage(true));
        Destroy(gameObject, fadeOutAnimation);
    }
    public void HandleHit()
    {
        StartCoroutine(HitEffect());
    }
    IEnumerator HitEffect()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Color origin = renderer.color;
        renderer.color = Color.red;
        yield return new WaitForSeconds(hitEffectInSeconds);
        renderer.color = origin;

    }
    IEnumerator FadeImage(bool fadeAway)
    {
        SpriteRenderer img = GetComponent<SpriteRenderer>();

        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = fadeOutAnimation; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= fadeOutAnimation; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
    public void StrikeCurrentTarget()
    {
        StrikeCurrentTarget(this.damage);
    }
        public void StrikeCurrentTarget(float damage)
    {
        if(!currentTarget) { return; }
        Health health = currentTarget.GetComponent<Health>();

        if(health)
        {
            health.DealDamange(damage);
        }
    }
}
