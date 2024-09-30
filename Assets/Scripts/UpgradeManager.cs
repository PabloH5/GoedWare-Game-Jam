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

    public int level = 0; // Nivel actual

    public float costMultiplier = 1.2f; // Multiplicador del costo
    public float valueMultiplier = 1.1f; // Multiplicador del valor

    public int maxLevel = 5; // Nivel máximo

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

    // Controladores
    private PlayerStats _playerStats;
    private Inventory _inventory;

    private void Awake()
    {
        // Encontrar y asignar controladores
        _playerStats = FindObjectOfType<PlayerStats>();
        if (_playerStats == null)
        {
            Debug.LogError("No se encontró un componente PlayerStats en la escena.");
        }

        _inventory = FindObjectOfType<Inventory>();
        if (_inventory == null)
        {
            Debug.LogError("No se encontró un componente Inventory en la escena.");
        }
    }

    private void Start()
    {
        healthUpgrade.costTxt.text = healthUpgrade.GetCurrentCost().ToString();
        velMoveUpgrade.costTxt.text = velMoveUpgrade.GetCurrentCost().ToString();
        strengthUpgrade.costTxt.text = strengthUpgrade.GetCurrentCost().ToString();
        velAttackUpgrade.costTxt.text = velAttackUpgrade.GetCurrentCost().ToString();
    }

    // Métodos para asignar a los botones en la UI
    public void PurchaseHealthUpgrade()
    {
        PurchaseUpgradeSpecific(healthUpgrade, "Health");
    }

    public void PurchaseVelMoveUpgrade()
    {
        PurchaseUpgradeSpecific(velMoveUpgrade, "Velocity");
    }

    public void PurchaseStrengthUpgrade()
    {
        PurchaseUpgradeSpecific(strengthUpgrade, "Strength");
    }

    public void PurchaseVelAttackUpgrade()
    {
        PurchaseUpgradeSpecific(velAttackUpgrade, "Attack Speed");
    }

    // Método general para comprar mejoras
    private void PurchaseUpgradeSpecific(Upgrade upgrade, string upgradeName)
    {
        if (_inventory)
        {
            wood = _inventory.woodAmount; // Obtener la cantidad actual de madera
        }

        // Verificar el nivel actual de la mejora
        if (upgrade.level >= upgrade.maxLevel)
        {
            Debug.Log($"Has alcanzado el nivel máximo de la mejora: {upgradeName}.");
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

                // Incrementar el nivel antes de aplicar la mejora
                upgrade.level++;

                // Aplicar la mejora utilizando el nivel actualizado
                ApplyUpgrade(upgrade, upgradeName);

                // Obtener el nuevo costo después de incrementar el nivel
                int newCost = upgrade.GetCurrentCost();
                upgrade.costTxt.text = newCost.ToString();

                Debug.Log($"Has comprado: {upgradeName} al nivel {upgrade.level}");
            }
            else
            {
                Debug.Log("No tienes suficiente madera para comprar esta mejora.");
            }
        }
    }

    // Método para aplicar la mejora
    private void ApplyUpgrade(Upgrade upgrade, string upgradeName)
    {
        // Obtener el valor de incremento basado en el nivel actual
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
                Debug.LogWarning("Mejora desconocida.");
                break;
}
}
}