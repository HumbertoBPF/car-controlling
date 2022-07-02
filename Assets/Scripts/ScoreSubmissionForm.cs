using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ScoreSubmissionForm : MonoBehaviour
{
    private int _gameId;
    public int GameId { set { _gameId = value; } }
    private long _score;
    public long Score { set { _score = value; } }
    // Form submit score (UI)
    [SerializeField]
    private InputField _usernameInputField;
    [SerializeField]
    private InputField _passwordInputField;
    [SerializeField]
    private GameObject _panel;
    [SerializeField]
    private Text _responseText;
    // Flags to control life cycle of the form
    private bool _isFormOpened = false;
    public bool IsFormOpened { get { return _isFormOpened; }}
    private bool _isFormSubmitted = false;
    public bool IsFormSubmitted { get { return _isFormSubmitted; }}
    private bool _isProcessingUpload = false;
    public bool IsProcessingUpload { get { return _isProcessingUpload; }}

    public void OnClickSubmitButton()
    {
        // _isFormSubmitted --> avoids the score to be submitted twice
        // _isProcessingUpload --> avoid the score to be submitted twice by clicking twice on the button before te end of processing the upload request
        if (!_isFormSubmitted && !_isProcessingUpload)
        {
            _isProcessingUpload = true;
            StartCoroutine(PostScore());
        }
        else if (_isFormSubmitted)
        {
            _responseText.text = "Score has already been submitted.";
        }
    }

    public void OnClickCancelButton()
    {
        SetVisibilityScoreForm(false);
    }
    /// <summary>
    /// Sends a POST request in order to 
    /// </summary>
    /// <returns></returns>
    IEnumerator PostScore()
    {
        string url = "http://localhost:8000/api/scores";
        string json = "{\"game\": " + _gameId + ",\"score\": " + _score + "}";

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
            Debug.Log("Sent: " + request.downloadHandler.text);
            if (request.responseCode == 201)
            {
                _responseText.text = "Score successfully uploaded";
                _isFormSubmitted = true;
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
    /// <summary>
    /// Formats the BasicAuthentication string given a username and a password.
    /// </summary>
    /// <param name="username">username input</param>
    /// <param name="password">password input</param>
    /// <returns></returns>
    string GetAuthString(string username, string password)
    {
        Debug.Log("username = " + username);
        Debug.Log("password = " + password);

        string auth = username + ":" + password;
        auth = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(auth));
        auth = "Basic " + auth;
        return auth;
    }

    public void SetVisibilityScoreForm(bool isVisible)
    {
        _responseText.text = "";
        _panel.SetActive(isVisible);
        _isFormOpened = isVisible;
    }

}
