using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

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
    // Form submit score (UI)
    [SerializeField]
    protected InputField _usernameInputField;
    [SerializeField]
    protected InputField _passwordInputField;
    [SerializeField]
    protected GameObject _panel;
    [SerializeField]
    protected Text _responseText;
    // Reference objects
    protected PlayerCar _playerCar;
    // Dimensions
    protected Vector3 _playerDimensions;
    // Flags to control life cycle of a game
    protected bool _isEndGame = false;
    protected bool _isGameOver = false;
    protected bool _isFormOpened = false;
    protected bool _isFormSubmitted = false;
    protected bool _isProcessingUpload = false;

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
        if (!_isFormOpened)
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
            // The authentication form is shown only if the users have not submitted their scores successfully yet
            if (!_isFormSubmitted)
            {
                SetVisibilityScoreForm(true);
            }
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

    public void OnClickSubmitButton()
    {
        // _isFormSubmitted --> avoids the score to be submitted twice
        // _isProcessingUpload --> avoid the score to be submitted twice by clicking twice on the button before te end of processing the upload request
        if (!_isFormSubmitted && !_isProcessingUpload)
        {
            _isProcessingUpload = true;
            StartCoroutine(PostScore());
        }else if (_isFormSubmitted)
        {
            _responseText.text = "Score has already been submitted.";
        }
    }

    public void OnClickCancelButton()
    {
        SetVisibilityScoreForm(false);
    }

    IEnumerator PostScore()
    {
        string url = "http://localhost:8000/api/scores";
        string json = "{\"game\": "+_gameId+ ",\"score\": "+GetScore()+"}";

        Debug.Log(json);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");

        request.SetRequestHeader("AUTHORIZATION", GetAuthString(_usernameInputField.text, _passwordInputField.text));

        request.disposeUploadHandlerOnDispose = true;
        request.disposeDownloadHandlerOnDispose = true;

        //Send the request then wait here until it returns
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + request.error);
            _responseText.text = "An error occurred during the upload of your score, please verify your internet connexion.";
        }
        else
        {
            Debug.Log("Sent: "+ request.downloadHandler.text);
            if (request.responseCode == 201)
            {
                _responseText.text = "Score successfully uploaded";
                _isFormSubmitted = true;
                _shortcutsText.text = "Press 'R' to play again\n'M' to return to the main menu";
            }
            else if (request.responseCode == 401)
            {
                _responseText.text = "Credentials error. Verify if your username and password are correct.";
            }
        }

        _isProcessingUpload = false;
        //Signals that this UnityWebRequest is no longer being used, and should clean up any resources it is using
        request.Dispose();
    }

    void SetVisibilityScoreForm(bool isVisible)
    {
        _responseText.text = ""; 
        _panel.SetActive(isVisible);
        _isFormOpened = isVisible;
    }

    string GetAuthString(string username, string password)
    {
        Debug.Log("username = " + username);
        Debug.Log("password = " + password);

        string auth = username + ":" + password;
        auth = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(auth));
        auth = "Basic " + auth;
        return auth;
    }

    protected virtual long GetScore()
    {
        return _chronometerTime*10;
    }

}
