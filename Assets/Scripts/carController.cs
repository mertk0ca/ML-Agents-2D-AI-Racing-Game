using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    [Header("Car Settings")]
    //driftmode 0.98
    //racingmode 0.35
    public float driftMode = 0.20f;
    public float accelerationConst = 50f;
    public float turnConst = 10f;
    public float maxSpeed = 15;
    public float kmh;

    public float accelerationInput = 0;
    public float turnInput = 0;
    public float rotationAngle = 0;
    public float velocityVsUp = 0;

    public Rigidbody2D carRigidbody2D;

    void OnGUI()
    {
        kmh = GetComponent<Rigidbody2D>().velocity.magnitude * 20;
    }

    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        ApplyEngineForce();
        ApplySteering();
        RemoveSideForce();
    }

    void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;

        if (velocityVsUp < -maxSpeed * 0.3f && accelerationInput < 0)
            return;

        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;

        if (accelerationInput == 0)
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 2f, Time.fixedDeltaTime);
        else carRigidbody2D.drag = 0;

        if (Input.GetKeyDown("down"))
        {
            accelerationInput = -1;
        }

        if (accelerationInput == -1)
            carRigidbody2D.drag = 1f;

        Vector2 engineForceVector = transform.up * accelerationInput * accelerationConst;

        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);

    }

    void ApplySteering()
    {
        float minTurnSpeed = (carRigidbody2D.velocity.magnitude / 1);
        minTurnSpeed = Mathf.Clamp01(minTurnSpeed);

        rotationAngle -= turnInput * turnConst * minTurnSpeed;

        carRigidbody2D.MoveRotation(rotationAngle);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        turnInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    void RemoveSideForce()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftMode;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //UnityEngine.Debug.Log(other.gameObject.name);
        //UnityEngine.Debug.Log(maxSpeed);

        if (other.gameObject.name == "trackRoad" || other.gameObject.name == "startLine" || other.gameObject.name == "checkpoint1" || other.gameObject.name == "checkpoint2" || other.gameObject.tag == "Curbs")
        {
            maxSpeed = 15;
            driftMode = 0.35f;
        }
        else
        {
            carRigidbody2D.drag = 1;
            maxSpeed = 5;
            driftMode = 0.98f;
        }

        if (other.gameObject.tag == "Pits")
        {
            maxSpeed = 5;
            driftMode = 0.35f;
        }

    }
}


//carController'a MoveToTargetAgent'tan SetInput kýsmýna deðiþkenler çekilmeli
