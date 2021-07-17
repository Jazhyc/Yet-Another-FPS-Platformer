using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{   

    private GameObject Player;
    public float speed = 0.1f;
    private float step;
    private bool isDead;

    private float distance;
    public float minDistance = 2f;
    public float engageDistance = 50f;

    // Start is called before the first frame update
    void Start()
    {   
        // Get properties of player object once scene loads
        Player = GameObject.Find("FPS Player");
    }

    // Update is called once per frame
    void Update()
    {   

        isDead = GetComponent<Target>().isDead;
        distance = Vector3.Distance(transform.position, Player.transform.position);

        if (!isDead && distance > minDistance && distance < engageDistance) {
            step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, step);
        }
    }
}
