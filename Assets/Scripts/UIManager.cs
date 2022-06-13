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
    // Text objects (UI)
    [SerializeField]
    protected Text _chronometerText;
    [SerializeField]
    protected Text _shortcutsText;
    [SerializeField]
    protected Text _endGameText;
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
        // Shortcut to main menu (always active)
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(0);
        }

        // When the game has ended(i.e. player cannot control the car anymore), the "R" key restarts it
        if (!_playerCar.IsEnabled && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(_indexScene);
        }

        if (!_playerCar.IsEnabled)
        {
            SetEndGameUI(true);
        }
    }

    protected IEnumerator ChronometerCoroutine()
    {
        while (_playerCar.IsEnabled)
        {
            yield return new WaitForSeconds(1.0f);
            _chronometerTime++;
            _chronometerText.text = _chronometerTime + " s";
        }
    }

    protected virtual void SetEndGameUI(bool isGameOver)
    {
        _playerCar.IsEnabled = false;
        // Default text corresponds to a game over scenario. If the player wins, change set text
        if (!isGameOver)
        {
            _endGameText.fontSize = 25;
            _endGameText.text = _successText;
        }
        _endGameText.gameObject.SetActive(true);
        _shortcutsText.text = "Press 'R' to play again\nor 'M' to return to the main menu";
    }
}
