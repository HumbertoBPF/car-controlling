using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerParkingGame : MonoBehaviour
{
    [SerializeField]
    private float _xBound = 10.0f;
    [SerializeField]
    private float _yBound = 5.80f;
    [SerializeField]
    private float _tolerance = 2.0f;
    private PlayerCar _playerCar;
    [SerializeField]
    private GameObject _parkingCarPrefab;
    // Start is called before the first frame update
    void Start()
    {
        _playerCar = GameObject.Find("Player_Car").GetComponent<PlayerCar>();
        Vector3 dimensionsPlayer = _playerCar.GetComponent<Renderer>().bounds.size;
        Vector3 dimensionsParkingCar = _parkingCarPrefab.GetComponent<Renderer>().bounds.size;
        // deltaX = space in the x coordinate that the player's car will have between the two parking cars
        float deltaX = dimensionsParkingCar.x + (dimensionsPlayer.x + _tolerance);
        float xmax = _xBound;
        float xmin = deltaX - _xBound;

        float xRandom = UnityEngine.Random.Range(xmin, xmax);
        Vector3 positionParkingCar1 = new Vector3(xRandom, _yBound, 0);
        Vector3 positionParkingCar2 = new Vector3(xRandom - deltaX, _yBound, 0);
        GameObject parkingCar1 = Instantiate(_parkingCarPrefab, positionParkingCar1, Quaternion.identity);
        parkingCar1.tag = "Parking Car 1";
        GameObject parkingCar2 = Instantiate(_parkingCarPrefab, positionParkingCar2, Quaternion.identity);
        parkingCar2.tag = "Parking Car 2";
    }
}
