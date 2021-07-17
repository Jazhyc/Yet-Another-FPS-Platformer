using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   

    private AudioSource Audio;
    public AudioClip selectSound;

    public GameObject TitleCanvas;
    public GameObject levelCanvas;

    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Functions for buttons down here
    
    public void EndGame()
    {   
        Audio.PlayOneShot(selectSound, 0.9f);
        Application.Quit();
        Debug.Log("Game Ended");
    }

    public void Credits()
    {
        Audio.PlayOneShot(selectSound, 0.9f);
        // Place Credit Code Here
    }

    public void selectLevel()
    {   
        TitleCanvas.GetComponent<Canvas> ().enabled = false;
        levelCanvas.GetComponent<Canvas> ().enabled = true;
        levelCanvas.GetComponent<CanvasScaler> ().enabled = true;
        Audio.PlayOneShot(selectSound, 0.9f);
    }

    public void ReturnToTitle()
    {   
        levelCanvas.GetComponent<Canvas> ().enabled = false;
        TitleCanvas.GetComponent<Canvas> ().enabled = true;
        Audio.PlayOneShot(selectSound, 0.9f);
    }

    public void SelectFirstLevel()
    {
        Audio.PlayOneShot(selectSound, 0.9f);
        SceneManager.LoadScene("Scene0");
    }

    public void SelectSecondLevel()
    {
        Audio.PlayOneShot(selectSound, 0.9f);
        SceneManager.LoadScene("Scene1");
    }

    public void SelectThirdLevel()
    {
        Audio.PlayOneShot(selectSound, 0.9f);
        SceneManager.LoadScene("Scene2");
    }

    public void Settings(){
        // Rick Roll
        Application.OpenURL ("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
    }
}
