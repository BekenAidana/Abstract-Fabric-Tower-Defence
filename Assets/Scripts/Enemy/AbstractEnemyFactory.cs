using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemyFactory 
{
    protected abstract string GetPrefabPath();

    protected GameObject CreateEnemyPrefab()
    {
        var prefabPath = GetPrefabPath();
        var prefab = Resources.Load<GameObject>(prefabPath);
        return GameObject.Instantiate(prefab);
    }

    public abstract Infantry CreateInfantry();
    public abstract Flying CreateFlying();
    public abstract Armored CreateArmored();
}

public class RoundEnemyFactory : AbstractEnemyFactory
{
    protected override string GetPrefabPath()
    {
        return "Prefabs/Enemies/Sphere";
    }

    public override Infantry CreateInfantry()
    {
        var go = CreateEnemyPrefab();
        var infantry = go.AddComponent<RoundInfantry>();
        return infantry;
    }

    public override Flying CreateFlying()
    {
        var go = CreateEnemyPrefab();
        var flying = go.AddComponent<RoundFlying>();
        var agent = go.GetComponent<UnityEngine.AI.NavMeshAgent>();
        flying.ConfigureNavMeshAgent(agent);
        return flying;
    }

    public override Armored CreateArmored()
    {
        var go = CreateEnemyPrefab();
        var armored = go.AddComponent<RoundArmored>();
        armored.IncreaseHealth(3);
        armored.Cost = 30;
        return armored;
    }
}

public class SquareEnemyFactory : AbstractEnemyFactory
{
    protected override string GetPrefabPath()
    {
        return "Prefabs/Enemies/Cube";
    }

    public override Infantry CreateInfantry()
    {
        var go = CreateEnemyPrefab();
        var infantry = go.AddComponent<SquareInfantry>();
        return infantry;
    }

    public override Flying CreateFlying()
    {
        var go = CreateEnemyPrefab();
        var flying = go.AddComponent<SquareFlying>(); 
        var agent = go.GetComponent<UnityEngine.AI.NavMeshAgent>();
        flying.ConfigureNavMeshAgent(agent);
        return flying;
    }

    public override Armored CreateArmored()
    {
        var go = CreateEnemyPrefab();
        var armored = go.AddComponent<SquareArmored>();
        armored.IncreaseHealth();
        return armored;
    }
}