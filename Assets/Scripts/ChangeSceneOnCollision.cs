using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInput>() != null)
            SceneManager.LoadScene(2);
    }
}
