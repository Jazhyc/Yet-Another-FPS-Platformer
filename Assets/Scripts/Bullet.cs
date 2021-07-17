using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   

    private float bulletSpeed = 15f;
    private GameObject Player;
    private Vector3 playerPosition;
    private Vector3 normalizedDirection;

    // Start is called before the first frame update
    void Start()
    {   
        Player = GameObject.Find("FPS Player");
        playerPosition = Player.transform.position;

        // Gets unit vector from player and bullet position at instantiation
        normalizedDirection = (playerPosition - transform.position).normalized;

        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += normalizedDirection * bulletSpeed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision) {

        PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();

        if (playerHealth != null) {
            playerHealth.Damage(collision);
        }

        Destroy(gameObject);
    }
}
