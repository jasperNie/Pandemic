using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Threading;
using UnityEngine;

public class moveCube : MonoBehaviour
{
    public GameObject[] continent;
    public Transform[] views;
    public float speed;
    //string[] continents; Come Back to This
    Transform currentView;
    private int i;
    
        // Start is called before the first frame update
    void Start() {
        currentView = views[0];
        i = 0;
        //continents = new string[5]{ "North America", "South America", "Europe", "Africa", "Asia", "Oceania"}; CB
    }
    // Update is called once per frame
    void LateUpdate()
    {    
        if (Input.GetMouseButtonDown(0)){
            Debug.Log("Mouse is down");
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

            if (hit){
                Debug.Log("Hit " + hitInfo.transform.gameObject.name);

                if (hitInfo.transform.gameObject.tag == "Continent"){
                    Debug.Log ("It's working!");

                    /*for (int j = 0; j < 6; j++) { 
                        if (continents[j] = hitInfo.transform.gameObject.tag) i = j;
                    }*/

                } 
                else {
                Debug.Log ("Not a Continent");
                }
            } 
            else {
                Debug.Log("No hit");
            }
        }

        //Camera movement code Here
        if (Input.GetKeyDown(KeyCode.RightArrow)) i++;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) i--;
        if (i > 6) i = 0;
        else if (i < 0) i = 6;
        currentView = views[i];

        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * speed);
        Vector3 currentAngle = new Vector3(Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * speed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * speed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * speed));
        transform.eulerAngles = currentAngle;
        //Camera movement code ends
    }
}

/*transform.position = new Vector3(0.0f, 0.0f, 0.0f);  for moving back to the default position */
