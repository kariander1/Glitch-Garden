using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CostDisplay : MonoBehaviour
{
    private TextMeshProUGUI text;
    private int currentCost = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.text = GetComponent<TextMeshProUGUI>();
    }


    public void HidePrice()
    {
        this.text.text = "";
    }
    public void RevealPrice()
    {
        gameObject.SetActive(true);
    }
    public void SetCost(int cost)
    {
        this.currentCost = cost;
        this.text.text = currentCost.ToString();
    }
}
