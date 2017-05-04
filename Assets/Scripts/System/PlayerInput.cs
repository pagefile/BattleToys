using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{

    // HACK: Kinda hacky, but it'll work for now. I just want to decouple the Input manager
    // calls from the objects so technically anything can control them without a major
    // refactor

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
        Target.ExecuteInput("Throttle", Input.GetAxis("Throttle"));
        Target.ExecuteInput("Pitch", Input.GetAxis("Pitch") + Input.GetAxis("Axis Pitch"));
        Target.ExecuteInput("Roll", -Input.GetAxis("Roll") + -Input.GetAxis("Axis Roll"));
        Target.ExecuteInput("Yaw", Input.GetAxis("Yaw"));
        Target.ExecuteInput("Primary Fire", Input.GetButton("Primary Fire"));
    }
}
