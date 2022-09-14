using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gradientTest : MonoBehaviour
{
    public double population;
    public double defaultCases;
    private double temp;
    public double colour;
    public float rValue;
    public float gValue;

    public Slider cases;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        temp = defaultCases/population;
        Debug.Log(temp);

        colour = 400 * (temp/0.0075);
        colour = colour - (colour % 1);
        Debug.Log(colour);

        if(colour<=200){
            gValue = 200;
            rValue = (float)colour;
        }

        if(colour>200){
            rValue = 200;
            gValue = 200 - ((float)colour - 200);
        }

        gameObject.GetComponent<Renderer>().material.color = new Color(rValue/255,gValue/255,0);
    }
}
