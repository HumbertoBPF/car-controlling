using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerDrivingGame : UIManager
{
    private float _distance;
    [SerializeField]
    private Text _speedText;
    [SerializeField]
    private Text _distanceText;
    // Start is called before the first frame update
    protected override void Start()
    {
        _indexScene = 3;
        _gameId = 3;
        base.Start();
        StartCoroutine(UpdateDistance());
    }

    protected override void Update()
    {
        base.Update();

        if ((_distance > 1000.0f) && !_isEndGame)
        {
            SetEndGameUI(false);
        }
        else
        {
            _speedText.text = (10 * _playerCar.SpeedTranslation).ToString("N2") + "Km/h";
        }
    }
    /// <summary>
    /// Coroutine to update the distance driven so far. The distance update is not implemented direcly on the update method because 
    /// known time interval between two distances updates is necessary in order to properly compute the increment in the distance.
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdateDistance()
    {
        while (!_isEndGame)
        {
            yield return new WaitForSeconds(0.5f);
            _distance += 10 * _playerCar.SpeedTranslation * 0.5f / 3.6f;
            _distanceText.text = _distance.ToString("N2") + "m";
        }
    }
}
