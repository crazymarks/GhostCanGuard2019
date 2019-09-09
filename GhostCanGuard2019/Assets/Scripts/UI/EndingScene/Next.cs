using UnityEngine.SceneManagement;
using UnityEngine;

public class Next : MonoBehaviour
{
    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex >= SceneManager.sceneCountInBuildSettings - 2)
        {
            gameObject.SetActive(false);
        }
    }
}
