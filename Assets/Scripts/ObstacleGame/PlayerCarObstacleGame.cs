using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarObstacleGame : PlayerCar
{
    protected override void ControlCarByKeys()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float clockwiseRotation = Input.GetAxis("Vertical");
        transform.Translate(horizontalInput * Vector3.right * _speedTranslation * Time.deltaTime);
        transform.Rotate(horizontalInput * clockwiseRotation * Vector3.forward * _speedRotation * Time.deltaTime);
    }
}
