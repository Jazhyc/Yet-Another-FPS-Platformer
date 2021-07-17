using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    
    public float health = 2f;

    private AudioSource Audio;
    public AudioClip hitSound;
    public GameObject deathEffect;
    public bool isDead = false;

    public float deathTime = 3f;

    // Parent effects to game object for cleanliness
    private GameObject Particles;

    //Public function to deal damage
    public void TakeDamage(float damage, float knockback, RaycastHit hit) {
        health -= damage;
        Audio.PlayOneShot(hitSound, 0.9f);

        if (health <= 0f || transform.position.y < -10f) {
            isDead = true;
            // Ragdoll Code?
            if (hit.rigidbody != null) {
                hit.rigidbody.freezeRotation = false;
                hit.rigidbody.AddForce(-hit.normal * knockback);
            }
            Invoke("Die", deathTime);
        }
    }

    void Die() {
        GameObject explosion = Instantiate(deathEffect, gameObject.transform);
        explosion.transform.parent = null;

        explosion.transform.parent = Particles.transform;

        Destroy(explosion, 2);
        Destroy(gameObject);
    }

    void Start()
    {
        Audio = GetComponent<AudioSource>();
        Particles = GameObject.Find("Particles");
    }
}
