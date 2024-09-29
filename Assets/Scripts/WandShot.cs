using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandShot : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint;     
    public float bulletSpeed = 20f;

    [SerializeField]private PlayerStats _playerStats;
    private float _cdShoot;

    public enum BulletsPref
    {
        DefaultBullet,
        SpongeBullet,
        HotDogBullet,
        PoopBullet
        
    }
    [Header("Switch the current bullet prefab")]
    public BulletsPref bulletEnum;
    public GameObject[] bulletPrefabs;

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

        SwitchBulletPrefab();
    }

    public void ApplyPowerUp()
    {
        
    }

    private void SwitchBulletPrefab()
    {
        bulletPrefab = bulletEnum switch
        {
            BulletsPref.DefaultBullet => bulletPrefabs[0],
            BulletsPref.SpongeBullet => bulletPrefabs[1],
            BulletsPref.HotDogBullet => bulletPrefabs[2],
            BulletsPref.PoopBullet => bulletPrefabs[3],
            _ => bulletPrefabs[0]
        };
    }

    void Shoot()
    {
        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * bulletSpeed;
    }
}
