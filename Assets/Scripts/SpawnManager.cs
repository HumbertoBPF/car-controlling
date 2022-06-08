using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private float _xBound = 10.0f;
    [SerializeField]
    private float _yBound = 5.45f;
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
        float deltaX = dimensionsParkingCar.x + (dimensionsPlayer.x + _tolerance);
        float xmax = _xBound;
        float xmin = deltaX - _xBound;
        Debug.Log("dimensionsPlayer = " + dimensionsPlayer);
        Debug.Log("dimensionsParkingCar = " + dimensionsParkingCar);
        Debug.Log("xmax = " + xmax);
        Debug.Log("xmin = " + xmin);

        float xRandom = UnityEngine.Random.Range(xmin, xmax);
        Vector3 positionParkingCar1 = new Vector3(xRandom, _yBound, 0);
        Vector3 positionParkingCar2 = new Vector3(xRandom - deltaX, _yBound, 0);
        Instantiate(_parkingCarPrefab, positionParkingCar1, Quaternion.identity);
        Instantiate(_parkingCarPrefab, positionParkingCar2, Quaternion.identity);
    }
}
