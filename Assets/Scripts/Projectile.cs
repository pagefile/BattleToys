using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    float Speed = 100.0f;

    [SerializeField]
    float Damage = 100.0f;



	// Use this for initialization
	void Start ()
    {
        Rigidbody rBody = GetComponent<Rigidbody>();
        rBody.velocity = Vector3.zero;
        rBody.velocity = Vector3.forward * rBody.mass * Speed;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
