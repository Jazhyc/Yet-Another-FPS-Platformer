using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollider : MonoBehaviour
{   

    public GameObject player;
    private SC_FPSController playerScript;
    
    public GameObject victoryText;

    private AudioSource Audio;
    public AudioClip winSound;

    // This bool is used for determining whether escape will return to title or not
    private bool victory = false;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<SC_FPSController>();
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            if (victory) {
                SceneManager.LoadScene("MainMenu");
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    void OnCollisionEnter(Collision other){
        if (other.gameObject.tag == "MovingPlatform"){
            player.transform.parent = other.gameObject.transform;

        } else if (other.gameObject.tag == "Wall") {
            playerScript.moveDirection = Vector3.zero;

        } else if (other.gameObject.tag == "WinFlag") {
            Audio.PlayOneShot(winSound, 0.9f);
            victoryText.SetActive(true);
            victory = true;
        }
    }

    void OnCollisionExit(Collision other){
        player.transform.parent = null;
    }
}
