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

        float currentY = transform.position.y;
        transform.position = new Vector3(_xInitial, currentY, _zInitial);
    }

    void SpeedControl()
    {
        float rightButtonInput = Input.GetAxis("Horizontal");

        if (_speedTranslation < 10.0f && rightButtonInput > 0)
        {
            _speedTranslation += _accelerationPerFrame;
        }

        if (_speedTranslation > 5.0f && rightButtonInput < 0)
        {
            _speedTranslation -= _accelerationPerFrame;
        }
    }

    void DirectionControl()
    {
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(verticalInput * Vector3.up * _speedTranslation * Time.deltaTime);

        float rotationAngle = transform.rotation.z;

        if (verticalInput == 0)
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
        else if (verticalInput != 0)
        {
            transform.Rotate(verticalInput * Vector3.forward * _speedRotation * Time.deltaTime);
        }
    }
}
