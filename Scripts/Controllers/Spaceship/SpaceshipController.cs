using UnityEngine;
using System.Collections;

public class SpaceshipController  : MonoBehaviour
{

    public bool disabled = false;

    #region public variables movement

    //Network variables
    public float yaw;
    public float pitch;
    public float roll;

    //public 
    public float maxSpeed = 10.0f;
    public float maxHorizontalSpeed = 3.0f;
    public float maxVerticalSpeed = 3.0f;

    public float accelerationMultiplier = 10f;

    public float turnSpeed = 10f;
    public float maxTurn = 20f;

    public float engine = 0f;
    public float engineAcceleration = 0.2f;

    public float xVel;
    public float yVel;
    public float zVel;

    public Vector3 pointVelocity;

    public float maxAllowedSpeed;
    public float boostTimer = 10.0f;
    public float boostCooldown = 20.0f;
    public bool flightBoost = false;
    #endregion

    #region private variables

    float newVel;
    float _acceleration = 0f;
    float _horizontalAcceleration = 0f;
    float _verticalAcceleration = 0f;

    float tmpMaxSpeed;
    float tmpAccelerationMultiplier;

    float tmpCooldown;

    GameObject _spaceship;
    Rigidbody _body;

    bool flightAssist = true;
    bool lerpEngine;

    public float _massMultiplier;

    public Vector3 AngularVelocity;
    #endregion

    #region MonoBehaviour Methods

    // Use this for initialization
    void Start()
    {
        _spaceship = gameObject;
        _body = _spaceship.GetComponent<Rigidbody>();
        _massMultiplier = _body.mass;

        tmpMaxSpeed = maxSpeed;
        tmpAccelerationMultiplier = accelerationMultiplier;
        tmpCooldown = boostCooldown;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AngularVelocity = _body.angularVelocity;

        if (disabled)
        {
            return;
        }

        ToggleFligtAssist();

        if (flightAssist)
        {
            _body.AddTorque(-_body.angularVelocity * _massMultiplier);
        }
            
        xVel = transform.InverseTransformDirection(_body.velocity).x;
        yVel = transform.InverseTransformDirection(_body.velocity).y;
        zVel = transform.InverseTransformDirection(_body.velocity).z;

        SetThrottle();
        HorizontalMovement();
        VerticalMovement();

        Yaw();
        Pitch();
        Roll();

        ApplyThrottle();
        ApplyTurn();

        if (flightAssist)
        {
            Stabilization();
        }

        DetectFlightBoost();
        Boost();
        Cooldown();

        if (lerpEngine)
        {
            engine += 0.05f;
            if(engine >= 1)
            {
                engine = 1;
                lerpEngine = false;
            }
        }
    }

    #endregion

    #region public methods

    public void Disabled()
    {
        disabled = true;
        if (disabled)
        {
            engine = 0f;
        }
    }

    public void Killed()
    {
        _body.velocity = Vector3.zero;
        _body.angularVelocity = Vector3.zero;
    }

    public void Enabled()
    {
        disabled = false;
    }

    #endregion

    #region private methods

    void SetThrottle()
    {
        if (Input.GetButton("Vertical") || Input.GetAxis("Vertical") > 0)
        {
            engine += engineAcceleration * Time.deltaTime * Input.GetAxis("Vertical");

            if (engine > 1)
            {
                engine = 1;
            }
            else if (engine < 0)
            {
                engine = 0;
            }
        }

        maxAllowedSpeed = maxSpeed * engine;

        if (zVel < maxAllowedSpeed)
        {
            _acceleration = engineAcceleration * maxSpeed * Time.deltaTime * accelerationMultiplier * _massMultiplier;
        }
        else if (zVel > maxAllowedSpeed && flightAssist)
        {
            newVel = zVel - maxAllowedSpeed;
            _acceleration = newVel * -5;
        }else if(zVel > maxSpeed)
        {
            newVel = zVel - maxAllowedSpeed;
            _acceleration = newVel * -0.1f;
        }

    }

    void HorizontalMovement()
    {

        _horizontalAcceleration = Input.GetAxis("Strafe") * Time.deltaTime * 200 * _massMultiplier;

        if(xVel > maxHorizontalSpeed)
        {
            newVel = xVel - maxHorizontalSpeed;
            _horizontalAcceleration = newVel * -1;
        }else if(xVel < maxHorizontalSpeed * -1)
        {
            newVel = xVel * -1 - maxHorizontalSpeed;
            _horizontalAcceleration = newVel;
        } 
    }

    void VerticalMovement() {

        _verticalAcceleration = Input.GetAxis("Up") * Time.deltaTime * 200 * _massMultiplier;

        if (yVel > maxVerticalSpeed)
        {
            newVel = yVel - maxVerticalSpeed;
            _verticalAcceleration = newVel * -1;
        }
        else if (yVel < maxVerticalSpeed * -1)
        {
            newVel = yVel * -1 - maxVerticalSpeed;
            _verticalAcceleration = newVel;
        }
    }

    void ApplyThrottle()
    {
        _body.AddForce(_spaceship.transform.forward * _acceleration);
        _body.AddForce(_spaceship.transform.up * _verticalAcceleration);
        _body.AddForce(_spaceship.transform.right * _horizontalAcceleration);
    }

    void Yaw()
    {
        yaw = turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal") * _massMultiplier * 12.5f;
        if (yaw > maxTurn)
        {
            yaw = maxTurn;
        }
        else if (yaw < maxTurn * -1)
        {
            yaw = maxTurn * -1;
        }
    }

    void Pitch()
    {
        pitch = turnSpeed * Time.deltaTime * Input.GetAxis("Mouse Y") * -1 * _massMultiplier * 2;
        if (pitch > maxTurn)
        {
            pitch = maxTurn;
        }else if(pitch < maxTurn * -1)
        {
            pitch = maxTurn * -1;
        }
    }

    void Roll()
    {
        roll = turnSpeed * Time.deltaTime * Input.GetAxis("Mouse X") * -1 * _massMultiplier * 4;
        if (roll > maxTurn)
        {
            roll = maxTurn;
        }
        else if (roll < maxTurn * -1)
        {
            roll = maxTurn * -1;
        }
    }

    void ApplyTurn()
    {
        _body.AddTorque(gameObject.transform.up * yaw);
        _body.AddTorque(gameObject.transform.forward * roll);
        _body.AddTorque(gameObject.transform.right * pitch);
    }

    void Stabilization()
    {
        if (!Input.GetButton("Up"))
        {
            _body.AddForce(_spaceship.transform.up * yVel * -1 * _massMultiplier);
        }
        if (!Input.GetButton("Strafe"))
        {
            _body.AddForce(_spaceship.transform.right * xVel * -1 * _massMultiplier);
        }
    }

    #endregion

    void ToggleFligtAssist()
    {
        if(Input.GetButtonDown("ToggleFlightAssist")) flightAssist = !flightAssist;
    }

    void DetectFlightBoost()
    {
        if ((Input.GetButton("Fire2") || Input.GetAxis("Fire2") > 0) && boostTimer > 0)
        {
            FlightBoost();
        }
        else if (Input.GetButtonUp("Fire2") || Input.GetAxis("Fire2") == 0)
        {
            EndBoost();
        }
    }

    void FlightBoost()
    {
        if (!flightBoost && boostTimer > 0)
        {
            maxSpeed *= 1.5f;
            accelerationMultiplier *= 3;
            flightBoost = true;
            //Lerp Engine
            lerpEngine = true;
        }
    }

    void EndBoost()
    {
        maxSpeed = tmpMaxSpeed;
        accelerationMultiplier = tmpAccelerationMultiplier;
        flightBoost = false;
    }

    void Cooldown()
    {
        if (!flightBoost && boostTimer < 10.0f)
        {
            boostCooldown -= Time.deltaTime;
            if (boostCooldown < 0)
            {
                boostCooldown = 20.0f;
                boostTimer = 10.0f;
            }
        }
        else if (flightBoost)
        {
            boostCooldown = 20.0f;
        }
    }
    
    void Boost()
    {
        if (flightBoost)
        {
            boostTimer -= Time.deltaTime;
        }
        if(boostTimer < 0)
        {
            EndBoost();
        }
    }
}