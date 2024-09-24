using System;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Upgrade
{
    public string upgradeName;

    public int baseCost;
    public float baseIncrementValue;

    public int level = 0;

    public float costMultiplier = 1.2f;
    public float valueMultiplier = 1.1f;

    public int maxLevel = 10; // Max Lvl

    public int GetCurrentCost()
    {
        return Mathf.CeilToInt(baseCost * Mathf.Pow(costMultiplier, level));
    }

    public float GetCurrentIncrementValue()
    {
        return baseIncrementValue * Mathf.Pow(valueMultiplier, level);
    }
}

public class UpgradeManager : MonoBehaviour
{
    [Header("Player Currency")]
    public float wood;

    [Header("Player Upgrades")]
    public Upgrade healthUpgrade;
    public Upgrade velMoveUpgrade;
    public Upgrade strengthUpgrade;
    public Upgrade velAttackUpgrade;

    private PlayerStats _playerStats;
    private Inventory _inventory;

    private void Awake()
    {
        if (FindObjectOfType<PlayerStats>())
        {
            _playerStats = FindObjectOfType<PlayerStats>();
        }
        if (FindObjectOfType<Inventory>())
        {
            _inventory = FindObjectOfType<Inventory>();
        }
    }

    // Método para comprar una mejora
    public bool PurchaseUpgrade(Upgrade upgrade)
    {
        if (_inventory)
        {
            wood = _inventory.woodAmount;
        }
        if (upgrade.level >= upgrade.maxLevel)
        {
            Debug.Log("Has alcanzado el nivel máximo de esta mejora.");
            return false;
        }

        int currentCost = upgrade.GetCurrentCost();
        if (wood >= currentCost)
        {
            wood -= currentCost;
            if (_inventory)
            {
                _inventory.woodAmount = wood;
            }
            ApplyUpgrade(upgrade);
            upgrade.level++; // Incrementa el nivel de la mejora
            Debug.Log($"Has comprado la mejora: {upgrade.upgradeName} al nivel {upgrade.level}");
            return true;
        }
        else
        {
            Debug.Log("No tienes suficiente oro para comprar esta mejora.");
            return false;
        }
    }

    // Método para aplicar la mejora
    private void ApplyUpgrade(Upgrade upgrade)
    {
        float incrementValue = upgrade.GetCurrentIncrementValue();

        switch (upgrade.upgradeName)
        {
            case "Health":
                _playerStats.HealthUpgrade(incrementValue);
                break;
            case "Velocity":
                _playerStats.VelMoveUpgrade(incrementValue);
                break;
            case "Strength":
                _playerStats.StrengthUpgrade(incrementValue);
                break;
            case "Attack Speed":
                _playerStats.VelAttackUpgrade(incrementValue);
                break;
            default:
                Debug.LogWarning("Mejora desconocida.");
                break;
        }
    }
}
