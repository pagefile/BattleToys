using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// I need to change either how this handles the objectives or how DestroyObjective works, since currently
// the DestroyObjective component is destroyed with the object. Not the best thing to do
public class GameMode : MonoBehaviour
{
    [SerializeField]
    List<Objective> ObjectiveList;

    // HACK: quick and dirty. Like a hobo
    static private GameMode _instance = null;
    static public GameMode Instance
    {
        get { return _instance; }
        private set { }
    }

    // Internal
    int totalCriticalSuccess = 0; // HACK: Hackish, but not nearly as much as what I had before
    int currentCirticalSuccess = 0;

	// Use this for initialization
	void Start ()
    {
		if(_instance == null)
        {
            // It gets the job done...
            _instance = this;
        }

        // Listen to objective events
        for(int i = 0; i < ObjectiveList.Count; i++)
        {
            Objective obj = ObjectiveList[i];
            if (obj.IsCritical)
            {
                totalCriticalSuccess++;
            }
            obj.UpdateObjective += UpdateObjective;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        // So...this is terrible, and I hate it, but I don't want to spend too much
        // time designing a more robust way to handle this.
        // I feel like ideally I would use the event system, but since I have work on
        // two days of the jam I really don't have the time to read up on it :(
        bool missionPassed = true;  // ick
        for(int i = 0; i < ObjectiveList.Count; i++)
        {
            Objective obj = ObjectiveList[i];
            if(obj.IsCritical && obj.CurrentState == Objective.State.Failure)
            {
                // do some sort of game over thing
            }
            else if(obj.IsCritical && obj.CurrentState == Objective.State.Active)
            {
                missionPassed = false; // ick
            }
        }
        if(missionPassed == true)   // ick
        {
            // do some sort of victory dance thing
        }
	}

    public void UpdateObjective(Objective obj, Objective.State state)
    {
        if(state == Objective.State.Success && obj.IsCritical)
        {
            currentCirticalSuccess++;
        }
        if(currentCirticalSuccess == totalCriticalSuccess)
        {
            Debug.Log("You win!");
        }
    }
}
