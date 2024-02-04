using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelController : MonoBehaviour
{
   public GameObject controlPanel;

    private bool isMoving = false;

    void Start()
    {
        controlPanel.SetActive(true);
    }

    void Update()
    {
        if (!isMoving && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            isMoving = true;
            controlPanel.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            controlPanel.SetActive(!controlPanel.activeSelf);
        }
    }
}
