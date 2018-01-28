using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
    public RectTransform pauseMenu;
    public RectTransform confirmQuit;
    public RectTransform confirmRestart;

    private void Awake()
    {
        if (pauseMenu)
            pauseMenu.gameObject.SetActive(false);

        if (confirmQuit)
            confirmQuit.gameObject.SetActive(false);

        if (confirmRestart)
            confirmRestart.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
    }

    public void Pause()
    {

        if (Time.timeScale == 0f)
        {
            if (confirmQuit.gameObject.activeInHierarchy)
                ToggleConfirmQuit();
            else
            {
                Time.timeScale = 1f;
                pauseMenu.gameObject.SetActive(false);
            }
            
        }
        else
        {
            Time.timeScale = 0f;
            pauseMenu.gameObject.SetActive(true);
        }  
    }

    public void ToggleConfirmQuit()
    {
        confirmQuit.gameObject.SetActive(!confirmQuit.gameObject.activeInHierarchy);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ToggleConfirmRestart()
    {
        confirmRestart.gameObject.SetActive(!confirmRestart.gameObject.activeInHierarchy);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
