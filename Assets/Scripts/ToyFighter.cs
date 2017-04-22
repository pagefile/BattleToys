using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyFighter : MonoBehaviour {
    // Editor Variables
    [SerializeField]
    private float MaxThrust = 500.0f;

    [SerializeField]
    private float RollTorque = 10.0f;

    [SerializeField]
    private float YawTorque = 10.0f;

    [SerializeField]
    private float PitchTorque = 10.0f;

    [SerializeField]
    private float SpeedAngularReduction = 0.3f;       // How much drag at max speed is applied

    [SerializeField]
    private float MinThrottle = 0.3f;           // The slowest the fighter is allowed to fly

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
    private float throttleRate = 0.3f;           // How fast the throttle changes with input

    // Control variables
    private float throttle = 0.0f;              // Scaled with thrust to make the jet go
    private float pitch = 0.0f;
    private float yaw = 0.0f;
    private float roll = 0.0f;
    private float gunCycleTime = 0.0f;
    private bool gunFire = false;

    // This is so hacky, but I want to get things working first. I'd have things like player input and
    // the shooty bits in their own classes and what not but I just want to get it all done first

	// Use this for initialization
	void Start () 
    {
        Rigidbody rBody = GetComponent<Rigidbody>();
        rBody.useGravity = false;
        maxSpeed = MaxThrust / rBody.mass / rBody.drag;     // I have no idea if this works. It just seems workable
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Read input
        // It's just easier to combine it all
        float throttleChange = Input.GetAxis("Throttle");
        pitch = Input.GetAxis("Pitch") + Input.GetAxis("Axis Pitch");
        yaw = Input.GetAxis("Yaw");
        roll = -Input.GetAxis("Roll") + -Input.GetAxis("Axis Roll");
        if(gunCycleTime < 0.0f && Input.GetAxis("Primary Fire") > 0.0f)
        {
            gunCycleTime = GunCycleRate;
            gunFire = true;
        }

        // Process systems
        gunCycleTime -= Time.deltaTime;

        // Process input
        throttle += throttleChange * Time.deltaTime;
        throttle = Mathf.Clamp(throttle, 0.0f, 1.0f);
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
        if(currentSpeed != 0.0f)
        {
            // Unlikely, but it's good to check
            speedAngleScalar = 1 - ((currentSpeed / maxSpeed) * SpeedAngularReduction);
        }

        // Apply thrust
        float effectiveThrottle = ((1.0f - MinThrottle) * throttle) + MinThrottle;
        rBody.AddRelativeForce(Vector3.forward * effectiveThrottle * MaxThrust);

        // Shoot guns
        if(gunFire)
        {
            gunFire = false;
            // Why not a static function though? Does TransformPoint do anything to the transform you call it on?
            Vector3 port1Pos = transform.TransformPoint(GunPort1.position);
            Vector3 port2Pos = transform.TransformPoint(GunPort2.position);
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
}
