using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //public float moveSpeed;
    //private Vector3 moveVelocity;
    private Rigidbody myRigidbody;
    private bool thrusting;
    private Vector3 moveInput;
    public float thrustSpeed = 1;

    private float turnDirection;
    public float turnSpeed = 1;

    private Camera mainCamera;
    public GunController theGun;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Input and move speed
        //moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        //moveVelocity = moveInput * moveSpeed;

        thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetMouseButton(0);

        // Set a point in space for the camera to look at on a ground plane
        // Ray from camera to the ground plane
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        // If the ray hits the ground, store the ray length
        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            // Debug line so we can see it in the editor
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            Vector3 mousePos = Input.mousePosition;
            turnDirection = Vector3.SignedAngle(myRigidbody.position, pointToLook, Vector3.up);

            if (Mathf.Sign(turnDirection) == -1)
            {
                myRigidbody.AddTorque(Vector3.up * -turnSpeed);
            }
            else if (Mathf.Sign(turnDirection) == 1)
            {
                myRigidbody.AddTorque(Vector3.up * turnSpeed);
            }

        }

        theGun.isFiring = Input.GetMouseButton(1);

    }

    void FixedUpdate()
    {
        // Trad rigid movement
        //myRigidbody.velocity = moveVelocity;

        if (thrusting)
        {
            myRigidbody.AddForce(transform.forward * thrustSpeed);
        }

    }
}
