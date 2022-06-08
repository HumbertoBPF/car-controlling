using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private long _chronometerTime;
    // Text objects (UI)
    [SerializeField]
    private Text _chronometerText;
    [SerializeField]
    private Text _playAgainText;
    [SerializeField]
    private Text _gameOverText;
    // Reference objects
    private PlayerCar _playerCar;
    // Start is called before the first frame update
    void Start()
    {
        _playerCar = GameObject.Find("Player_Car").GetComponent<PlayerCar>();
        StartCoroutine(ChronometerCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerCar.IsDamaged)
        {
            SetGameOverUI();
        }
    }

    IEnumerator ChronometerCoroutine()
    {
        while (!_playerCar.IsDamaged)
        {
            yield return new WaitForSeconds(1.0f);
            _chronometerTime++;
            _chronometerText.text = _chronometerTime + " s";
        }
    }

    private void SetGameOverUI()
    {
        // Show game over text
        _gameOverText.gameObject.SetActive(true);
        _playAgainText.gameObject.SetActive(true);
        // When the game is over, the "R" key restarts it
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

}
