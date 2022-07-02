using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerDrivingGame : MonoBehaviour
{
    [SerializeField]
    private GameObject _cpuCarPrefab;
    private PlayerCarDrivingGame _playerCarDrivingGame;
    // Start is called before the first frame update
    void Start()
    {
        _playerCarDrivingGame = GameObject.Find("Player_Car").GetComponent<PlayerCarDrivingGame>();
        StartCoroutine(SpawnCpuCar());   
    }

    IEnumerator SpawnCpuCar()
    {
        // Keep spawning CPU cars while player can move
        while (_playerCarDrivingGame.IsEnabled)
        {
            // Randomize the position where the car is spawned
            float randomY = UnityEngine.Random.Range(-1.9f, 3.8f);
            GameObject instance = Instantiate(_cpuCarPrefab, new Vector3(12.20f, randomY, 0.0f), Quaternion.identity);
            instance.tag = "Parking Car 1";
            // Also randomize the time of the next spawn
            float randomWaitingTime = UnityEngine.Random.Range(1.5f, 3.1f);
            yield return new WaitForSeconds(randomWaitingTime);
        }
    }
}
