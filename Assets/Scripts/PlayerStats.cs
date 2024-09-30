using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Current Stats")]
    public float healthMax;
    public float currentHealth;
    public float velMove;
    public float strength;
    public float velAttack;
    
    [Header("Player Base Stats")]
    [SerializeField] private float _health;
    [SerializeField] private float _velMove;
    [SerializeField] private float _strength;
    [SerializeField] private float _velAttack;
    [SerializeField] private HealthBar _healthBar;
    

    private void Awake()
    {
        healthMax = _health;
        currentHealth = _health;
        velMove = _velMove;
        strength = _strength;
        velAttack = _velAttack;
    }

    private void Update()
    {
        if (currentHealth < 0)
        {
            Destroy(this.gameObject);
        }
    }

    //Method for heal the player
    public void Heal(float healValue)
    {
        currentHealth += currentHealth + healValue;
    }
    //METHODS FOR UPGRADE THE STATS

    public void HealthUpgrade(float incrementValue)
    {
        currentHealth = healthMax * incrementValue;
    }
    public void VelMoveUpgrade(float incrementValue)
    {
        velMove = velMove * incrementValue;
    }
    public void StrengthUpgrade(float incrementValue)
    {
        strength = strength * incrementValue;
    }
    public void VelAttackUpgrade(float incrementValue)
    {
        velAttack = velAttack * incrementValue;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("BossBullet"))
        {
            currentHealth -= other.GetComponent<Bullet>().damage;
            _healthBar.UpdateHealthbar(healthMax, currentHealth);
        } 
    }
}
