using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarObstacleGame : PlayerCar
{
    /// <summary>
    /// Horizontal input translates the car along the driver's vision axis. Vertical input controls the car's
    /// rotation.
    /// </summary>
    protected override void ControlCarByKeys()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float clockwiseRotation = Input.GetAxis("Vertical");
        transform.Translate(horizontalInput * Vector3.right * _speedTranslation * Time.deltaTime);
        transform.Rotate(horizontalInput * clockwiseRotation * Vector3.forward * _speedRotation * Time.deltaTime);
    }
}
