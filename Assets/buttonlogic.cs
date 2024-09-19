using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonlogic : MonoBehaviour
{
    public string sceneToLoad;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
