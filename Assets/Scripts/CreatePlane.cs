using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlane : MonoBehaviour
{
    
    public float time = 0;
    int temp;
    public float timedelay = 5.9f;
    public GameObject myPrefab;
    public GameObject[] name;
    // Update is called once per frame
    void Update()
    {
        name = GameObject.FindGameObjectsWithTag("Plane");
        time +=Time.deltaTime;
        if(time >= timedelay){
            time = 0;
            temp = Random.Range(1,15);
            switch(temp){
                case 1: 
                    Instantiate(myPrefab, new Vector3(-28.42f, 31.16f, 9.62f), Quaternion.Euler(new Vector3(0, -90f, 0)));
                    break;
                case 2: 
                    Instantiate(myPrefab, new Vector3(-34.74f, 31.16f, 13.87f), Quaternion.Euler(new Vector3(0, -34.26f, 0)));
                    break;
                case 3: 
                    Instantiate(myPrefab, new Vector3(-1.6f, 31.16f, 13.7f), Quaternion.Euler(new Vector3(0, 45.43f, 0)));
                    break;
                case 4: 
                    Instantiate(myPrefab, new Vector3(-2.58f, 31.16f, 13.7f), Quaternion.Euler(new Vector3(0, 91.4f, 0)));
                    break;
                case 5:
                    Instantiate(myPrefab, new Vector3(-18.7f, 31.16f, -9.4f), Quaternion.Euler(new Vector3(0, 141.7f, 0)));
                    break;
                case 6:
                    Instantiate(myPrefab, new Vector3(3.62f, 31.16f, -10.2f), Quaternion.Euler(new Vector3(0, 113.37f, 0)));
                    break;
                case 7:
                    Instantiate(myPrefab, new Vector3(-21.95f, 31.16f, -4.23f), Quaternion.Euler(new Vector3(0, 300f, 0)));
                    break;
                case 8:
                    Instantiate(myPrefab, new Vector3(3.15f, 31.16f, -12.27f), Quaternion.Euler(new Vector3(0, 175.5f, 0)));
                    break;
                case 9:
                    Instantiate(myPrefab, new Vector3(1.16f, 31.16f, 14.46f), Quaternion.Euler(new Vector3(0, 2.72f, 0)));
                    break;
                case 10:
                    Instantiate(myPrefab, new Vector3(1.16f, 31.16f, 14.46f), Quaternion.Euler(new Vector3(0, -75.2f, 0)));
                    break;
                case 11:
                    Instantiate(myPrefab, new Vector3(26.8f, 31.16f, 11.2f), Quaternion.Euler(new Vector3(0, 102.59f, 0)));
                    break;
                case 12:
                    Instantiate(myPrefab, new Vector3(26.8f, 31.16f, 11.2f), Quaternion.Euler(new Vector3(0, 57.02f, 0)));
                    break;
                case 13:
                    Instantiate(myPrefab, new Vector3(26.8f, 31.16f, 11.2f), Quaternion.Euler(new Vector3(0, -20.72f, 0)));
                    break;
                case 14:
                    Instantiate(myPrefab, new Vector3(2.41f, 31.16f, -10.38f), Quaternion.Euler(new Vector3(0, -125.64f, 0)));
                    break;
                case 15:
                    Instantiate(myPrefab, new Vector3(40.46f, 31.16f, -13.7f), Quaternion.Euler(new Vector3(0, -200.4f, 0)));
                    break;
            }
        }
        if(time >= 2.9f){
             Destroy(name[0]);
        }
    }


}
