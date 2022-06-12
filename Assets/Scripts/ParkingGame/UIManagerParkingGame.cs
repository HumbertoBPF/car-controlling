using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerParkingGame : UIManagerObstacleGame
{
    // Text objects(UI)
    [SerializeField]
    private Text _distanceToWallText;
    [SerializeField]
    private Text _warningText;
    // Reference objects
    private GameObject _parkingCar1;
    private GameObject _parkingCar2;
    private GameObject _wall;
    // Dimensions
    private Vector3 _parkingCarDimensions;
    // Start is called before the first frame update
    protected override void Start()
    {
        _indexScene = 1;
        _wall = GameObject.Find("Wall");
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!_playerCar.IsDamaged)
        {
            UpdateDistanceToWall();
            UpdateWarning();
        }
    }

    private void UpdateDistanceToWall()
    {
        // Checks the degree of the infraction according to the distance to the wall
        float distanceToWall = _wall.transform.position.y - (_playerCar.transform.position.y + _playerDimensions.y/2.0f);
        if (distanceToWall <= 0.50f)
        {
            _distanceToWallText.color = Color.green;
        }
        // Slight infraction
        else if (distanceToWall < 1.0f)
        {
            _distanceToWallText.color = Color.yellow;
        }
        // Serious infraction
        else
        {
            _distanceToWallText.color = Color.red;
        }
        _distanceToWallText.text = "Distance to wall: " + distanceToWall.ToString("0.00");
    }

    private void UpdateWarning()
    {
        // These two if statement assure that the references to the prefabs objects are gotten only once
        if (_parkingCar1 == null)
        {
            _parkingCar1 = GameObject.FindWithTag("Parking Car 1");
            _parkingCarDimensions = _parkingCar1.GetComponent<Renderer>().bounds.size;
        }

        if (_parkingCar2 == null)
        {
            _parkingCar2 = GameObject.FindWithTag("Parking Car 2");
        }
        // Checks if the player's car is between the two parking cars (wrt to the x coordinate)
        if ( 
            (_playerCar.transform.position.x + _playerDimensions.x/2.0f) < (_parkingCar1.transform.position.x - _parkingCarDimensions.x/2.0f)
            && (_playerCar.transform.position.x - _playerDimensions.x / 2.0f) > (_parkingCar2.transform.position.x + _parkingCarDimensions.x / 2.0f) 
            )
        {
            _warningText.text = "";
        }
        else
        {
            _warningText.text = "Place the car between \nthe other two";
        }
    }

}
