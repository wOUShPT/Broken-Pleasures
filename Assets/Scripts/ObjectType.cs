using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectType : MonoBehaviour
{
    public string type;
    private FMOD.Studio.EventInstance _hit;

    private void Awake()
    {
        _hit = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Drop");
        _hit.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        _hit.setVolume(0.5f);
    }


    private void OnCollisionEnter(Collision other)
    {
        _hit.start();
    }
}
