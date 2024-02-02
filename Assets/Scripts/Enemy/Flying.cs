using UnityEngine;

public abstract class Flying : Enemy
{
    [SerializeField] private float _height = 0.75f;

    public float Height
    {
        get { return _height; }
        set { _height = value; }
    }

    public void ConfigureNavMeshAgent(UnityEngine.AI.NavMeshAgent agent)
    {
        agent.baseOffset = Height;
        agent.autoTraverseOffMeshLink = false;
        agent.updateUpAxis = false;
    }

}

public class RoundFlying : Flying
{

}

public class SquareFlying : Flying
{

}

