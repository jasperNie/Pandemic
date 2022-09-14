using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class activateCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    private static int currentCountryNumber;
    public GameObject canvas;
    public TMP_Text weeks;

    public int week = 0;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       currentCountryNumber = clickCamera.selectedCountry; 
       if(currentCountryNumber == 0){
           canvas.SetActive(true);
       }
       else{
           canvas.SetActive(false);
       }
        /*infectCountryV2[] countriess = FindObjectsOfType<infectCountryV2>();
        for(int i = 0; i < countriess.Length ; i++){
            if(countriess[i])
        }*/

    }

    public void continueUpdate (){
        week++;
        weeks.text = "Weeks: " + week;
        infectCountryV2[] countries = FindObjectsOfType<infectCountryV2>();
        for(int i = 0; i < countries.Length ; i++){
            countries[i].nextWeek();
        }            
    }

}
