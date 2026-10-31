using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    [SerializeField] private Camera cam;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void MakeItShake(float duration, float intensity, float speed = 1.0f)
    {
        StartCoroutine(Shake(duration, intensity, speed));
    }

    private IEnumerator Shake(float duration, float intensity, float speed = 1.0f)
    {
        float elapsedTime = 0.0f;
        Vector3 initialPos = cam.transform.localPosition;

        while (elapsedTime < duration)
        {
            float magnitude = intensity * Mathf.Exp(speed * elapsedTime);
            float offsetX = Random.Range(-1.0f, 1.0f) * magnitude;
            float offsetY = Random.Range(-1.0f, 1.0f) * magnitude;

            cam.transform.localPosition = new Vector3(initialPos.x + offsetX, initialPos.y + offsetY, initialPos.z);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        cam.transform.localPosition = initialPos;
    }
}