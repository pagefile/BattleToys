using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// HACK: Very hacky. There's a better way to do this, and it's on the tip of my tongue,
// but I don't have the time for it right now
public class HUD : MonoBehaviour
{

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
        if (StatusText && Player)
        {
            float speed = Player.GetComponent<Rigidbody>().velocity.magnitude;
            StatusText.text = "Speed: " + speed.ToString("0.00") + "\nHealth: " + Player.GetComponent<Destructable>().CurrentHealth.ToString("0");
        }
    }
}
