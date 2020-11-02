using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    
    [SerializeField] Projectile projectile;
    AttckerSpawner myLaneSpawner;
    Animator animator;
    [SerializeField] GameObject gun;
    public void Fire()
    {

        Instantiate(this.projectile, gun.transform.position, Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
    
        SetLaneSpawner();
        animator = GetComponent<Animator>();
    }

    private void SetLaneSpawner()
    {
        AttckerSpawner[] spawners = FindObjectsOfType<AttckerSpawner>();
        foreach (AttckerSpawner spawner in spawners)
        {
            bool isCloseEnough = Mathf.Abs(spawner.transform.position.y - transform.position.y) <= Mathf.Epsilon;
            if(isCloseEnough)
            {
                myLaneSpawner = spawner;
            }
        }
    }

    private bool IsAttackerInLane()
    {
        if (myLaneSpawner && myLaneSpawner.transform.childCount > 0)
            return true;

        return false;
    }
    // Update is called once per frame
    void Update()
    {
        
            if (IsAttackerInLane())
            {
                animator.SetBool("IsAttacking", true);
            }
            else
            {
                animator.SetBool("IsAttacking", false);
            }
        
    }
}
