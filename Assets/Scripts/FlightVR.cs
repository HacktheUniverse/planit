using UnityEngine;
using System.Collections;

public class FlightVR : MonoBehaviour 
{
	private OVRPlayerController controller;

	private Vector3 currentSpeed;
	private float speedIncrement = 0.015f;

    //upward vars
	private const float MAX_SPEED = 0.35f;
	private const float MIN_SPEED = -0.35f;
	private const float DAMPING_MULTIPLIER = 0.92f;

    //forward vars
    private const float MAX_FORWARD_SPEED = 0.75f;
    private const float MIN_FORWARD_SPEED = 0.2f;

	private bool isGroundTouched = false; //flag for the first time player touches ground

	// Use this for initialization
	void Start () 
	{
		controller = GetComponent<OVRPlayerController>();
		currentSpeed = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey(KeyCode.Space) || OVRGamepadController.GPC_GetAxis(OVRGamepadController.Axis.RightTrigger) > 0 && currentSpeed.y < MAX_SPEED)
		{
            controller.GravityModifier = 0;
			currentSpeed.y += speedIncrement;
		}
		if(Input.GetKey(KeyCode.LeftShift) || OVRGamepadController.GPC_GetAxis(OVRGamepadController.Axis.LeftTrigger) > 0 && currentSpeed.y > MIN_SPEED && gameObject.transform.position.y > 1.5f)
		{
			currentSpeed.y -= speedIncrement;
		}

        //variable flight speed
        if (Input.GetAxis("Mouse ScrollWheel") < 0 || OVRGamepadController.GPC_GetButton(OVRGamepadController.Button.Down)) //back
        {
            if (controller.Acceleration > MIN_FORWARD_SPEED)
                controller.Acceleration -= 0.1f;
        }
		else if (Input.GetAxis("Mouse ScrollWheel") > 0 || OVRGamepadController.GPC_GetButton(OVRGamepadController.Button.Up)) //forward
        {
            if (controller.Acceleration < MAX_FORWARD_SPEED)
                controller.Acceleration += 0.1f;
        }

		transform.Translate(currentSpeed);
		currentSpeed.y *= DAMPING_MULTIPLIER;
	}

	public void DisableGravity()
	{
		controller.GravityModifier = 0;
		controller.Acceleration = 0.2f;
	}

	public void EnableGravity()
	{
		controller.GravityModifier = 0.379f;
		controller.Acceleration = 0.1f;
	}
}
