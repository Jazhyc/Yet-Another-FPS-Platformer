using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using TMPro;

public class PlayerHealth : MonoBehaviour
{   

    public AudioClip hitSound;

    private AudioSource Audio;

    public TMP_Text scoreText;
    private int playerhealth = 100;

    private float immuneTime = 1f;
    private float ImmuneFrames;

    private Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(Collision collision) {
        // Play hit sound and decrease player health by 1
        if (Time.time > ImmuneFrames) {

            ImmuneFrames = Time.time + immuneTime;

            Debug.Log("Player Took Damage");
            playerhealth-=25;
            Audio.PlayOneShot(hitSound, 0.4f);

            scoreText.text = "Player Health: " + playerhealth +"%";

            // If below zero, then reset the scene
            if (playerhealth <= 0) { 
                SceneManager.LoadScene(scene.name);
            }
        }
    }
}
