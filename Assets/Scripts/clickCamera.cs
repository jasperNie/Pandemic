using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class clickCamera : MonoBehaviour
{
    public GameObject[] continent;
    public GameObject WorldPlane;
    public GameObject LoweredPlane;
    public Transform[] views;
    public float speed;
    public static int m;

    string[] continents = new string[] {"North America", "South America", "Europe", "Africa", "Asia", "Oceania"};

    string[] countries = new string[] {"World", "Alaska", "West Canada", "Central America", "East United States", "Greenland", "North Canada", "Ontario", "East Canada", "West United States",
    "Argentina and Chile", "Brazil", "Andean Nations", "Caribbean South America", "UK and Ireland", "Iceland", "Central Europe", "North Europe", "Italy and Balkans", "Russia, Baltics, and Caucasus", "Southwest Europe",
    "Central Africa", "East Africa", "Egypt and Libya", "Madagascar", "Northwest Africa", "South Africa",
    "Central Asia", "China", "India and Pakistan", "Siberia", "Japan", "Middle East and Arabian Peninsula", "North China and Mongolia", "Southeast Asia",
    "East Australia", "Malaysia/Indonesia", "New Guinea", "West Australia"};

    public static int selectedCountry;

    Transform currentView;
    public Vector3 newYAxis;
    public Vector3 loweredYAxis;

    private int i; //i is used to determine which camera angle to use. 1 is North America, 6 is Oceania
    public bool OtherKey = false;
    public bool LerpUp = false;
    public bool LerpDown;
    
        // Start is called before the first frame update
    void Start() {
        //UnityEngine.Debug.Log("At Void Start");
        currentView = views[0];
        i = 0;
        selectedCountry = 0;
    }
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            i = 0;
            //UnityEngine.Debug.Log("Pressing Escape");
            selectedCountry = 0;
            m = 0;
        }
    }
    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0)){
            LerpUp = true;
            //UnityEngine.Debug.Log("Mouse is down");
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

            if (hit){
                //UnityEngine.Debug.Log("Hit " + hitInfo.transform.gameObject.name);

                if ((hitInfo.transform.gameObject.transform.parent.tag == "North America")|| (hitInfo.transform.gameObject.transform.parent.tag == "South America") || (hitInfo.transform.gameObject.transform.parent.tag == "Europe") || (hitInfo.transform.gameObject.transform.parent.tag == "Africa") ||
                      (hitInfo.transform.gameObject.transform.parent.tag == "Asia") || (hitInfo.transform.gameObject.transform.parent.tag == "Oceania"))
                {
                    OtherKey = false;

                    for (int j = 0; j < 6; j++) { //Setting i for the camera angle on clicking
                        if (continents[j] == hitInfo.transform.gameObject.transform.parent.tag) i = j+1;
                    }
                    for (int k = 0; k <= 38; k++) { //for setting the Selected Country variable
                        if (countries[k] == hitInfo.transform.gameObject.name) selectedCountry = k;
                        //UnityEngine.Debug.Log("Hit " + hitInfo.transform.gameObject.name + " which is Country Number " + selectedCountry);
                    }

                } 
                else {
                //UnityEngine.Debug.Log ("Not a Continent");
                }
            } 
            else {
                //UnityEngine.Debug.Log("No hit");
            }
        }

        //Camera movement code Here
        
        if (i == 7) i = 0;
        else if (i < 0) i = 6;
        currentView = views[i];

        //UnityEngine.Debug.Log("Showing View " + i);

        if (OtherKey == false) {
            transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * speed);

            Vector3 currentAngle = new Vector3(Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * speed), //for Lerping the camera
                Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * speed),
                Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * speed));

            transform.eulerAngles = currentAngle; //for Changing the Camera Angle 
        }
        //Raising Country Script!!!
        /*if (LerpUp == true)
        {
            for (int m = 1; m <= 38; m++)
            {
                if (selectedCountry == m)
                {
                    newYAxis = new Vector3(GameObject.Find(countries[m]).transform.position.x, WorldPlane.transform.position.y, GameObject.Find(countries[m]).transform.position.z);
                    GameObject selectedGameObject = GameObject.Find(countries[m]);
                    Vector3 raisedVector3 = Vector3.Lerp(selectedGameObject.transform.position, newYAxis, Time.deltaTime * speed);
                    selectedGameObject.transform.position = new Vector3(raisedVector3.x, raisedVector3.y, raisedVector3.z);
                    UnityEngine.Debug.Log("The Country is Lerping Up");
                    LerpUp = false;
                }
                /*else if (m != selectedCountry)        
                {
                    loweredYAxis = new Vector3(GameObject.Find(countries[m]).transform.position.x, LoweredPlane.transform.position.y, GameObject.Find(countries[m]).transform.position.z);
                    GameObject newselectedGameObject = GameObject.Find(countries[m]);
                    Vector3 loweredVector3 = Vector3.Lerp(newselectedGameObject.transform.position, loweredYAxis, Time.deltaTime * speed);
                    newselectedGameObject.transform.position = new Vector3(loweredVector3.x, loweredVector3.y, loweredVector3.z);
                    UnityEngine.Debug.Log("The Country is Lerping Down");
                } 
            }
        }*/

        //Camera movement code ends
    }
}