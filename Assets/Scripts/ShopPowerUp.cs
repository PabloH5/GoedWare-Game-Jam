using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PowerUp
{
    public string name;
    public int cost;
    public bool wasSelled;

    public Button purchaseButton;
    public Button useButton;
    public Text costTxt;
}
public class ShopPowerUp : MonoBehaviour
{
    [Header("Player Wood")]
    public float wood;
    
    [Header("Player PowerUps")]
    public PowerUp sponge;
    public PowerUp hotDog;
    public PowerUp poop;
    
    

    private int _powerUpCounter;

    //Controllers
    private PlayerStats _playerStats;
    private WandShot _wandShot;
    private Inventory _inventory;
    
    
    private void Awake()
    {
        //find and assing controllers
        if (FindObjectOfType<PlayerStats>())
        {
            _playerStats = FindObjectOfType<PlayerStats>();
        }

        if (FindObjectOfType<WandShot>())
        {
            _wandShot = FindObjectOfType<WandShot>();
        }
        if (FindObjectOfType<Inventory>())
        {
            _inventory = FindObjectOfType<Inventory>();
        }
        
    }

    private void Start()
    {
        sponge.costTxt.text = sponge.cost.ToString();
        hotDog.costTxt.text = hotDog.cost.ToString();
        poop.costTxt.text = poop.cost.ToString();
    }

    private void PurchasePowerUp(PowerUp powerUp)
    {
        if (_inventory)
        {
            wood = _inventory.woodAmount; //subtract wood of the player amount
        }

        if (_powerUpCounter < 2)
        {
            int currentCost = powerUp.cost;
            if (wood >= currentCost)
            {
                wood -= currentCost;
                if (_inventory)
                {
                    _inventory.woodAmount = wood;
                }
                powerUp.purchaseButton.interactable = false;
                powerUp.useButton.interactable = true;
                powerUp.costTxt.text = currentCost.ToString();
                Debug.Log($"U buy: {powerUp.name}");
                _powerUpCounter++;

            }
            else
            {
                Debug.Log("Don't have enough wood to buy this upgrade.");
            
            }
        }
        
    }

    //methods for Shop buttons
    public void PurchaseSponge()
    {
        PurchasePowerUp(sponge);
    }
    public void PurchaseHotDog()
    {
        PurchasePowerUp(hotDog);
    }
    public void PurchasePoop()
    {
        PurchasePowerUp(poop);
    }
    
    public void ActivateDefault()
    {
        _wandShot.bulletEnum = WandShot.BulletsPref.DefaultBullet;
    }
    public void ActivateSponge()
    {
        _wandShot.bulletEnum = WandShot.BulletsPref.SpongeBullet;
    }
    public void ActivateHotDog()
    {
        _wandShot.bulletEnum = WandShot.BulletsPref.HotDogBullet;
    }
    public void ActivatePoop()
    {
        _wandShot.bulletEnum = WandShot.BulletsPref.PoopBullet;
    }
    public void ActivateDrug()
    {
        
    }
}
