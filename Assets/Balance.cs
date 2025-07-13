using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Balance : MonoBehaviour
{
    public int balance = 1200;
    public Text balanceText;
    public int unitCost;
    public int factoryCost;
    public static bool canBuy;
    public static bool canBuy2;
    private float timer = 0f;
    public static int increaseAmount = 5; // Amount to increase balance every second

    public void PurchaseUnit()
    {
        if (balance >= unitCost && UnitScript.isTouchingLand)
        {
            balance -= unitCost;
            canBuy = true;
            UpdateBalanceText();
        }
        else
        {
            canBuy = false;
        }
        //canBuy = false;

    }
    public void PurchaseFactory()
    {
        if (balance >= factoryCost && UnitScript.isTouchingLand)
        {
            balance -= factoryCost;
            canBuy2 = true;
            UpdateBalanceText();
        }
        else
        {
            canBuy2 = false;
        }
        //canBuy2 = false;
    }
    private void UpdateBalanceText()
    {
        if (balanceText != null)
        {
            balanceText.text = "Balance: " + balance.ToString();
        }
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            balance += increaseAmount;
            UpdateBalanceText();
            timer = 0f;
        }
    }
    
}

