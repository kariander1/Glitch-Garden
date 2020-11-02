using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HealthDisplay : MonoBehaviour
{
    [SerializeField] int lives = 5;
    [SerializeField] int damage = 1;
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        UpdateDisplay();
    }
    private void UpdateDisplay()
    {
        text.text = this.lives.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void takeLife()
    {

        this.lives -= damage ;
            UpdateDisplay();
            
        
     if(lives<=0)
        {

            FindObjectOfType<LevelController>().HandleLoseCondition();
        }
    }

}
