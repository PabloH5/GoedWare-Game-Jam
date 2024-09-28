using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandShot : MonoBehaviour
{
    public GameObject[] bulletPrefabs;
    public GameObject bulletPrefab; 
    public Transform firePoint;     
    public float bulletSpeed = 20f;

    [SerializeField]private PlayerStats _playerStats;
    private float _cdShoot;

    private void Awake()
    {
        if (!_playerStats)
        {
            _playerStats = FindObjectOfType<PlayerStats>();
        }
        
    }

    void Update()
    {
        _cdShoot += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && _cdShoot >= _playerStats.velAttack)
        {
            _cdShoot = 0;
            Shoot();
        }
    }

    public void ApplyPowerUp()
    {
        
    }

    void Shoot()
    {
        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * bulletSpeed;
    }
}
