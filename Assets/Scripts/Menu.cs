using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnClickParkingGameButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickObstacleGameButton()
    {
        SceneManager.LoadScene(2);
    }
}
