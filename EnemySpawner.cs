using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class EnemySpawner : MonoBehaviour
{

    [Header("References")] 
    [SerializeField]
    private GameObject[] enemyPrefabs;

    [SerializeField]
    private TMP_Text roundText;

    [Header("Attributes")] 
    [SerializeField]
    private int baseEnemies = 8;

    [SerializeField] 
    private float eps = 0.5f; //Enemies per second

    [SerializeField] 
    private float timeBetweenWaves = 5f;

    [SerializeField] 
    private float difficultyScalar = 0.75f;

    [Header("Events")] 
    [SerializeField] 
    public static UnityEvent onEnemyDeath = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceSpawn;
    private int enemiesAlive;
    private int enemiesLeft;
    private bool isSpawning = false;

    private void Awake()
    {
        onEnemyDeath.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        UpdateRoundText();
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (!isSpawning) return;

        timeSinceSpawn += Time.deltaTime;

        if (timeSinceSpawn >= (1f / eps) && enemiesLeft > 0)
        {
            SpawnEnemy();
            enemiesLeft--;
            enemiesAlive++;
            timeSinceSpawn = 0f;
        }

        if (enemiesLeft == 0 && enemiesLeft == 0)
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeft = EnemiesPerWave();
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[0];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalar));
    }

    private void UpdateRoundText()
    {
        if (roundText != null)
        {
            roundText.text = "Round " + currentWave;
        }
    }
}
