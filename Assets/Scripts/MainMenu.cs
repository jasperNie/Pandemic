using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    public GameObject audio;
    void Start(){
        DontDestroyOnLoad(audio);
    }

    public void playGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
   
    public void quitGame() {
        Debug.Log("game quit");
        Application.Quit();
    }

    public void winGame(){
        GameObject[] test = GameObject.FindGameObjectsWithTag("music");
        Destroy(test[0]);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
