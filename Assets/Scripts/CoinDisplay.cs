using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinDisplay : MonoBehaviour
{
    [SerializeField] int coins = 100;
    TextMeshProUGUI coinsText;
    // Start is called before the first frame update
    void Start()
    {
        coinsText = GetComponent<TextMeshProUGUI>();
        UpdateDisplay();
    }
    private void UpdateDisplay()
    {
        coinsText.text = this.coins.ToString();
    }
    public void Addcoins(int coins)
    {
        this.coins += coins;
        UpdateDisplay();
    }
    public bool Spendcoins(int coins)
    {
        if(this.coins>=coins)
        {
            this.coins -= coins;
            UpdateDisplay();
            return true;
        }
        return false;
    }
}
