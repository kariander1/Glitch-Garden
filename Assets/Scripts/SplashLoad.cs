using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SplashLoad : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI loadText;

    [SerializeField] float AnimationCounter = 1f;
    [SerializeField] int maxDots = 5;
    [SerializeField] float angVelocity = 1f;
    [SerializeField] float maxAng = 5f;

    private int currentDots = 0;
    private float counter = 0;
    private Rigidbody2D body;

    private bool freezeCheck = false;
    // Start is called before the first frame update
    void Start()
    {
        body = loadText.GetComponent<Rigidbody2D>();
       
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if(counter>=AnimationCounter)
        {
            counter = 0;
            currentDots++;
            if(currentDots > maxDots )
            {
                currentDots = 0;
            }
        }
        string text = "Loading";
        for (int i = 0; i < currentDots; i++)
        {
            text += ".";
        }
        loadText.text = text;

        loadText.transform.Rotate(new Vector3(0, 0, angVelocity * Time.deltaTime));
        if(Mathf.Abs(body.rotation) < this.maxAng)
        {
            freezeCheck = false ;
        }
        if(!freezeCheck && Mathf.Abs( body.rotation) > this.maxAng )
        {
            freezeCheck = true;
            angVelocity = -angVelocity;
        }
    }
}
