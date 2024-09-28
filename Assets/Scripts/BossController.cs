using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossController : MonoBehaviour
{
    [Header("Boss Current Stats")]
    public float healthMax;
    public float currentHealth;
    public float velAttack;
    public float damage;
    
    [Header("Boss Base Stats")]
    [SerializeField] private float _health;
    [SerializeField] private float _velAttack;
    [SerializeField] private float _damage;
    
    [Header("Shoot")]
    public GameObject bulletPrefab; 
    public Transform firePoint;     
    public float bulletSpeed = 20f;
    private float _cdShoot;

    [Header("Boss Debuffs")] 
    [SerializeField] private float _stuntTime;
    [SerializeField] private bool _isStunt;
    [SerializeField] private bool _isPoop;
    
    private float _poopDebuffTime = 0f;
    private float _stuntDebuffTime = 0f;


    private void Awake()
    {
        healthMax = _health;
        currentHealth = _health;
        velAttack = _velAttack;
        damage = _damage;
    }

    private void Update()
    {
        // Debuff for vel attack increment (slow attac)
        if (_isStunt)
        {
            _stuntDebuffTime += Time.deltaTime;

            if (_stuntDebuffTime >= _stuntTime)
            {
                // Set to default values
                velAttack = _velAttack;
                _isStunt = false;
                _stuntDebuffTime = 0f;
            }
        }

        // Debuff for damage decrement 
        if (_isPoop)
        {
            _poopDebuffTime += Time.deltaTime;

            if (_poopDebuffTime >= _stuntTime)
            {
                // Set to default values
                damage = _damage;
                _isPoop = false;
                _poopDebuffTime = 0f;
            }
        }
        
        _cdShoot += Time.deltaTime;
        if (_cdShoot >= velAttack)
        {
            _cdShoot = 0;
            Shoot();
        }
    }
    
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = -firePoint.up * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("DefaultBullet"))
        {
            currentHealth -= other.GetComponent<Bullet>().damage;
        } 
        else if(other.CompareTag("SpongeBullet"))
        {
            currentHealth -= other.GetComponent<Bullet>().damage;
        }
        else if(other.CompareTag("HotDogBullet"))
        {
            currentHealth -= other.GetComponent<Bullet>().damage;
            //only apply debuff when _isStunt is false, this for not spam skills
            if (!_isStunt)
            {
                _isStunt = true;
                _stuntDebuffTime = 0f;

                velAttack += (_velAttack * 0.4f);
            }
        }   
        else if(other.CompareTag("PoopBullet"))
        {
            currentHealth -= other.GetComponent<Bullet>().damage;
    
            //only apply debuff when _isPoop is false, this for not spam skills
            if (!_isPoop)
            {
                _isPoop = true;
                _poopDebuffTime = 0f; 
                damage *= 0.5f; 
            }
        }    
    }
}
