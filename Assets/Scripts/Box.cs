using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class Box : MonoBehaviour
{
    private GameManager _gameManager;
    public string type;
    [FormerlySerializedAs("_animator")] public Animator animator;
    private FMOD.Studio.EventInstance _correct;
    private FMOD.Studio.EventInstance _wrong;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _correct = FMODUnity.RuntimeManager.CreateInstance("event:/Stingers/Correct");
        _wrong = FMODUnity.RuntimeManager.CreateInstance("event:/Stingers/Wrong");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))
        {
            if (other.GetComponent<ObjectType>().type == type)
            {
                _gameManager.Score(4);
                _correct.start();
                animator.Rebind();
                animator.Play("Correct");
            }
            else
            {
                _gameManager.Score(-4);
                animator.Rebind();
                animator.Play("Wrong");
                _wrong.start();
            }

        }
    }
}
