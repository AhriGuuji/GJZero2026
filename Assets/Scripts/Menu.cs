using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManagement.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
