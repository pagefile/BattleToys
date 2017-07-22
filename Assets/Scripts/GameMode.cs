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
        // HACK: Let player quit without resorting to alt-f4
        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
	}

    public void UpdateObjective(Objective obj, Objective.State state)
    {
        if (CheckCriticalObjectives() == Objective.State.Success)
        {
            Debug.Log("You win!");
        }
    }

    // Check to see if mission objectives are met
    // Returns objective state enum
    // Active - Game is still ongoing
    // Success - Critical objectives complete. Win state
    // Failure - Critical objectives failed. Lose state
    private Objective.State CheckCriticalObjectives()
    {
        bool active = false;
        for(int i = 0; i < ObjectiveList.Count; i++)
        {
            Objective obj = ObjectiveList[i];
            if(obj.IsCritical)
            {
                if(obj.CurrentState == Objective.State.Failure)
                {
                    return Objective.State.Failure;
                }   
                else if(obj.CurrentState == Objective.State.Active)
                {
                    active = true;
                }
            }
        }
        // If the code gets here, no critical objective was failed. If there is still an
        // active critical objective, the level isn't over yet
        if(active)
        {
            return Objective.State.Active;
        }
        return Objective.State.Success;
    }
}
