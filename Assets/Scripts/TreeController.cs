using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [Header("Tree Scriptable Object")]
    [SerializeField] private TreesSo _treesValues;
    
    [Header("Default Max Stats")]
    [SerializeField] private float _healthMax;
    [SerializeField] private float _lootMax;

    [Header("Current Stats")] 
    [SerializeField] private float _health;

    [SerializeField] private float _lootAmount;
    
    [SerializeField] private bool _isFarming;

    //Controllers
    private Inventory _inventory;
    private PlayerStats _playerStats;
    private float _cdDamage =0;
    private void Awake()
    {
        
        _healthMax = _treesValues.health;
        // _lootMax = _treesValues.lootAmount;

        _health = _healthMax;
        // _lootAmount = _lootMax;
        
        
        if (FindObjectOfType<PlayerStats>())
        {
            _playerStats = FindObjectOfType<PlayerStats>();
        }
        if (FindObjectOfType<Inventory>())
        {
            _inventory = FindObjectOfType<Inventory>();
        }
        
    }

    private void Update()
    {
        _cdDamage += Time.deltaTime;
        if (_isFarming && _cdDamage >= _playerStats.velAttack)
        {
            Debug.Log(_cdDamage);
            _cdDamage = 0;
            _health -= _playerStats.strength;
            _inventory.woodAmount = _healthMax - _health;
            // _lootAmount -= _playerStats.dropAmount * _lootMax;
            // _inventory.woodAmount += _playerStats.dropAmount * _lootMax;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FarmArea"))
        {
            Debug.Log("ME FARMEAN");
            _isFarming = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FarmArea"))
        {
            Debug.Log("ME FARMEAN");
            _isFarming = false;
        }
    }
}
