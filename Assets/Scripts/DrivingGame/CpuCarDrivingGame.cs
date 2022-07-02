using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CpuCarDrivingGame : MonoBehaviour
{
    // Car movement variables
    private PlayerCarDrivingGame _playerCarDrivingGame;

    // Start is called before the first frame update
    void Start()
    {
        _playerCarDrivingGame = GameObject.Find("Player_Car").GetComponent<PlayerCarDrivingGame>();
    }

    // Update is called once per frame
    void Update()
    {   
        // CPU car moves while player can move
        if (_playerCarDrivingGame.IsEnabled)
        {
            float currentX = transform.position.x;

            if (currentX < -12.20f)
            {
                Destroy(this.gameObject);
            }

            transform.Translate(Vector3.left * _playerCarDrivingGame.SpeedTranslation * Time.deltaTime);
        }
    }
}
