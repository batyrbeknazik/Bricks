using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 tch;
    private Vector3 touchPosition;
    private Rigidbody rb;
    private Vector3 direction;
    private float moveSpeed = 25f;
    public float forwardForce;
    public GameObject collidedObstacle;
    // Use this for initialization
    private void Start()
    {

        //forwardForce = 500f;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        //rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            tch = touch.position;
            tch.z = 13;
            tch.y = -4;
            touchPosition = Camera.main.ScreenToWorldPoint(tch);
            direction = (touchPosition - transform.position);
            rb.velocity = new Vector3(direction.x, direction.y,direction.z) * moveSpeed;

            if (touch.phase == TouchPhase.Ended)
                rb.velocity = Vector3.zero;
        }
    }
    public void OnCollisionEnter(Collision collision)
	{
        if(collision.gameObject.tag == "obstacle"){
            collidedObstacle = collision.gameObject;
        }
	}

}

