using UnityEngine;

public abstract class Armored : Enemy
{
    [SerializeField] private float _healthCoef = 2;
    private const float MinHealth = 100f; 
    private const float MaxHealth = 300f;
    private Renderer _renderer;

    public override float Health
    {
        get { return base.Health; }
        set
        {
            base.Health = value;
            UpdateColorBasedOnHealth();
        }
    }

    public float HealthCoef
    {
        get { return _healthCoef; }
        set { _healthCoef = value; }
    }

    public void IncreaseHealth()
    {
        Health *= _healthCoef;
    }

    public void IncreaseHealth(float newHealthCoef)
    {
        HealthCoef = newHealthCoef;
        IncreaseHealth();
    }

    protected override void Awake()
    {
        base.Awake();
        _renderer = GetComponent<Renderer>();
    }

    private void UpdateColorBasedOnHealth()
    {
        if (_renderer != null)
        {
            float healthRatio = (Health - MinHealth) / (MaxHealth - MinHealth);
            healthRatio = Mathf.Clamp01(healthRatio);

            Color color = Color.Lerp(Color.white, Color.black, healthRatio);
            _renderer.material.color = color;
        }
    }

}

public class RoundArmored : Armored
{

}

public class SquareArmored : Armored
{

}

