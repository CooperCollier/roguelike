using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	//--------------------------------------------------------------------------------

	public GameObject PlayButton;
    public GameObject QuitButton;

	//--------------------------------------------------------------------------------
    
    public void Play() {
    	SceneManager.LoadScene(1);
    }

    public void Quit() {
        Application.Quit();
    }

    //--------------------------------------------------------------------------------

}