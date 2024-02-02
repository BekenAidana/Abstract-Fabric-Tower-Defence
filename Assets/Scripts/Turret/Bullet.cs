using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _damage;

    public void Init(float dmg)
    {
        _damage = dmg;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<IEnemy>() is IEnemy enemy)
        {
            enemy.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }
}