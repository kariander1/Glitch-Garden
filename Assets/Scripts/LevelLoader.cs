using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] int nextSceneDelay=4;

    int currentSceneIndex;
    void LoadMainMenu()
    {

        SceneManager.LoadScene(1);
    }
    IEnumerator LoadNextWithDelay(int delay)
    {

        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(currentSceneIndex+1);
    }
   public void LoadNextScene(int delay)
    {
        StartCoroutine(LoadNextWithDelay(delay));
    }
    // Start is called before the first frame update

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;   
        if(currentSceneIndex ==0)
        {
            LoadNextScene(this.nextSceneDelay);
        }
    }
    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void LoadMainMenuScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start Screen");
    }
    public void LoadLoseScene()
    {
        SceneManager.LoadScene("Lose Screen");
    }
    public void LoadOptionsScene()
    {
        SceneManager.LoadScene("Options Screen");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
