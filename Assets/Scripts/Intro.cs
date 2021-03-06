using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public List<string> dialogues;
    public int currentIndex;
    public Animator diegoAnimator;
    public Animator audreyAnimator;
    public Animator dialogueBoxAnimator;
    public Animator sceneTransitionAnimator;
    private FMOD.Studio.EventInstance _music;
    void Awake()
    {
        currentIndex = 0;
        StartCoroutine(RunDialogue());
        dialogueText.text = "";
        _music= FMODUnity.RuntimeManager.CreateInstance("event:/Music/MenuMusic");
        _music.start();
    }

    private void OnDestroy()
    {
        _music.stop(STOP_MODE.ALLOWFADEOUT);
    }

    IEnumerator RunDialogue()
    {
        yield return new WaitForSeconds(1f);
        diegoAnimator.Play("In");
        yield return new WaitForSeconds(1f);
        audreyAnimator.Play("In");
        yield return new WaitForSeconds(1f);
        dialogueBoxAnimator.Play("In");
        yield return new WaitForSeconds(2f);
        while (currentIndex < dialogues.Count)
        {
            dialogueText.text = dialogues[currentIndex];
            currentIndex++;
            yield return new WaitForSeconds(5f);
            if (currentIndex == 4)
            {
                diegoAnimator.Play("Dress");
            }
        }
        sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Game");
    }
}
