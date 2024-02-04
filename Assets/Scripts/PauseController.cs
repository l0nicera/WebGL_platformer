using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public UnityEvent onGamePaused;
    public UnityEvent onGameResumed;

    public GameObject pauseMenuUI;
    public CursorController cursorController;

    private bool isPaused = false;

    void Start()
    {
            Time.timeScale = 1;
            pauseMenuUI.SetActive(false);
            cursorController.SetPauseState(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0;
            cursorController.SetPauseState(true);
            onGamePaused.Invoke();
        }
        else
        {
            Time.timeScale = 1;
            cursorController.SetPauseState(false);
            onGameResumed.Invoke();
        }
    }

    public void ResumeGame()
    {
        TogglePause();
    }

}
