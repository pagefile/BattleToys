using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    // Editor
    [SerializeField]
    float Speed = 100.0f;

    [SerializeField]
    float Damage = 10.0f;

    [SerializeField]
    float LifeTime = 5.0f;

    // Set on spawn
    public Vector3 InheritVelocity { get; set; }

    // Internal
    bool dead = false;

	// Use this for initialization
	void Start ()
    {
        Rigidbody rBody = GetComponent<Rigidbody>();
        rBody.velocity = Vector3.zero;
        rBody.velocity = transform.forward * rBody.mass * Speed + InheritVelocity;
        Destroy(gameObject, LifeTime);
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    void OnCollisionEnter(Collision col)
    {
        if(dead)
        {
            return;
        }
        Rigidbody rBody = GetComponent<Rigidbody>();
        rBody.useGravity = true;

        // Check to see if it's something that can damage
        Destructable dest = col.gameObject.GetComponent<Destructable>();
        if(dest == null)
        {
            return;
        }
        dest.DoDamage(Damage);
        dead = true;
    }
}
