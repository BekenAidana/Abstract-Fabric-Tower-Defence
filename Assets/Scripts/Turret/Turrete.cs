using System;
using UnityEngine;

namespace TD.Towers
{
    public class Turrete : MonoBehaviour
    {
        [SerializeField] private TurretData _turretData;
        [SerializeField] protected Transform[] _shellOut;
        [SerializeField] private Transform _pylon;
        [SerializeField] private bool _fireAllGuns;

        protected float _lastShoot;
        private int _shellOutIndex;
        private Transform _target;

        public TurretData TurretData => _turretData;

        private void Update()
        {
            _target = FindClosestTarget(); 

            if (_target != null)
            {
                RotateToTarget(_target); 

                float dis = (_target.position - transform.position).magnitude;
                if (dis <= _turretData.FireRange && Time.time > _lastShoot + _turretData.FireRate)
                {
                    Shoot(++_shellOutIndex % _shellOut.Length, _fireAllGuns);
                    _lastShoot = Time.time;
                }
            }
        }

        private Transform FindClosestTarget()
        {
            Transform closestTarget = null;
            float closestDistance = float.MaxValue;

            foreach (var enemy in WaveManager.Instance.EnemyList)
            {
                if (enemy != null && enemy.gameObject.activeSelf)
                {
                    float distance = (enemy.transform.position - transform.position).magnitude;
                    if (distance < closestDistance && distance <= _turretData.FireRange)
                    {
                        closestTarget = enemy.transform;
                        closestDistance = distance;
                    }
                }
            }

            return closestTarget;
        }

        private void RotateToTarget(Transform target)
        {
            Vector3 dir = (target.position - _pylon.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            _pylon.rotation = Quaternion.Lerp(_pylon.rotation, lookRotation, Time.deltaTime * _turretData.RotationSpeed);
        }

        protected virtual void Shoot(int index, bool fireAllGuns = false)
        {
            if (!fireAllGuns)
            {
                InstantiateAndShootBullet(_shellOut[index]);
            }
            else
            {
                foreach (var shellOut in _shellOut)
                {
                    InstantiateAndShootBullet(shellOut);
                }
            }
        }

        private void InstantiateAndShootBullet(Transform shellOutTransform)
        {
            GameObject bullet = Instantiate(_turretData.Bullet, shellOutTransform.position, Quaternion.identity);
            Bullet bul = bullet.GetComponent<Bullet>();
            if (bul != null)
            {
                bul.Init(_turretData.Damage);
            }

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(shellOutTransform.forward * _turretData.ShootForce, ForceMode.Impulse);
            }

            Destroy(bullet, 1f);
            
        }

        
    }
}
