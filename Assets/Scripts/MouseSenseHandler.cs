using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MouseSenseHandler : MonoBehaviour
{
    public MouseSense mouseSenseData;
    public CinemachineVirtualCamera vCam;
    private CinemachinePOV _povComponent;

    private void Awake()
    {
        _povComponent = vCam.GetCinemachineComponent<CinemachinePOV>();
        _povComponent.m_HorizontalAxis.m_MaxSpeed = mouseSenseData.sensitivity * 5f;
        _povComponent.m_VerticalAxis.m_MaxSpeed = mouseSenseData.sensitivity * 0.56f * 5f;
    }
}
