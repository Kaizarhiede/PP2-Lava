using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_buttonFunctions : MonoBehaviour
{
    
    public void resume()
    {

        CS_gameManager.instance.unpauseState();
        CS_gameManager.instance.isPaused = !CS_gameManager.instance.isPaused;

    }

    public void restart()
    {

        CS_gameManager.instance.unpauseState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void quit()
    {

        Application.Quit();

    }

}
