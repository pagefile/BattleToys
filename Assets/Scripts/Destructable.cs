using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// This whole idea could be a lot more sophisticated. It could cause object deltion or a state change
// I don't have a lot of time though so it's just gonna kill the object. If I finish the core earlier
// than I expect I'll make it more robust and what not.
public class Destructable : MonoBehaviour
{
    [SerializeField]
    float MaxHealth = 100.0f;

    [SerializeField]
    private float PersistAfterDeath = 0.0f;         // How many seconds after death the game object should remain

    // Internal
    private float Health;

    public float CurrentHealth
    {
        get { return Health; }
        private set { }
    }
    // Something about an explosion effect here......later

	// Use this for initialization
	void Start ()
    {
        Health = MaxHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void DoDamage(float damage)
    {
        Health -= damage;
        
        // uh...if it's less than 0 it's ded fred
        if(Health <= 0)
        {
            // Uhm...nothing fancy I guess
            Destroy(gameObject, PersistAfterDeath < 0.1f ? 0.1f : PersistAfterDeath);
            // TODO: I feel like the game mode or something should really be notified of this object's death
            // It seems important.
            ExecuteEvents.Execute<IDestroyedEventTarget>(gameObject, null, (x, y) => x.OnDestroyed());
        }
    }

    public void Heal(float heal)
    {
        Health = Mathf.Clamp(Health + heal, Health, MaxHealth);
    }
}
