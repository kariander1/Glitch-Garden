using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject loseLabel;
    [SerializeField] AudioClip winSFX;
    [SerializeField] AudioClip loseSFX;

    int numberOfAttackers = 0;
    bool levelTimerFinished = false;

    private void Start()
    {
      if(winLabel)
        winLabel.SetActive(false);
      if(loseLabel)
        loseLabel.SetActive(false);
    }
    public void AttackerSpawned()
    {
        this.numberOfAttackers++;
    }
    public void AttackerKilled()
    {
        this.numberOfAttackers--;
        if(numberOfAttackers <=0 && levelTimerFinished)
        {
            AudioSource.PlayClipAtPoint(this.winSFX, transform.position);
            winLabel.SetActive(true);
            FindObjectOfType<LevelLoader>().LoadNextScene(5);
        }
    }
    public void LevelTimerFinished()
    {
        levelTimerFinished = true;
        StopSpawners();
    }

    private void StopSpawners()
    {
        AttckerSpawner[] spawnerArray = FindObjectsOfType<AttckerSpawner>();
        foreach (AttckerSpawner spawner in spawnerArray)
        {
            spawner.StopSpawning();
        }
    }
    public void HandleLoseCondition()
    {
        Time.timeScale = 0;
        AudioSource.PlayClipAtPoint(this.loseSFX, transform.position);   
        loseLabel.SetActive(true);
    }


}
