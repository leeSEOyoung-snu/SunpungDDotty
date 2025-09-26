using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class AvoidGameManager : MonoBehaviour
{
    public static AvoidGameManager Instance { get; private set; }

    [Header("Programmer")]
    [SerializeField] private GameObject ddottyPrefab;
    [SerializeField] private Transform[] mommyCannons; // L, R, T
    [SerializeField] private Transform ddottyParent;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI lifeText;

    [Header("Level Designer")]
    [SerializeField] private float minSpawnDelay;
    [SerializeField] private float maxSpawnDelay;
    [SerializeField] private int life;

    private readonly float[] _ddottyInitPos = new[] { -8.5f, 8.5f, 5f };
    
    private float _elapsedTime = 0f;

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
        scoreText.text = "00:00:00";
        lifeText.text = life.ToString();
        
        for (int i = 0; i < mommyCannons.Length; i++)
        {
            float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            StartCoroutine(SpawnDdotty(i, spawnDelay));
        }
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        
        int hour = (int)(_elapsedTime / 3600f);
        int min = (int)(_elapsedTime / 60f);
        int sec = (int)(_elapsedTime % 60f);
        
        scoreText.text = $"{hour:D2}:{min:D2}:{sec:D2}";
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

    public void OnDdottyCollision(int id)
    {
        life--;
        lifeText.text = life.ToString();
        
        if (life <= 0)
        {
            OnEndGame();
        }
    }

    private void OnEndGame()
    {
        Time.timeScale = 0f;
    }
}
