using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[System.Serializable]
public class Upgrade
{
    public string upgradeName;

    public int baseCost;
    public float baseIncrementValue;

    public int level = 0; //current level

    public float costMultiplier = 1.2f; //multiplier of cost
    public float valueMultiplier = 1.1f; //Multiplier of value

    public int maxLevel = 5; // Max Lvl

    public Text costTxt;

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
    [Header("Player Wood")]
    public float wood;

    [Header("Player Upgrades")]
    public Upgrade healthUpgrade;
    public Upgrade velMoveUpgrade;
    public Upgrade strengthUpgrade;
    public Upgrade velAttackUpgrade;

    //Controllers
    private PlayerStats _playerStats;
    private Inventory _inventory;

    private void Awake()
    {
        //find and assing controllers
        if (FindObjectOfType<PlayerStats>())
        {
            _playerStats = FindObjectOfType<PlayerStats>();
        }
        if (FindObjectOfType<Inventory>())
        {
            _inventory = FindObjectOfType<Inventory>();
        }
    }

    private void Start()
    {
        healthUpgrade.costTxt.text  = healthUpgrade.baseCost.ToString();
        velMoveUpgrade.costTxt.text  = velMoveUpgrade.baseCost.ToString();
        strengthUpgrade.costTxt.text  = strengthUpgrade.baseCost.ToString();
        velAttackUpgrade.costTxt.text  = velAttackUpgrade.baseCost.ToString();
    }

    //Method for assign to button Health in UI
    public void PurchaseHealthUpgrade()
    {
        PurchaseUpgradeSpecific(healthUpgrade, "Health");
    }

    //Method for assign to button VelMove in UI
    public void PurchaseVelMoveUpgrade()
    {
        PurchaseUpgradeSpecific(velMoveUpgrade, "Velocity");
    }
    
    //Method for assign to button Strength in UI
    public void PurchaseStrengthUpgrade()
    {
        PurchaseUpgradeSpecific(strengthUpgrade, "Strength");
    }
    
    //Method for assign to button VelAttack in UI
    public void PurchaseVelAttackUpgrade()
    {
        PurchaseUpgradeSpecific(velAttackUpgrade, "Attack Speed");
    }
    // Method for purchase general upgrades, where the parameters necessary are type of Upgrade and the name
    private void PurchaseUpgradeSpecific(Upgrade upgrade, string upgradeName)
    {
        if (_inventory)
        {
            wood = _inventory.woodAmount; //subtract wood of the player amount
        }

        //check the current upgrade lvl
        if (upgrade.level >= upgrade.maxLevel) 
        {
            Debug.Log($"Reached the top level of upgrade: {upgradeName}.");
            
        }
        else
        {
            int currentCost = upgrade.GetCurrentCost();
            if (wood >= currentCost)
            {
                wood -= currentCost;
                if (_inventory)
                {
                    _inventory.woodAmount = wood;
                }
                ApplyUpgrade(upgrade, upgradeName);
                upgrade.level++;
                upgrade.costTxt.text = currentCost.ToString();
                Debug.Log($"U buy: {upgradeName} at lvl {upgrade.level}");
            
            }
            else
            {
                Debug.Log("Don't have enough wood to buy this upgrade.");
            
            }
        }
        
        
    }

    // Method for apply and update the purchased upgrade
    private void ApplyUpgrade(Upgrade upgrade, string upgradeName)
    {
        float incrementValue = upgrade.GetCurrentIncrementValue();

        switch (upgradeName)
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
                Debug.LogWarning("Unknwoledge upgrade.");
                break;
        }
    }
}
