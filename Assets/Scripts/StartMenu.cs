using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
      

    public void StartGame()
    {
      
        SceneManager.LoadScene("Finales Spiel");
    }
  
    public void Exit()
    {
        Application.Quit();
    }

    public void Controls()
    {
        SceneManager.LoadScene("ControlsUI");
    }
}
