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

    [Header("Boss Debuffs")] 
    [SerializeField] private bool _isStunt;
    [SerializeField] private float _stuntTime;
    
    [SerializeField] private bool _isPoop;


    private float _debuffTime=0;
    private PlayerStats _playerStats;
    
    private void Awake()
    {
        healthMax = _health;
        currentHealth = _health;
        velAttack = _velAttack;

        if (!_playerStats)
        {
            _playerStats = FindObjectOfType<PlayerStats>();
        }
    }

    private void Update()
    {
        _debuffTime += Time.deltaTime;
        
        //the speed attack of the boss increment when hotdog hit him
        if (_isStunt && _debuffTime <= _stuntTime)
        {
            velAttack += (_velAttack * 0.1f);
        }
        
        //the boss damage decrement when a poop bullet hit him
        if (_isPoop && _debuffTime <= _stuntTime)
        {
            damage *= 0.5f;
            _isPoop = false;
        }
        
        //set to default values when the time of stunt is over
        if (_debuffTime >= _stuntTime)
        {
            velAttack = _velAttack;
            damage = _damage;
        }
        
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("DefaultBullet"))
        {
            currentHealth -= other.GetComponent<Bullet>().damage;
        } 
        if(other.CompareTag("SpongeBullet"))
        {
            currentHealth -= other.GetComponent<Bullet>().damage;
        }
        if(other.CompareTag("HotDogBullet"))
        {
            _isStunt = true;
        }   
        if(other.CompareTag("PoopBullet"))
        {
            _isPoop = true;
        }   
    }
    
}
