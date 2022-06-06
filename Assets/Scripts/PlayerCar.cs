using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCar : MonoBehaviour
{
    [SerializeField]
    private bool _isDamaged = false;
    // Car movement variables
    [SerializeField]
    private float _speedTranslation = 3.5f;
    [SerializeField]
    private float _speedRotation = 10.0f;
    // UI variables
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _playAgainText;
    public bool IsDamaged { get { return _isDamaged; } set { _isDamaged = value; } }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDamaged)
        {
            ControlCarByKeys();
        }
        else
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

    void ControlCarByKeys()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float clockwiseRotation = Input.GetAxis("Vertical");
        transform.Translate(horizontalInput * Vector3.right * _speedTranslation * Time.deltaTime);
        transform.Rotate(horizontalInput * clockwiseRotation * Vector3.forward * _speedRotation * Time.deltaTime);
    }
}
