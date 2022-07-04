using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerParkingGame : UIManager
{
    private float _distanceToWall;
    // Text objects(UI)
    [SerializeField]
    private Text _distanceToWallText;
    [SerializeField]
    private Text _warningText;
    [SerializeField]
    private Button _endGameButton;
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
        _gameId = 2;
        _wall = GameObject.Find("Wall");
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (_playerCar.IsEnabled)
        {
            UpdateDistanceToWall();
            UpdateWarning();
            // Checks if end game button should appear
            if (_distanceToWall < 1.0f && IsPlayerCarBetweenParkingCars() && IsPlayerCarHorizontallyAligned())
            {
                _endGameButton.gameObject.SetActive(true);
            }
            else
            {
                _endGameButton.gameObject.SetActive(false);
            }
        }
        else
        {
            _endGameButton.gameObject.SetActive(false);
        }
    }

    protected override void SetEndGameUI(bool isGameOver)
    {
        // Give a feedback to the user about its performance
        if (_distanceToWall <= 0.50f)
        {
            _successText += "\nPerfect parking. Your score was ";
        }
        // Slight infraction
        else if (_distanceToWall < 1.0f)
        {
            _successText = "Thank you for playing. You got a slight infraction.\nTry to improve you result. Your score was ";
        }
        base.SetEndGameUI(isGameOver);
    }

    private void UpdateDistanceToWall()
    {
        // Checks the degree of the infraction according to the distance to the wall in order to set the text color
        // 0.11f ---> due to the fact that the player car object is bigger than the actual car image
        _distanceToWall = _wall.transform.position.y - (_playerCar.transform.position.y + _playerDimensions.y/2.0f) - 0.11f;
        if (_distanceToWall <= 0.50f)
        {
            _distanceToWallText.color = Color.green;
        }
        // Slight infraction
        else if (_distanceToWall < 1.0f)
        {
            _distanceToWallText.color = Color.yellow;
        }
        // Serious infraction
        else
        {
            _distanceToWallText.color = Color.red;
        }
        _distanceToWallText.text = "Distance to wall: " + _distanceToWall.ToString("0.00");
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
        
        if (IsPlayerCarBetweenParkingCars())
        {
            if (IsPlayerCarHorizontallyAligned())
            {
                _warningText.text = "";
            }
            else
            {
                _warningText.text = "Align the car\nhorizontally";
            }
        }
        else
        {
            _warningText.text = "Place the car between\nthe other two";
        }
    }
    /// <summary>
    /// Checks if the player's car is correctly aligned, i.e. if the rotation around the z axis is inferior to plus or 
    /// minus 5 degrees.
    /// </summary>
    /// <returns></returns>
    private bool IsPlayerCarHorizontallyAligned()
    {
        return (_playerCar.gameObject.transform.eulerAngles.z < 5.0f) && (_playerCar.gameObject.transform.eulerAngles.z > -5.0f);
    }
    /// <summary>
    /// Checks if the player's car is between the two parking cars (wrt to the x coordinate).
    /// </summary>
    /// <returns></returns>
    private bool IsPlayerCarBetweenParkingCars()
    {
        return (_playerCar.transform.position.x + _playerDimensions.x / 2.0f) < (_parkingCar1.transform.position.x - _parkingCarDimensions.x / 2.0f)
            && (_playerCar.transform.position.x - _playerDimensions.x / 2.0f) > (_parkingCar2.transform.position.x + _parkingCarDimensions.x / 2.0f);
    }

    public void OnClickEndGameButton()
    {
        if (!_isEndGame)
        {
            SetEndGameUI(false);
        }
    }

    protected override long GetScore()
    {
        if (_distanceToWall > 0.50f && _distanceToWall < 1.0f)
        {
            // Infraction, so we double the score as penalty(greater the score, worse the performance) 
            return 2*base.GetScore();
        }
        
        return base.GetScore();
    }

}
