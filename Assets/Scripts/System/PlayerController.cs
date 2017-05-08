using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    // HACK: Kinda hacky, but it'll work for now. I just want to decouple the Input manager
    // calls from the objects so technically anything can control them without a major
    // refactor. At the very least, Target should be a field or something public so it
    // can be changed by code for different reasons (character change, character spawn,
    // joining a network game, things like that)

    [SerializeField]
    private InputComponent Target;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Target == null)
        {
            return;
        }
        // For now it only cares about the fighter stuff
        Target.ExecuteInput("Boost", Input.GetButton("Boost"));
        Target.ExecuteInput("Pitch", Input.GetAxis("Pitch") + Input.GetAxis("Axis Pitch"));
        Target.ExecuteInput("Roll", -Input.GetAxis("Roll") + -Input.GetAxis("Axis Roll"));
        Target.ExecuteInput("Yaw", Input.GetAxis("Yaw"));
        Target.ExecuteInput("Primary Fire", Input.GetButton("Primary Fire"));
    }
}
