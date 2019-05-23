using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {
    private GameObject menuScriptGameObject;
    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        menuScriptGameObject = GameObject.FindWithTag("MainCamera");
        rb = GetComponent<Rigidbody>();

        menuScriptGameObject.GetComponent<Menu>();
	}
	
	// Update is called once per frame
	void Update () {
        

        if(!menuScriptGameObject.GetComponent<Menu>().gameOver){
            rb.AddForce(0, 0, -menuScriptGameObject.GetComponent<Menu>().forwardForce * Time.deltaTime);
        }
        else{
            rb.velocity = Vector3.zero;
            menuScriptGameObject.GetComponent<Menu>().cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }


        if(transform.position.z<-7f){
            Destroy(gameObject);
        }



	}
	private void OnCollisionEnter(Collision collision)
	{
        
        if(collision.collider.tag=="Player"){

            menuScriptGameObject.GetComponent<Menu>().gameOver = true;
        }
	}
}
