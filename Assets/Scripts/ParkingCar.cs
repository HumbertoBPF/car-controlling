using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingCar : MonoBehaviour
{
    private PlayerCar _playerCar;
    // Start is called before the first frame update
    void Start()
    {
        _playerCar = GameObject.Find("Player_Car").GetComponent<PlayerCar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            _playerCar.IsDamaged = true;
        }
    }
}
