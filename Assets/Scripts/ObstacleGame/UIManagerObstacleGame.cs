using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerObstacleGame : UIManager
{
    // Start is called before the first frame update
    protected override void Start()
    {
        _indexScene = 2;
        _gameId = 1;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        // If the player car leaves the parking, the player wins
        if ((_playerCar.gameObject.transform.position.x - _playerDimensions.x > 10.0f) && !_isEndGame)
        {
            SetEndGameUI(false);
        }
    }
}
