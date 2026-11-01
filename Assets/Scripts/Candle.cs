using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField] private ItemLock lockSource;
    [SerializeField] private GameObject lightObj;

    private void OnEnable() => lockSource.Solved += ActivateLight;
    private void OnDisable() => lockSource.Solved -= ActivateLight;

    private void ActivateLight()
    {
        lightObj.SetActive(true);
    }
}
