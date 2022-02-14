using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class ButtonSound : MonoBehaviour
{
    private FMOD.Studio.EventInstance _hover;
    private FMOD.Studio.EventInstance _click;
    void Start()
    {
        _hover = FMODUnity.RuntimeManager.CreateInstance("event:/UI/PencilHover");
        _click = FMODUnity.RuntimeManager.CreateInstance("event:/UI/PencilClick");
    }

    public void PlayHover()
    {
        _hover.start();
    }

    public void PlayPress()
    {
        _click.start();
    }
}
