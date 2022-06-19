using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Identifiers of this game
    protected int _gameId;
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
    [SerializeField]
    protected GameObject _panel;
    protected ScoreSubmissionForm _scoreSubmissionForm;
    // Reference objects
    protected PlayerCar _playerCar;
    // Dimensions
    protected Vector3 _playerDimensions;
    // Flags to control life cycle of a game
    protected bool _isEndGame = false;
    protected bool _isGameOver = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _scoreSubmissionForm = _panel.GetComponent<ScoreSubmissionForm>();
        _playerCar = GameObject.Find("Player_Car").GetComponent<PlayerCar>();
        _playerDimensions = _playerCar.GetComponent<Renderer>().bounds.size;
        StartCoroutine(ChronometerCoroutine());
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!_scoreSubmissionForm.IsFormOpened)
        {
            SetKeyboardShortcuts();
        }

        if (!_playerCar.IsEnabled && !_isEndGame)
        {
            SetEndGameUI(true);
        }
    }

    void SetKeyboardShortcuts()
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

        // When the game has ended(i.e. player cannot control the car anymore) and the user wins, the "S" key allows to save the score
        if (!_playerCar.IsEnabled && !_isGameOver && Input.GetKeyDown(KeyCode.S))
        {
            _scoreSubmissionForm.GameId = _gameId;
            _scoreSubmissionForm.Score = GetScore();
            _scoreSubmissionForm.SetVisibilityScoreForm(true);
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
        
        if (!_isGameOver)
        {
            _endGameText.text += "\nYour score was " + GetScore();
        }
    }

    protected virtual void SetEndGameUI(bool isGameOver)
    {
        _playerCar.IsEnabled = false;
        _isEndGame = true;
        _isGameOver = isGameOver;
        string shortcutsText = "Press 'R' to play again\n'M' to return to the main menu";
        // Default text corresponds to a game over scenario. If the player wins, change set text appearance and add a short cut allowing user to save the score
        if (!isGameOver)
        {
            _endGameText.fontSize = 25;
            _endGameText.text = _successText;
            shortcutsText += "\n'S' to save your score";
        }
        _endGameText.gameObject.SetActive(true);
        _shortcutsText.text = shortcutsText;
    }

    protected virtual long GetScore()
    {
        return _chronometerTime*10;
    }

}
