using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
   private CinemachineFreeLook cinemachineCamera;

    void Start()
    {
       cinemachineCamera = GetComponent<CinemachineFreeLook>();
    }

    void Update()
    {
        if (Input.GetAxis("CameraRecenter") == 1)
        {
            cinemachineCamera.m_RecenterToTargetHeading.m_enabled = true;
        }
        else
        {
            cinemachineCamera.m_RecenterToTargetHeading.m_enabled = false;
        }
    }
}
