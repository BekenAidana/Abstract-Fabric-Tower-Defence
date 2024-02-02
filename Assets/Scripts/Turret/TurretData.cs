using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "Turrets/TurretData")]
public class TurretData : ScriptableObject
{
    public string TurretName;
    public float Cost;
    public float FireRange;
    public float RotationSpeed;
    public float Damage;
    public float FireRate;
    public float ShootForce;
    public GameObject Bullet;
    public Vector3 PlacePosition;
}