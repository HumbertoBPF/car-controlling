using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerObstacleGame : MonoBehaviour
{
    [SerializeField]
    private float _xUpperBound = 8.0f;
    [SerializeField]
    private float _xLowerBound = -8.0f;
    [SerializeField]
    private float _yUpperBound = 6.0f;
    [SerializeField]
    private float _yLowerBound = -4.0f;
    private PlayerCar _playerCar;
    private Vector3 _dimensionsPlayer;
    [SerializeField]
    private GameObject _parkingCarPrefab;
    private Vector3 _dimensionsParkingCarPrefab;
    [SerializeField]
    private int _numbersOfSpawns = 3;
    // Dictionary to store the positions of the _grid that contain a car
    private Dictionary<string, GameObject> _grid = new Dictionary<string, GameObject>();
    private int _xGrid;
    private int _yGrid;
    // Start is called before the first frame update
    void Start()
    {
        _playerCar = GameObject.Find("Player_Car").GetComponent<PlayerCar>();
        _dimensionsPlayer = _playerCar.GetComponent<Renderer>().bounds.size;
        _dimensionsParkingCarPrefab = _parkingCarPrefab.GetComponent<Renderer>().bounds.size;
        // Dimensions of the _grid
        _xGrid = (int) ((_xUpperBound - _xLowerBound) / (_dimensionsPlayer.x + (_dimensionsPlayer.y + 1)));
        _yGrid = (int) ((_yUpperBound - _yLowerBound) / (_dimensionsPlayer.y + (_dimensionsPlayer.y + 1)));
        Debug.Log("xGrid = " + _xGrid);
        Debug.Log("yGrid = " + _yGrid);
        _grid.Add("0|0", _playerCar.gameObject);

        while (_grid.Count < _numbersOfSpawns + 1)
        {
            int i = UnityEngine.Random.Range(0, _xGrid + 1);
            int j = UnityEngine.Random.Range(0, _yGrid + 1);

            if (!_grid.ContainsKey(i + "|" + j))
            {
                float x = _xLowerBound + (_dimensionsPlayer.x + (_dimensionsPlayer.y + 1)) * i;
                float y = _yLowerBound + (_dimensionsPlayer.y + (_dimensionsPlayer.y + 1)) * j;
                _grid[i + "|" + j] = AddParkingCarToScene(x, y);

                int orientation = UnityEngine.Random.Range(0, 2);
                if (orientation == 0 && _grid.Count < _numbersOfSpawns + 1)
                {
                    InstantiateNeighbor(i, j);
                }
            }
        }
    }

    GameObject AddParkingCarToScene(float x, float y)
    {
        Vector3 position = new Vector3(x, y, 0);

        GameObject instance = Instantiate(_parkingCarPrefab, position, Quaternion.identity);
        // Randomizing orientation
        int orientation = UnityEngine.Random.Range(0, 2);
        instance.transform.Rotate(orientation * 180 * Vector3.forward);

        instance.tag = "Parking Car 1";

        return instance;
    }

    void InstantiateNeighbor(int i, int j)
    {
        ArrayList shiftList = new ArrayList();

        if ((i + 1 <= _xGrid) && !_grid.ContainsKey((i + 1) + "|" + j))
        {
            Vector3 shift = new Vector3(1, 0, 0);
            shiftList.Add(shift);
        }

        if ((i - 1 >= 0) && !_grid.ContainsKey((i - 1) + "|" + j))
        {
            Vector3 shift = new Vector3(-1, 0, 0);
            shiftList.Add(shift);
        }

        if ((j + 1 <= _yGrid) && !_grid.ContainsKey(i + "|" + (j + 1)))
        {
            Vector3 shift = new Vector3(0, 1, 0);
            shiftList.Add(shift);
        }

        if ((j - 1 >= 0) && !_grid.ContainsKey(i + "|" + (j - 1)))
        {
            Vector3 shift = new Vector3(0, -1, 0);
            shiftList.Add(shift);
        }

        if (shiftList.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, shiftList.Count);
            Vector3 shift = (Vector3) shiftList[randomIndex];
            Vector3 shiftPosition = Vector3.Scale(shift, _dimensionsParkingCarPrefab);
            int shiftI = (int) shift.x;
            int shiftJ = (int) shift.y;

            Debug.Log((i + shiftI) + "|" + (j + shiftJ));

            GameObject parkingCar = _grid[i + "|" + j];

            float x = parkingCar.transform.position.x + shiftPosition.x;
            float y = parkingCar.transform.position.y + shiftPosition.y;
            _grid[(i+shiftI) + "|" + (j+shiftJ)] = AddParkingCarToScene(x, y);
        }
    }

}
