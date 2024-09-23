using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [SerializeField] private TreesSo _treesValues;

    [Header("Current Stats")] 
    [SerializeField] private float _health;

    [SerializeField] private float _lootAmount;
    
    [SerializeField] private bool _isFarming;

    private PlayerStats _playerStats;
    private float _cdDamage =0;
    private void Awake()
    {
        _health = _treesValues.health;
        _lootAmount = _treesValues.lootAmount;
        if (FindObjectOfType<PlayerStats>())
        {
            _playerStats = FindObjectOfType<PlayerStats>();
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
