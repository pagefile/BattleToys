using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Input component to put on any game object that uses any kind of input for it's behavior
// Not just for controllers. Network, AI, whatever. It's to decouple the game object
// from the controller logic.
public class InputComponent : MonoBehaviour
{
    public delegate void AxisBinding(float input);
    public delegate void ButtonBinding(bool input);
    // TODO: Add a binding for button state change (ie OnButtonDown, OnButtonUp)


    // Dictionaries for storing delgates
    private Dictionary<string, AxisBinding> AxisInputs = new Dictionary<string, AxisBinding>();
    private Dictionary<string, ButtonBinding> ButtonInputs = new Dictionary<string, ButtonBinding>();

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnDestroy()
    {
        AxisInputs.Clear();
        ButtonInputs.Clear();
    }

    public void ExecuteInput(string inputName, float value)
    {
        AxisBinding axis = AxisInputs[inputName];
        // Exception handling here? I don't think it should happen but I'm paranoid that
        // a delegate will be pointing to a bad/cleanedup function somewhere and
        // that won't be cool
        if(axis != null)
        { 
            axis(value);
        }
    }

    public void ExecuteInput(string inputName, bool value)
    {
        ButtonBinding button = ButtonInputs[inputName];
        // Exception handling here? I don't think it should happen but I'm paranoid that
        // a delegate will be pointing to a bad/cleanedup function somewhere and
        // that won't be cool
        if(button != null)
        {
            button(value);
        }
    }

    public void BindInput(string inputName, AxisBinding binding)
    {
        AxisInputs.Add(inputName, binding);
    }

    public void BindInput(string inputName, ButtonBinding binding)
    {
        ButtonInputs.Add(inputName, binding);
    }
}
