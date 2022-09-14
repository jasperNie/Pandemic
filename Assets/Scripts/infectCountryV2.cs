using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class infectCountryV2 : MonoBehaviour{

    
    public GameObject[] adjacentCountry;
    public Material[] material;
    Renderer rend;

    public bool wearMask = false;
    public bool businessClose = false;
    public bool lockDown = false;
    private bool startResearch = false;
    private bool isSelected = false;
    public bool playerSelected = false;

    public GameObject canvas;
    public GameObject winCanvas;
    public GameObject loseCanvas;
    public TMP_Text cases;
    public TMP_Text death;
    public TMP_Text weeks;
    public TMP_Text country;
    public double defaultCases;
    public float[] infectionRate;
    public float[] deathRate;
    private float rateInfection;
    private float rateDeath;
    public double totalDeaths = 0;
    private int week = 0;
    private double numCases;
    private double recovered = 0;
    public Slider healthcareBudget;
    public Slider vaccineBudget;
    public Button research;
    public Slider vaccineProgress;
    public Slider opinionPublic;
    public Button maskButton;
    public Button businessButton;
    public Button lockButton;
    public int researchWeeks;
    public float budgetPercent;
    private float businessReduce = 0.9f;
    private float lockReduce = 0.7f;
    private int weeksInfected = 0;
    private int weeks1k = 0;
    public double deathCount;

    private float businessWeeksCounter = 0;
    private float lockweeksCounter = 0;
    private float casesWeeksCounter = 0;
    private float deathWeeksCounter = 0;

    public double population;
    private double temp;
    public double colour;
    public float rValue;
    public float gValue;

    public int countryNumber;
    private static int currentCountryNumber;

    private static int virusCountry;

    /*string[] countries = new string[] {"World", "Alaska", "West Canada", "Central America", "East United States", "Greenland", "North Canada", "Ontario", "East Canada", "West United States",
    "Argentina and Chile", "Brazil", "Andean Nations", "Caribbean South America", "UK and Ireland", "Iceland", "Central Europe", "North Europe", "Italy and Balkans", "Russia, Baltics, and Caucasus", "Southwest Europe",
    "Central Africa", "East Africa", "Egypt and Libya", "Madagascar", "Northwest Africa", "South Africa",
    "Central Asia", "China", "India and Pakistan", "Siberia", "Japan", "Middle East and Arabian Peninsula", "North China and Mongolia", "Southeast Asia",
    "East Australia", "Malaysia/Indonesia", "New Guinea", "West Australia"}; */


    //public Vector3 newYAxis;
    //public Vector3 loweredYAxis;
    //public GameObject WorldPlane;
    //public GameObject LoweredPlane;
    //public static int speed;


    // Start is called before the first frame update
    void Start(){
        virusCountry = virusRandomizer.num;
        if(virusCountry == countryNumber){
            defaultCases = 10;
            playerSelected = true;
        }
        
        rend = GetComponent<Renderer>(); //find the mesh renderer on the country
        rend.enabled = true; //make sure that the mesh renderer is enabled
        cases.text = "Cases: " + defaultCases;
        weeks.text = "Weeks: " + week;
        death.text = "Deaths: " + totalDeaths;
        
        vaccineProgress.maxValue = researchWeeks;

        if(defaultCases < 1){
                rend.sharedMaterial = material[0];
                gameObject.tag ="Safe";
                //Debug.Log("Safe");
            }
        if(defaultCases < 1000 && defaultCases > 0){
                rend.sharedMaterial = material[1];
                gameObject.tag ="Danger1";
                //Debug.Log("Danger 1");
            }
        if(defaultCases < 10000 && defaultCases > 999){
            rend.sharedMaterial = material[1];
            gameObject.tag ="Danger2";
            //Debug.Log("Danger 2");
        }
        if(defaultCases > 9999){
            rend.sharedMaterial = material[1];
            gameObject.tag ="Danger3";
            //Debug.Log("Danger 3");
        }
        country.text = "Country: " + this.gameObject.name;

    }
    
    // Update is called once per frame
    void Update(){
        
        currentCountryNumber = clickCamera.selectedCountry;

        if(currentCountryNumber == countryNumber){
            isSelected = true;
        }
        else{
            isSelected = false;
        }

        if(isSelected == true){
            canvas.SetActive(true);
        }
        else{
            canvas.SetActive(false);
        }
        

        if(playerSelected == true){
            healthcareBudget.interactable = true;
            vaccineBudget.interactable = true;
            research.interactable = true;
            maskButton.interactable = true;
            businessButton.interactable = true;
            lockButton.interactable = true;
        }
        else if (playerSelected == false){
            healthcareBudget.interactable = false;
            vaccineBudget.interactable = false;
            research.interactable = false;
            maskButton.interactable = false;
            businessButton.interactable = false;
            lockButton.interactable = false;
        }

        vaccineBudget.maxValue = budgetPercent;
        healthcareBudget.maxValue = budgetPercent;

        if(wearMask==true){
            maskButton.GetComponent<Image>().color = Color.green;
        }
        else{
            maskButton.GetComponent<Image>().color = Color.red;
        }
        if(businessClose==true){
            businessButton.GetComponent<Image>().color = Color.green;
        }
        else{
            businessButton.GetComponent<Image>().color = Color.red;
        }
        if(lockDown==true){
            lockButton.GetComponent<Image>().color = Color.green;
        }
        else{
            lockButton.GetComponent<Image>().color = Color.red;
        }


        if(defaultCases>199){
                healthcareBudget.gameObject.SetActive(true);
                research.gameObject.SetActive(true);
            }
            else{
                healthcareBudget.gameObject.SetActive(false);
                vaccineBudget.gameObject.SetActive(false);
                research.gameObject.SetActive(false);
                vaccineProgress.gameObject.SetActive(false);
            }
        if(defaultCases>199 && startResearch == true){
            research.gameObject.SetActive(false);
            vaccineBudget.gameObject.SetActive(true);
            vaccineProgress.gameObject.SetActive(true);
        }

        if(businessClose==true){
            budgetPercent = 1;
            budgetPercent = budgetPercent * businessReduce;
        }
        else if(lockDown==false){
            budgetPercent = 1;
        }
        if (lockDown==true){
            budgetPercent = 1;
            budgetPercent = budgetPercent * lockReduce;
        }
        else if(businessClose==false){
            budgetPercent = 1;
        }
        
        if(weeksInfected !=0){
            temp = defaultCases/population;

            colour = 400 * (temp/0.01);
            colour = ((colour - (colour % 1))+5)*3;

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

        if(vaccineProgress.value == researchWeeks){
            winCanvas.SetActive(true);
        }


    }

    public void nextWeek (){
            //Debug.Log(this.gameObject.name);
            
            checkLose();
            if(this.gameObject.tag == "Safe"){
                checkDanger();
            }
            else{
                calculate_Cases();
            }
            IncrementProgress();
            changeVaccine();
            changeHealthcare();
            publicOpinion();
            randomEvent();
            week++;
            weeks.text = "Weeks: " + week;

            if(playerSelected == false){
                computer();
            }

            if(defaultCases < 1){
                rend.sharedMaterial = material[0];
                gameObject.tag ="Safe";
                //Debug.Log("Safe");
                weeks1k = 0;
            }
            if(defaultCases < 1000 && defaultCases > 0){
                rend.sharedMaterial = material[1];
                gameObject.tag ="Danger1";
                //Debug.Log("Danger 1");
                weeks1k = 0;
            }
            if(defaultCases < 10000 && defaultCases > 999){
                rend.sharedMaterial = material[1];
                gameObject.tag ="Danger2";
                //Debug.Log("Danger 2");
                weeks1k++;
            }
            if(defaultCases > 9999){
                rend.sharedMaterial = material[1];
                gameObject.tag ="Danger3";
                //Debug.Log("Danger 3");
                weeks1k++;
            }
    }

    /*public void LateUpdate()
    {
        if (currentCountryNumber == countryNumber)
        {
            newYAxis = new Vector3(transform.position.x, WorldPlane.transform.position.y, transform.position.z);
            Vector3 raisedVector3 = Vector3.Lerp(transform.position, newYAxis, Time.deltaTime * speed);
            transform.position = new Vector3(raisedVector3.x, raisedVector3.y, raisedVector3.z);
            UnityEngine.Debug.Log("The Country is Lerping Up");
        }
        else if (currentCountryNumber != countryNumber)
        {
            loweredYAxis = new Vector3(transform.position.x, LoweredPlane.transform.position.y, transform.position.z);
            Vector3 loweredVector3 = Vector3.Lerp(transform.position, loweredYAxis, Time.deltaTime * speed);
            transform.position = new Vector3(loweredVector3.x, loweredVector3.y, loweredVector3.z);
            UnityEngine.Debug.Log("The Country is Lerping Down");
        }
    }*/



    public void checkLose(){
      GameObject[] countryDanger; 
      float counter = 0;
        countryDanger = GameObject.FindGameObjectsWithTag("Danger3");

        counter = countryDanger.Length;

        //Debug.Log(counter);
        if((counter/38) >= 0.5){
            //Debug.Log("You lost");
            loseCanvas.SetActive(true);
        }
    }

    public void computer (){
        float temp;
        if(weeksInfected == 5){
            temp = Random.Range(0,100);
            if (temp <= 20){
                if(wearMask == false){
                    wearMasks();
                }
            }
        }
        if(weeksInfected == 6){
            temp = Random.Range(0,100);
            if (temp <= 40){
               if(wearMask == false){
                    wearMasks();
                }
            }
        }
        if(weeksInfected == 7){
            temp = Random.Range(0,100);
            if (temp <= 60){
                if(wearMask == false){
                    wearMasks();
                }
            }
        }
        if(weeksInfected == 8){
            temp = Random.Range(0,100);
            if (temp <= 80){
                if(wearMask == false){
                    wearMasks();
                }
            }
        }
        if(weeksInfected == 9){
               if(wearMask == false){
                    wearMasks();
                }
        }
        if(weeks1k == 1){
            temp = Random.Range(0,100);
            if (temp <= 25){
                if(businessClose == false){
                    closeBusiness();
                }
            }
        }
        if(weeks1k == 2){
            temp = Random.Range(0,100);
            if (temp <= 50){
                if(businessClose == false){
                    closeBusiness();
                }
            }
        }
        if(weeks1k == 3){
            temp = Random.Range(0,100);
            if (temp <= 75){
                if(businessClose == false){
                    closeBusiness();
                }
            }
        }
        if(weeks1k == 4){
                if(businessClose == false){
                    closeBusiness();
                }
        }

        if(defaultCases >= 10000 && totalDeaths >= 100){
            if(lockDown == false){
                downLock();
            }
        }
    } 

    public void researchStart(){
        startResearch = true;
    }
    
    public void changeVaccine(){
        vaccineBudget.value = budgetPercent - healthcareBudget.value; 
        healthcareBudget.value = budgetPercent - vaccineBudget.value;
    }

    public void changeHealthcare(){
        healthcareBudget.value = budgetPercent - vaccineBudget.value;
        vaccineBudget.value = budgetPercent - healthcareBudget.value; 
    }

    public void wearMasks(){
        wearMask = !wearMask;
    }
    public void closeBusiness(){
        businessClose = !businessClose;
    }
    public void downLock(){
        lockDown = !lockDown;
    }

    /*
    public void calculateCases (){
        double mask1 = 1;
        double business1 = 1;
        double down1 = 1;
        double temp;
        double casesSubtract = 0.6;
        double health = 1;

        rateInfection = Random.Range(infectionRate[0],infectionRate[1]);
        if(gameObject.tag != "Safe"){
            weeksInfected++;
            if(defaultCases>199){
                health = 1-((1-casesSubtract)*healthcareBudget.value);
            }
            
            if(wearMask==true){
                mask1 = 0.4;
            }

            if(businessClose==true){
                business1 = 0.5;
            }

            if(lockDown==true){
                down1 = 0.15;
                business1 = 1;
                mask1 = 1;
            }

            defaultCases = defaultCases * health;
            for(int j = 0;j<ticks;j++){
                numCases = ((rateInfection * mask1 * business1 * down1 + 1) * defaultCases);
                defaultCases = numCases;
            }
            temp = numCases % 1;
            numCases = numCases - temp;
            defaultCases = numCases;
            cases.text = "Cases: " + numCases;
            
        }
    }
    */
    
    public void checkDanger(){
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

        for(int x=0;x<adjacentCountry.Length;x++)
        if(gameObject.tag == "Safe"){ 
            while(isInfected == false && i<(x+1)){
                if(adjacentCountry[i].tag == "Danger1"){
                    double num = Random.Range(1,101);
                    if (num<danger1Chance){ 
                        rend.sharedMaterial = material[1];
                        gameObject.tag ="Danger1";
                        defaultCases++;
                        cases.text = "Cases: " + defaultCases;
                        //Debug.Log(this.gameObject.name + " Has Been Infected by " + this.adjacentCountry[i].name);
                        isInfected = true;
                    }
                    else{
                        //Debug.Log(this.gameObject.name + " is Still Safe");
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
                        //Debug.Log(this.gameObject.name + " Has Been Infected by " + this.adjacentCountry[i].name);
                        isInfected = true;
                    }
                    else{
                        //Debug.Log(this.gameObject.name + " is Still Safe");
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
                        //Debug.Log(this.gameObject.name + " Has Been Infected by " + this.adjacentCountry[i].name);
                        isInfected = true;
                    }
                    else{
                        //Debug.Log(this.gameObject.name + " is Still Safe");
                        i++;
                    }
                }
                else{
                    i++;
                }
            }
        }
    }

    public void IncrementProgress(){
        if(startResearch==true){
            vaccineProgress.value = vaccineProgress.value + vaccineBudget.value;
        }
    }


    public void publicOpinion(){
        float maskDeacrease = 0.05f;
        float calculatedMask = 0f;
        float businessDeacrease = 0.2f;
        float businessWeeks = 15f;
        float businessAdd = 0.01f;
        float calculatedBusiness = 0f;
        float lockDeacrease = 0.4f;
        float lockAdd = 0.02f;
        float lockWeeks = 6f;
        float calcuatedlock = 0f;
        float casesDecrease = 0.02f;
        float casesWeeks = 5f;
        float calculatedCases = 0f;
        float deathDecrease = 0.04f;
        float calculateDeath;

        float researchDeacrease = 0.3f;
        float calculatedResearch = 0;

        if(businessClose==true){
            businessWeeksCounter++;
        }
        else{
            businessWeeksCounter = 0;
        }

        if(lockDown == true){
            lockweeksCounter++;
        }
        else{
            lockweeksCounter = 0;
        }

        if(defaultCases>=10000){
            casesWeeksCounter++;
        }
        else{
            casesWeeksCounter = 0;
        }

        if(deathCount>=150){
            deathWeeksCounter++;
        }
        else{
            deathWeeksCounter = 0;
        }

        if(wearMask==true){
            calculatedMask = maskDeacrease;
        }

        if(businessClose==true && businessWeeksCounter>=businessWeeks){
            calculatedBusiness = (businessDeacrease + (businessAdd*(businessWeeksCounter-businessWeeks)));
        }

        if(lockDown==true && lockweeksCounter>=lockWeeks){
            calcuatedlock = (lockDeacrease +(lockAdd *(lockweeksCounter-lockWeeks)));
        }

        if(defaultCases>=10000 && casesWeeksCounter>=casesWeeks){
            calculatedCases = (casesDecrease + (casesDecrease*(casesWeeksCounter-casesWeeks)));
        }

        calculateDeath = deathDecrease * deathWeeksCounter;

        calculatedResearch = ((researchDeacrease * vaccineProgress.value)/100)*30;

        opinionPublic.value = 100*(1-((calculatedMask+calculatedBusiness+calcuatedlock+calculatedCases+calculateDeath)-calculatedResearch));
    }

public void randomEvent(){
    double rand;
    double rands;
    double protest = 5;
    double smallCharity = 0;
    double bigCharity = 0;

    double natDisaster = 3;
    double more = 10;
    double less = 10;

    double protesttemp;
    double smalltemp;
    double bigtemp;

    bool randEvent = false;

    //change chances

    protesttemp = 100 - opinionPublic.value;
    if(protesttemp <= 0){
        protest = 0;
    }
    else{
        protesttemp = (protesttemp/100)*60;
        protest = protesttemp + protest;
        
    }

    smalltemp = 130 - opinionPublic.value;
    if(smalltemp > 50){
        smallCharity = 0;
    }
    else {
        smallCharity = 50 - smalltemp;
    }

    bigtemp = 130 - opinionPublic.value;
    if(bigtemp > 40){
        bigCharity = 0;
    }
    else{
        bigCharity = 40 - bigtemp;
    }
    //Debug.Log("Random Event Chances: \n Protest: " + protest + "\n Small Charity: " + smallCharity + "\n Big Charity: " + bigCharity);

    
    //Public Opinion Based Events

    rands = Random.Range(0,4);
    //Debug.Log("Public Opinion Based Events " + rands);
    if(rands == 1){
        rand = Random.Range(1,100);
        if(rand <= protest){
            defaultCases= defaultCases + 500;
            randEvent = true;
            //Debug.Log("Protest");
        }
    }
    else if (rands == 2){
        rand = Random.Range(1,100);
        if(rand <= smallCharity){
            budgetPercent = budgetPercent + 0.05f;
            randEvent = true;
            //Debug.Log("Small Charity");
        }
    }
    else if (rands == 3){
        rand = Random.Range(1,100);
        if(rand <= bigCharity){
            budgetPercent = budgetPercent + 0.1f;
            randEvent = true;
            //Debug.Log("Big Charity");
        }
    }

    //Non-Public Opinion Based Events

    if(randEvent == false){
        rands = Random.Range(0,4);
        if (rands == 1){
            rand = Random.Range(1,100);
            if(rand <=natDisaster){
                infectionRate[1] = infectionRate[1] + 0.02f;
                //Debug.Log("Natural Disaster");
            }
        }
        else if (rands == 2){
            rand = Random.Range(1,100);
            if(rand <= more){
                deathRate[1]= deathRate[1] + 0.02f;
                //Debug.Log("More Deadly");
            }
        }
        
        else if (rands == 3){
            rand = Random.Range(1,100);
            if(rand <= less){
                deathRate[1]= deathRate[1] - 0.02f;
                //Debug.Log("Less Deadly");
            }
        }
    }
}

    public void calculate_Cases(){
        double S = population - defaultCases;
        double R = recovered;

        double mask1 = 1;
        double business1 = 1;
        double down1 = 1;
        double temp;

        weeksInfected++;
        
        if(wearMask==true){
                mask1 = 0.4;
            }

            if(businessClose==true){
                business1 = 0.5;
            }

            if(lockDown==true){
                down1 = 0.15;
                business1 = 1;
                mask1 = 1;
            }

        double R_factor = 1 + (Random.Range(infectionRate[0],infectionRate[1])* mask1 * business1 * down1);
        double recovery_rate = 0.2 * healthcareBudget.value;

        rateDeath = Random.Range(deathRate[0],deathRate[1]);

        deathCount = defaultCases * rateDeath;
        deathCount = deathCount * (budgetPercent + 0.08 - healthcareBudget.value)*2;
        temp = deathCount % 1;
        deathCount = deathCount - temp;

        if(weeksInfected >= 5){
            totalDeaths = totalDeaths + deathCount;
            defaultCases = defaultCases - deathCount;
            death.text = "Deaths: " + totalDeaths;
        }

        double contacts = defaultCases * R_factor;
        double new_infections = contacts * (S/(defaultCases+R+S));
        double change_in_R = (recovery_rate * defaultCases);
        defaultCases = defaultCases + (int)new_infections - (int)change_in_R;
        recovered = R + (int)change_in_R;
        cases.text = "Cases: " + defaultCases;

    }
}