using UnityEngine;

public class DoorLock : MonoBehaviour
{
    [SerializeField] private ItemLock lockSource;

    private void OnEnable() => lockSource.Solved += Break;
    private void OnDisable() => lockSource.Solved -= Break;

    private void Break() 
    {
        Destroy(gameObject);   
    }
}
