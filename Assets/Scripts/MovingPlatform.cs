using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{   

    public int moveDistance = 10;

    private Vector3 startPosition;
    public bool moveLeft = true;
    public float platformSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveLeft) {
            transform.Translate(Vector3.left * platformSpeed * Time.deltaTime);
        } else {
            transform.Translate(Vector3.right * platformSpeed * Time.deltaTime);
        }

        if (transform.position.x < startPosition.x - moveDistance) {
			moveLeft = false;
		} else if (transform.position.x > startPosition.x + moveDistance) {
			moveLeft = true;
		}
    }
}
