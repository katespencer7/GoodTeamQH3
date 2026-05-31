using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditSceneLoader : MonoBehaviour
{
    public string sceneName;

    public void LoadScene()
    {
        SceneManager.LoadScene("Credits");
    }
}