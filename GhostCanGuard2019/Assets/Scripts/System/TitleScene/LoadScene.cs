using UnityEngine.SceneManagement;
using UnityEngine;


public static class LoadScene
{
    
    public static void loadScene(string SceneName)
    {
        SceneManager.LoadSceneAsync(SceneName);
    }
    public static void loadScene(int SceneNumber)
    {

        SceneManager.LoadSceneAsync(SceneNumber);
    }
    public static void reloadCurrent()
    {
        loadScene(SceneManager.GetActiveScene().name);
    }
    public static void loadNext()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 2)
        {
            loadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Debug.Log("Has Been Last Available Scene, Cant Load");
        }
    }
}
