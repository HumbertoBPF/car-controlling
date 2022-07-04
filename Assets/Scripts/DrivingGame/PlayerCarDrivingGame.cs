using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarDrivingGame : PlayerCar
{
    private float _accelerationPerFrame = 0.01f;

    protected override void ControlCarByKeys()
    {
        SpeedControl();
        DirectionControl();
        // The car does not moves along the x and z axis.
        float currentY = transform.position.y;
        transform.position = new Vector3(_xInitial, currentY, _zInitial);
    }
    /// <summary>
    /// Increases and reduces the speed of the car when right and left arrow keys respectively are pressed.
    /// The speed lies in the interval [5,10].
    /// </summary>
    void SpeedControl()
    {
        float verticalInput = Input.GetAxis("Vertical");

        if (_speedTranslation < 10.0f && verticalInput > 0)
        {
            _speedTranslation += _accelerationPerFrame;
        }

        if (_speedTranslation > 5.0f && verticalInput < 0)
        {
            _speedTranslation -= _accelerationPerFrame;
        }
    }
    /// <summary>
    /// Moves the car to its left or its writing depending on the vertical arrow keys that are being pressed.
    /// A rotation transformation is also applied in order to make this movement more realistic.
    /// </summary>
    void DirectionControl()
    {
        float horizontalInput = -Input.GetAxis("Horizontal");
        transform.Translate(horizontalInput * Vector3.up * _speedTranslation * Time.deltaTime);

        float rotationAngle = transform.rotation.z;

        if (horizontalInput == 0)
        {
            if (rotationAngle > 0)
            {
                transform.Rotate(-1 * Vector3.forward * _speedRotation * Time.deltaTime);
            }

            if (rotationAngle < 0)
            {
                transform.Rotate(Vector3.forward * _speedRotation * Time.deltaTime);
            }
        }
        else if (horizontalInput != 0)
        {
            transform.Rotate(horizontalInput * Vector3.forward * _speedRotation * Time.deltaTime);
        }
    }
}
