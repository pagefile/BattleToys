using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DestroyObjective : Objective, IDestroyedEventTarget
{
    // Internal
    private Destructable objective = null;

	// Use this for initialization
	void Start () 
    {
        objective = GetComponent<Destructable>();
        if(objective == null)
        {
            // It can't be destroyed. No bueno
            OnUpdateObjective(Objective.State.Failure);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // IDestroyedEventTarget
    public void OnDestroyed()
    {
        OnUpdateObjective(Objective.State.Success);
    }
}
