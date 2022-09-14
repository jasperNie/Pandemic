using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class infectingCountries : MonoBehaviour{
    
    public GameObject[] adjacentCountry;
    public Material[] material;
    Renderer rend;

    public bool wearMask = false;
    public bool businessClose = false;
    public bool lockDown = false;

    public TMP_Text cases;
    public double defaultCases;
    public double infectionRate;
    public double ticks;
    private double numCases;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>(); //find the mesh renderer on the country
        rend.enabled = true; //make sure that the mesh renderer is enabled
        cases.text = "Cases: " + defaultCases;
        
        if(defaultCases < 1){
                rend.sharedMaterial = material[0];
                gameObject.tag ="Safe";
                Debug.Log("Safe");
            }
        if(defaultCases < 1000 && defaultCases > 0){
                rend.sharedMaterial = material[1];
                gameObject.tag ="Danger1";
                Debug.Log("Danger 1");
            }
        if(defaultCases < 10000 && defaultCases > 999){
            rend.sharedMaterial = material[2];
            gameObject.tag ="Danger2";
            Debug.Log("Danger 2");
        }
        if(defaultCases > 9999){
            rend.sharedMaterial = material[3];
            gameObject.tag ="Danger3";
            Debug.Log("Danger 3");
        }
        StartCoroutine(keyDown());//stop the code until the two functions below run
    }

    private IEnumerator keyDown(){
        yield return waitForKeyPress(KeyCode.Space); //runs below function to return a value
        calculateCases();
        checkDanger(); //runs the checkDanger function
    }

    private IEnumerator waitForKeyPress(KeyCode key){ //waits for user to press spacebar
        bool done = false;
        while(!done){
            if(Input.GetKeyDown(key)){
                done = true;
            }
            yield return null;
        }
    }

    void checkDanger(){
        bool isInfected = false;
        int i = 0;
        double danger1Chance = 10;
        double danger2Chance = 40;
        double danger3Chance = 80;

        double mask = 1;
        double business = 1;
        double down = 1;

        if(wearMask==true){
            mask = 0.7;
        }

        if(businessClose==true){
            business = 0.75;
        }

        if(lockDown==true){
            down = 0.4;
            business = 1;
            mask = 1;
        }

        danger1Chance = danger1Chance * mask * business * down;
        danger2Chance = danger2Chance * mask * business * down;
        danger3Chance = danger3Chance * mask * business * down;

        for(int x=0;x<adjacentCountry.Length;x++)//find the number of adjacent countries
        if(gameObject.tag == "Safe"){ //runs the while loop below only if the country currently has no cases
            while(isInfected == false && i<(x+1)){ //while loop will only run if isInfected is false and all of the adjacent countries have not been checked yet
                if(adjacentCountry[i].tag == "Danger1"){ //checks if adjacent country has the tag "Danger1"
                    double num = Random.Range(1,101); //randomly generates a number between 1 and 100
                    if (num<danger1Chance){ //if the number is less than 10 then change the material, tag, and display a message in the debug log (same for next two if statements)
                        rend.sharedMaterial = material[1];
                        gameObject.tag ="Danger1";
                        defaultCases++;
                        cases.text = "Cases: " + defaultCases;
                        Debug.Log(this.gameObject.name + " Has Been Infected by " + this.adjacentCountry[i].name);
                        isInfected = true;
                    }
                    else{
                        Debug.Log(this.gameObject.name + " is Still Safe"); //displays if the country is not infected
                        i++;
                    }
                }
                else if(adjacentCountry[i].tag == "Danger2"){
                    double num = Random.Range(1,101);
                    if (num<danger2Chance){
                        rend.sharedMaterial = material[1];
                        gameObject.tag ="Danger1";
                        defaultCases++;
                        cases.text = "Cases: " + defaultCases;
                        Debug.Log(this.gameObject.name + " Has Been Infected by " + this.adjacentCountry[i].name);
                        isInfected = true;
                    }
                    else{
                        Debug.Log(this.gameObject.name + " is Still Safe");
                        i++;
                    }
                }   
                else if(adjacentCountry[i].tag == "Danger3"){
                    double num = Random.Range(1,101);
                    if (num<danger3Chance){
                        rend.sharedMaterial = material[1];
                        gameObject.tag ="Danger1";
                        defaultCases++;
                        cases.text = "Cases: " + defaultCases;
                        Debug.Log(this.gameObject.name + " Has Been Infected by " + this.adjacentCountry[i].name);
                        isInfected = true;
                    }
                    else{
                        Debug.Log(this.gameObject.name + " is Still Safe");
                        i++;
                    }
                }
            }
        }
    }
    void calculateCases (){
        double mask1 = 1;
        double business1 = 1;
        double down1 = 1;
        double temp;

        if(gameObject.tag != "Safe"){
            if(wearMask==true){
                mask1 = 0.4;
            }

            if(businessClose==true){
                business1 = 0.5;
            }

            if(lockDown==true){
                down1 = 0.25;
            }
            for(int j = 0;j<ticks;j++){
                numCases = ((infectionRate * mask1 * business1 * down1 + 1) * defaultCases);
                defaultCases = numCases;
                Debug.Log(defaultCases);
            }
            temp = numCases % 1;
            numCases = numCases - temp;
            defaultCases = numCases;
            cases.text = "Cases: " + numCases;
            if(numCases < 1000){
                rend.sharedMaterial = material[1];
                gameObject.tag ="Danger1";
                Debug.Log("Danger 1");
            }
            if(numCases < 10000 && numCases > 999){
                rend.sharedMaterial = material[2];
                gameObject.tag ="Danger2";
                Debug.Log("Danger 2");
            }
            if(numCases > 9999){
                rend.sharedMaterial = material[3];
                gameObject.tag ="Danger3";
                Debug.Log("Danger 3");
            }
        }
    }
}