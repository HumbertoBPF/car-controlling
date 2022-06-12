using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerObstacleGame : MonoBehaviour
{
    protected int _indexScene = 2;
    protected long _chronometerTime;
    // Text objects (UI)
    [SerializeField]
    protected Text _chronometerText;
    [SerializeField]
    protected Text _playAgainText;
    [SerializeField]
    protected Text _gameOverText;
    // Reference objects
    protected PlayerCar _playerCar;
    // Dimensions
    protected Vector3 _playerDimensions;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _playerCar = GameObject.Find("Player_Car").GetComponent<PlayerCar>();
        _playerDimensions = _playerCar.GetComponent<Renderer>().bounds.size;
        StartCoroutine(ChronometerCoroutine());
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_playerCar.IsDamaged)
        {
            SetGameOverUI();
        }
    }

    protected IEnumerator ChronometerCoroutine()
    {
        while (!_playerCar.IsDamaged)
        {
            yield return new WaitForSeconds(1.0f);
            _chronometerTime++;
            _chronometerText.text = _chronometerTime + " s";
        }
    }

    protected void SetGameOverUI()
    {
        // Show game over text
        _gameOverText.gameObject.SetActive(true);
        _playAgainText.gameObject.SetActive(true);
        // When the game is over, the "R" key restarts it
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(_indexScene);
        }
    }
}
