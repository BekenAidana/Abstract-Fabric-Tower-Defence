using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;
    public static event Action<int> WaveEnded;
    public static event Action<int> EnemiesKilled;

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Transform _moveTarget;
    [SerializeField] private List<EnemyWave> waves = new List<EnemyWave>();

    public List<Enemy> EnemyList = new List<Enemy>();

    private int currentWaveIndex = 0; 
    private int _enemiesRemainingToKill;
    private int _enemiesKilled;

    void Start()
    {
        StartNextWave();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeWaves();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeWaves()
    {
        foreach (var wave in waves)
        {
            wave.factories = new List<AbstractEnemyFactory>
            {
                new SquareEnemyFactory(),
                new RoundEnemyFactory()
            };
        }
    }

    void StartNextWave()
    {
        if (currentWaveIndex < waves.Count)
        {
            _enemiesRemainingToKill = waves[currentWaveIndex].count;
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));
            currentWaveIndex++;
        }
        else
        {
            Debug.Log("Все волны завершены!");
        }
    }

    IEnumerator SpawnWave(EnemyWave wave)
    {
        var factory = wave.GetNextFactory(currentWaveIndex);
        for (int i = 0; i < wave.count; i++)
        {
            EnemyType type = wave.GetEnemyTypeByPercentage();

            Enemy enemy = null;
            switch (type)
            {
                case EnemyType.Infantry:
                    enemy = factory.CreateInfantry();
                    break;
                case EnemyType.Flying:
                    enemy = factory.CreateFlying();
                    break;
                case EnemyType.Armored:
                    enemy = factory.CreateArmored();
                    break;
            }
            enemy.transform.position = new Vector3(-7, 0.5f, 27);
            enemy.SetDestination(_moveTarget.position);
            enemy.OnEnemyKilled += OnEnemyKilled;
            EnemyList.Add(enemy);
            yield return new WaitForSeconds(wave.spawnInterval);
        }

    }

    private void OnEnemyKilled(float money)
    {
        _gameManager.PlayerMoney += money;
        _gameManager.UpdatePlayerMoney();
        _enemiesRemainingToKill--;
        _enemiesKilled++;
        EnemiesKilled?.Invoke(_enemiesKilled);
        if (_enemiesRemainingToKill <= 0)
        {
            WaveEnded?.Invoke(currentWaveIndex);
            StartNextWave();
        }
    }
}
