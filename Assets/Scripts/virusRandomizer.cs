using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class virusRandomizer : MonoBehaviour
{
    
    public TMP_Text country;
    public static int num;

    string[] countries = new string[] {"World", "Alaska", "West Canada", "Central America", "East United States", "Greenland", "North Canada", "Ontario", "East Canada", "West United States",
    "Argentina and Chile", "Brazil", "Andean Nations", "Caribbean South America", "UK and Ireland", "Iceland", "Central Europe", "North Europe", "Italy and Balkans", "Russia, Baltics, and Caucasus", "Southwest Europe",
    "Central Africa", "East Africa", "Egypt and Libya", "Madagascar", "Northwest Africa", "South Africa",
    "Central Asia", "China", "India and Pakistan", "Siberia", "Japan", "Middle East and Arabian Peninsula", "North China and Mongolia", "Southeast Asia",
    "East Australia", "Malaysia/Indonesia", "New Guinea", "West Australia"};
    
    // Start is called before the first frame update
    void Start()
    {
        num = Random.Range(1,37);
        country.text = countries[num];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
