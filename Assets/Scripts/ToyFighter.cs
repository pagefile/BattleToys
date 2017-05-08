using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyFighter : MonoBehaviour {
    // Editor Variables
    [SerializeField]
    private float Thrust = 500.0f;

    [SerializeField]
    private float BoostThrust = 500.0f;

    [SerializeField]
    private float RollTorque = 10.0f;

    [SerializeField]
    private float YawTorque = 10.0f;

    [SerializeField]
    private float PitchTorque = 10.0f;

    [SerializeField]
    private float SpeedAngularReduction = 0.3f;       // How much drag at max speed is applied

    // I don't really like it, but these game objects are used as positional spawns for projectiles. It works. Dunno if there's a better method. I bet there is
    [SerializeField]
    private Transform GunPort1;                // bleh
    [SerializeField]
    private Transform GunPort2;                // bleh 2: Return of bleh

    [SerializeField]
    float GunCycleRate = 0.5f;               // How many seconds it takes before it's ready to fire again

    [SerializeField]
    Projectile GunProjectile;

    // internal variables
    private float maxSpeed = 0.0f;              // Used for determining angular drag

    // Control variables
    private float throttleChange = 0.0f;
    private float throttle = 0.0f;              // Scaled with thrust to make the jet go
    private float pitch = 0.0f;
    private float yaw = 0.0f;
    private float roll = 0.0f;
    private float gunCycleTime = 0.0f;
    private bool triggerDown = false;
    private bool gunFire = false;
    private bool boosting = false;

    // This is so hacky, but I want to get things working first. I'd have things like player input and
    // the shooty bits in their own classes and what not but I just want to get it all done first

	// Use this for initialization
	void Start () 
    {
        Rigidbody rBody = GetComponent<Rigidbody>();
        rBody.useGravity = false;
        maxSpeed = BoostThrust / rBody.mass / rBody.drag;     // I have no idea if this works. It just seems workable

        InputComponent input = GetComponent<InputComponent>();
        if(input != null)
        {
            input.BindInput("Pitch", SetPitch);
            input.BindInput("Yaw", SetYaw);
            input.BindInput("Roll", SetRoll);
            input.BindInput("Primary Fire", TriggerDown);
            input.BindInput("Boost", Boosting);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(triggerDown && gunCycleTime < 0.0f)
        {
            gunCycleTime = GunCycleRate;
            gunFire = true;
        }

        // Process systems
        gunCycleTime -= Time.deltaTime;
	}

    void FixedUpdate()
    {
        // Apply angular drag based on speed: faster speed == more drag
        Rigidbody rBody = GetComponent<Rigidbody>();
        Vector3 currentVelocity = rBody.velocity;
        float currentSpeed = currentVelocity.magnitude;
        // Don't wanna end up accidentally extrapolating
        currentSpeed = Mathf.Min(maxSpeed, currentSpeed);
        float speedAngleScalar = 0.0f;
        float currentThrust = Thrust;

        if(currentSpeed != 0.0f)
        {
            // Unlikely, but it's good to check
            speedAngleScalar = 1 - ((currentSpeed / maxSpeed) * SpeedAngularReduction);
        }

        if(boosting)
        {
            currentThrust = BoostThrust;
        }

        // Apply thrust
        rBody.AddRelativeForce(Vector3.forward * currentThrust);

        // Shoot guns
        if(gunFire)
        {
            gunFire = false;
            // Why not a static function though? Does TransformPoint do anything to the transform you call it on?
            Projectile one = Instantiate(GunProjectile, GunPort1.position, rBody.transform.rotation);
            Projectile two = Instantiate(GunProjectile, GunPort2.position, rBody.transform.rotation);
            one.InheritVelocity = rBody.velocity;
            two.InheritVelocity = rBody.velocity;
        }

        // Apply turning torque
        rBody.AddRelativeTorque(Vector3.forward * RollTorque * roll * speedAngleScalar);
        rBody.AddRelativeTorque(Vector3.right * PitchTorque * pitch * speedAngleScalar);
        rBody.AddRelativeTorque(Vector3.up * YawTorque * yaw * speedAngleScalar);
    }

    // Input
    public void SetPitch(float val)
    {
        pitch = val;
    }

    public void SetYaw(float val)
    {
        yaw = val;
    }

    public void SetRoll(float val)
    {
        roll = val;
    }

    public void TriggerDown(bool val)
    {
        triggerDown = val;
    }

    public void ThrottleChanged(float val)
    {
        throttleChange = val;
    }

    public void Boosting(bool val)
    {
        boosting = val;
    }
}
