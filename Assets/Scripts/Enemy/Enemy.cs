using UnityEngine;
using UnityEngine.UI;
using System;


public class Enemy : MonoBehaviour, IEnemy
{
    public event Action<float> OnEnemyKilled;

    [SerializeField] private float _health = 100;
    [SerializeField] private int _cost = 10;
    [SerializeField] private float _maxHealth;
    
    private Image _healthImage;
    private GameObject _canvas;
    private bool _isDead = false;

    public virtual float Health
    {
        get { return _health; }
        set { _health = value; 
            _maxHealth = value;}
    }

    public int Cost
    {
        get { return _cost; }
        set { _cost = value; }
    }

    protected virtual void Awake()
    {   
        FindHealthImage();
        _canvas = transform.GetChild(0).gameObject;
        _maxHealth = _health;
    }

    private void FindHealthImage()
    {
        Transform imageParent = transform.Find("Canvas/Image");
        if (imageParent != null)
        {
            _healthImage = imageParent.transform.GetChild(0).gameObject.GetComponent<Image>();
        }
    }

    public void SetDestination(Vector3 targetPosition)
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(targetPosition);
    }

    public void TakeDamage(float dmg)
    {
        _health -= dmg;
        _healthImage.fillAmount = _health / _maxHealth;
        
        if (_health <= 0 && !_isDead)
        {
            _isDead = true;
            gameObject.SetActive(false);
            OnEnemyKilled?.Invoke(_cost);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Vector3 dir = Camera.main.transform.position - _canvas.transform.position;
        dir.y = 0;
        if (dir != Vector3.zero)
        {
            _canvas.transform.rotation = Quaternion.LookRotation(dir);
        }
    }

}

public interface IEnemy
{
    void TakeDamage(float dmg);
}

