using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    protected int _indexScene;
    protected string _successText = "Congratulations :)";
    protected long _chronometerTime;
    protected bool _isEndGame = false;
    // Text objects (UI)
    [SerializeField]
    protected Text _chronometerText;
    [SerializeField]
    protected Text _playAgainText;
    [SerializeField]
    protected Text _endGameText;
    [SerializeField]
    protected Text _menuShortcutText;
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
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(0);
        }

        // When the game has ended, the "R" key restarts it
        if (_isEndGame && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(_indexScene);
        }

        if (_playerCar.IsDamaged)
        {
            SetEndGameUI(true);
        }
    }

    protected IEnumerator ChronometerCoroutine()
    {
        while (!_isEndGame)
        {
            yield return new WaitForSeconds(1.0f);
            _chronometerTime++;
            _chronometerText.text = _chronometerTime + " s";
        }
    }

    protected virtual void SetEndGameUI(bool isGameOver)
    {
        _isEndGame = true;
        // Show game over text
        if (!isGameOver)
        {
            _endGameText.fontSize = 25;
            _endGameText.text = _successText;
        }
        _menuShortcutText.gameObject.SetActive(false);
        _endGameText.gameObject.SetActive(true);
        _playAgainText.gameObject.SetActive(true);
    }
}
