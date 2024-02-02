using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType { Infantry, Flying, Armored }

[System.Serializable]
public class EnemyWave
{
    public List<AbstractEnemyFactory> factories;
    public int count;
    public float spawnInterval;

    public AbstractEnemyFactory GetNextFactory(int waveIndex)
    {
        int factoryIndex = waveIndex % factories.Count;
        return factories[factoryIndex];
    }

    public EnemyType GetEnemyTypeByPercentage()
    {
        float roll = Random.Range(0f, 100f);

        if (roll < 50)
        {
            return EnemyType.Infantry;
        }
        else if (roll < 75)
        {
            return EnemyType.Flying;
        }
        else
        {
            return EnemyType.Armored;
        }
    }
}