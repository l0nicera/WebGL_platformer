using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{

    void Start()
    {
        SetPauseState(false);
    }

    private bool isGamePaused = false;

    public void SetPauseState(bool isPaused)
    {
        isGamePaused = isPaused;
        UpdateCursorState();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!isGamePaused)
        {
            Cursor.lockState = focus ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

    private void UpdateCursorState()
    {
        if (isGamePaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
