using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class AvoidGameManager : MonoBehaviour
{
    public static AvoidGameManager Instance { get; private set; }

    [Header("Programmer")] [SerializeField]
    private GameObject ddottyPrefab;

    [SerializeField] private Transform[] mommyCannons; // L, R, T
    [SerializeField] private Transform ddottyParent;

    [Header("Level Designer")] [SerializeField]
    private float minSpawnDelay;

    [SerializeField] private float maxSpawnDelay;

    private readonly float[] _ddottyInitPos = new[] { -8.5f, 8.5f, 5f };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        for (int i = 0; i < mommyCannons.Length; i++)
        {
            float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            StartCoroutine(SpawnDdotty(i, spawnDelay));
        }
    }

    private IEnumerator SpawnDdotty(int mommyIdx, float time)
    {
        yield return new WaitForSeconds(time);
        
        while (true)
        {
            Vector3 ddottyInitPos;
            Vector2 direction;
            switch (mommyIdx)
            {
                case 0:
                    ddottyInitPos = new Vector3(_ddottyInitPos[0], mommyCannons[0].position.y, 0);
                    direction = Vector2.right;
                    break;

                case 1:
                    ddottyInitPos = new Vector3(_ddottyInitPos[1], mommyCannons[1].position.y, 0);
                    direction = Vector2.left;
                    break;

                case 2:
                    ddottyInitPos = new Vector3(mommyCannons[2].position.x, _ddottyInitPos[2], 0);
                    direction = Vector2.down;
                    break;

                default:
                    yield break;
            }

            int ddottyId = Random.Range(0, Resources.LoadAll<Sprite>("DdottySheet").Length - 1);

            GameObject newDdotty = Instantiate(ddottyPrefab, ddottyInitPos, Quaternion.identity, ddottyParent);
            newDdotty.GetComponent<AvoidDdottyController>().Init(ddottyId, direction);

            float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
