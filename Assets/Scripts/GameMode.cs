using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameMode : MonoBehaviour {

    [SerializeField]
    private Text StatusText;

    // HACK: The player should be spawned by the game mode and kept track of that way....
    // But this way is easier...and gross
    [SerializeField]
    ToyFighter Player;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(StatusText)
        {
            float speed = Player.GetComponent<Rigidbody>().velocity.magnitude;
            StatusText.text = "Speed: " + speed.ToString("0.00");
        }
	}
}
