using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WaitXSecondsBeforeChangeScene : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(7);

        SceneManager.LoadScene(0);
    }
}
