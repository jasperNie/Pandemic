using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{

    public Slider Volume;
    public float temp;
    public AudioSource test;
    // Update is called once per frame
    void Update()
    {
        temp = Volume.value;
        test.volume = temp;
    }
}
