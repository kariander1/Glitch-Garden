using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttckerSpawner : MonoBehaviour
{
    bool spawn = true;
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 5f;
    [SerializeField] Attacker[] attackerPrefab;
    // Start is called before the first frame update
    private float difficultyFactor;
    IEnumerator Start()
    {
         difficultyFactor = (2.5f - PlayerPrefsController.GetDifficulty());
        while (spawn)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnDelay*difficultyFactor, (maxSpawnDelay*difficultyFactor)+1));
            SpawnAttacker();
        }
        
    }
    public void StopSpawning()
    {
        spawn = false;
    }
    private void SpawnAttacker()
    {
        int spawnIndex = UnityEngine.Random.Range(0, attackerPrefab.Length);
        Attacker newAttacker =   Instantiate(attackerPrefab[spawnIndex], transform.position, transform.rotation) as Attacker;
        newAttacker.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
