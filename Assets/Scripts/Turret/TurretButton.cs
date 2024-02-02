using System;
using System.Collections;
using System.Collections.Generic;
using TD.Towers;
using UnityEngine;
using UnityEngine.UI;

public class TurretButton : MonoBehaviour
{
    public static event Action TurretCreated;
    [SerializeField] private TowerUpgrade _towerUpgrade;
    [SerializeField] private CameraController _controller;
    
    private Turrete _turret;

    public void PlaceTurret()
    {
        _turret = _towerUpgrade.Init();
        if (_turret != null)
        {
            if (GameManager.Instance.PlayerMoney >= _turret.TurretData.Cost)
            {
                TurretCreated?.Invoke();
                GameManager.Instance.PlayerMoney -= _turret.TurretData.Cost;
                GameManager.Instance.UpdatePlayerMoney();
                _controller.Pointer = Instantiate(_towerUpgrade).gameObject;
                _controller.Pointer.transform.position = new Vector3(0, 50, 0);
            }
        }
    }
}