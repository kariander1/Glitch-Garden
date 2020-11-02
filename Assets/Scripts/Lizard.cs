using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    private GameObject target;
    private void OnTriggerEnter2D(Collider2D otherCollision)
    {
        GameObject other = otherCollision.gameObject;
        if(other.GetComponent<Defender>())
        {
            target = other;
            GetComponent<Attacker>().Attack(target);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            target = null;
            GetComponent<Attacker>().StopAttack();
        }
    }
}
