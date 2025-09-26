using UnityEngine;

public class ReceiveGameManager : MonoBehaviour
{
    [SerializeField] private GameObject mobPrefab;

    public void SpawnMob()
    {
        Instantiate(mobPrefab);
    }
}
