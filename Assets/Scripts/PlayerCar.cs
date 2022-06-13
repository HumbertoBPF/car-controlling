using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    // Flag to determine if the player can move the car
    [SerializeField]
    private bool _isEnabled = true;
    public bool IsEnabled { get { return _isEnabled; } set { _isEnabled = value; } }
    // Initial position of the car on the screen
    [SerializeField]
    private float _xInitial = 0.0f;
    [SerializeField]
    private float _yInitial = 0.0f;
    [SerializeField]
    private float _zInitial = 0.0f;
    // Car movement variables
    [SerializeField]
    private float _speedTranslation = 5.0f;
    [SerializeField]
    private float _speedRotation = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(_xInitial, _yInitial, _zInitial);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isEnabled)
        {
            ControlCarByKeys();
        }
    }

    void ControlCarByKeys()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float clockwiseRotation = Input.GetAxis("Vertical");
        transform.Translate(horizontalInput * Vector3.right * _speedTranslation * Time.deltaTime);
        transform.Rotate(horizontalInput * clockwiseRotation * Vector3.forward * _speedRotation * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Parking Car 1" || collider.tag == "Parking Car 2" || collider.tag == "Wall")
        {
            _isEnabled = false;
        }
    }

}
