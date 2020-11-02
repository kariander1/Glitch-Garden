using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField] int coinCost = 100;
    [SerializeField] AudioClip SpawnSound;
    [SerializeField] float hitEffectInSeconds;
    [SerializeField] float fadeOutAnimation = 1f;

    List<SpriteRenderer> renderers;
    public void AddCoints(int amount)
    {
        FindObjectOfType<CoinDisplay>().Addcoins(amount);
    }
    public int GetCost()
    {
        return coinCost;
    }
    private void Start()
    {
        
        setRenderers();
        if (SpawnSound)
        {
            AudioSource.PlayClipAtPoint(SpawnSound, transform.position);
        }
    }
    private void setRenderers()
    {
        renderers = new List<SpriteRenderer>();
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer)
            this.renderers.Add(renderer);
        foreach (SpriteRenderer render in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            this.renderers.Add(render);
        }
    }
    public void HandleHit()
    {
        StartCoroutine(HitEffect());
    }
    public void HandleDeath()
    {
        Destroy(GetComponent<BoxCollider2D>());
        StartCoroutine(FadeImage(true));
        Destroy(gameObject, fadeOutAnimation);
    }
    IEnumerator HitEffect()
    {
        foreach (SpriteRenderer renderer in this.renderers)
        {
            Color origin = renderer.color;
            renderer.color = Color.red;
            yield return new WaitForSeconds(hitEffectInSeconds);
            renderer.color = origin;

        }

    }
    IEnumerator FadeImage(bool fadeAway)
    {
        foreach (SpriteRenderer img in this.renderers)
        {
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
    }
}
