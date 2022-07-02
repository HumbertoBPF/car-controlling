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

    public void OnClickDrivingGameButton()
    {
        SceneManager.LoadScene(3);
    }

    public void OnClickInstructionsButton()
    {
        SceneManager.LoadScene(4);
    }
}
