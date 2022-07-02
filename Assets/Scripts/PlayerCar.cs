using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCar : MonoBehaviour
{
    // Flag to determine if the player can move the car
    [SerializeField]
    protected bool _isEnabled = true;
    public bool IsEnabled { get { return _isEnabled; } set { _isEnabled = value; } }
    // Initial position of the car on the screen
    [SerializeField]
    protected float _xInitial = 0.0f;
    [SerializeField]
    protected float _yInitial = 0.0f;
    [SerializeField]
    protected float _zInitial = 0.0f;
    // Car movement variables
    [SerializeField]
    protected float _speedTranslation = 5.0f;
    public float SpeedTranslation { get { return _speedTranslation; } }
    [SerializeField]
    protected float _speedRotation = 15.0f;

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
    /// <summary>
    /// Method describing how the user can control the car movement.
    /// </summary>
    protected abstract void ControlCarByKeys();

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Parking Car 1" || collider.tag == "Parking Car 2" || collider.tag == "Wall")
        {
            _isEnabled = false;
        }
    }

}
