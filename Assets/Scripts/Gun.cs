using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{   
    public float range = 100f;
    public float damage = 10f;
    public float firerate = 2f;
    public float knockback = 1000f;
    private float nextTimetoFire = 0f;
    public Camera gunCamera;
    public GameObject gunFlash;

    private Light gunLight;
    public ParticleSystem muzzleFlash;
    public GameObject impactFlash;

    private AudioSource Audio;
    public AudioClip gunSound;

    // Parent effects to game object for cleanliness
    private GameObject Particles;

    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        gunLight = gunFlash.GetComponent<Light>();

        Particles = GameObject.Find("Particles");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimetoFire) {

            nextTimetoFire = Time.time + 1f / firerate;
            Shoot();
            Audio.PlayOneShot(gunSound, 0.9f);
        }
    }

    void Shoot() {

        gunLight.enabled = true;
        muzzleFlash.Play();
        Invoke("StopFlash", 0.1f);

        RaycastHit hit;
        if (Physics.Raycast(gunCamera.transform.position, gunCamera.transform.forward, out hit, range)) {

            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            if (target != null) {
                target.TakeDamage(damage, knockback, hit);
            }

            GameObject impact = Instantiate(impactFlash, hit.point, Quaternion.LookRotation(hit.normal));
            impact.transform.parent = Particles.transform;
            Destroy(impact, 1);
        }
    }

    void StopFlash() {
        gunLight.enabled = false;
    }
}
