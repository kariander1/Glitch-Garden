using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D otherCollision)
    {
        GameObject other = otherCollision.gameObject;
        if(other.GetComponent<Gravestone>())
        {
            GetComponent<Animator>().SetTrigger("jumpTrigger"); 
        }
        else if (other.GetComponent<Defender>())
        {
            target = other;
            GetComponent<Attacker>().Attack(target);
        }
        
    }
}
