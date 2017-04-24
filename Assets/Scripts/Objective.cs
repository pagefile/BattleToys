using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    [SerializeField]
    protected string Name = "Object";     // Name or short description of objective.

    [SerializeField]
    protected string Description = "This is your objective";      // Description of objective

    [SerializeField]
    protected bool Critical = true;       // Is the objective critical? Failing a critical objective is level failure. All critical objectives must succeed for level success.

    public delegate void ObjectiveUpdateHandler(Objective obj, State state);
    public event ObjectiveUpdateHandler UpdateObjective;

    public enum State
    {
        Active,
        Success,
        Failure,
    };

    protected State objectiveState = State.Active;
    public State CurrentState
    {
        get { return objectiveState; }
        private set { objectiveState = value; }
    }

    public bool IsCritical
    {
        get { return Critical; }
        private set { }
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    protected virtual void OnUpdateObjective(State state)
    {
        CurrentState = state;
        UpdateObjective(this, state);
    }
}
