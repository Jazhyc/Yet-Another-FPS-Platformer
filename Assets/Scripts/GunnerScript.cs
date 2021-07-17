using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerScript : MonoBehaviour
{   

    private GameObject Player;
    public GameObject Gun; 

    // Refers to prefab
    public GameObject Bullet;

    // Refers to instantiated object
    private GameObject bullet;

    private bool isDead;

    // Used to tell the enemy where the player is, y is reset so the enemy does not bend
    private Vector3 enemyDirection;
    private Vector3 gunEndPoint;
    private bool hasExecuted = false;

    // Parent projectiles to another object
    private GameObject Projectiles;

    // Variables for firing projectiles
    private float nextTimetoFire = 0f;
    private float fireRate = 2f;

    // Variables for enabling fire based on distance from player
    private float distance;
    public float engageDistance = 50f;

    // Sound Effects
    private AudioSource Audio;
    public AudioClip gunSound;


    // Start is called before the first frame update
    void Start()
    {   

        Player = GameObject.Find("FPS Player");

        Projectiles = GameObject.Find("Projectiles");
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {   
        isDead = GetComponent<Target>().isDead;
        gunEndPoint = Gun.transform.position + Gun.transform.right * 1.5f;

        enemyDirection = Player.transform.position;
        enemyDirection.y = transform.position.y;

        if (!isDead) {
            transform.LookAt(enemyDirection);

            distance = Vector3.Distance(transform.position, Player.transform.position);

            if (distance < engageDistance && Time.time >= nextTimetoFire) {

                nextTimetoFire = Time.time + 1f / fireRate;

                Audio.PlayOneShot(gunSound, 0.1f);

                bullet = Instantiate(Bullet, gunEndPoint, Gun.transform.rotation * Quaternion.Euler(0, -90, 0));
                bullet.transform.parent = Projectiles.transform;
            }

        }  else if (!hasExecuted) {
            Gun.transform.parent = null;
            Gun.GetComponent<Rigidbody>().useGravity = true;
            Gun.GetComponent<Rigidbody>().isKinematic = false;

            Destroy(Gun, 5);
            hasExecuted = true;
        }
    }
}
