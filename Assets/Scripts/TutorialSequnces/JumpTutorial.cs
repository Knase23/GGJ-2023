using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class JumpTutorial : Task
{

    private int numberOfJumpts;
    public int numberOfJumpsToComplete;
    public TutorialTextBox tutorialTextObject;
    [TextArea]
    public string textToShow;
    private Vector2 _currentInput;

    private bool active;
    private Coroutine _runningCoroutine;
    public override void StartTask()
    {
        _runningCoroutine = StartCoroutine(TaskWaiter());
        
        Player.Instance.Jump.jumpAction.ToInputAction().performed += OnPerformed;
    }

    protected override void OnTaskComplete()
    {
        Player.Instance.Jump.jumpAction.ToInputAction().performed -= OnPerformed;
        base.OnTaskComplete();
    }

    private IEnumerator TaskWaiter()
    {
        tutorialTextObject.ShowText(textToShow);
        while (numberOfJumpts < numberOfJumpsToComplete)
        {
            yield return new WaitForEndOfFrame();
        }
        tutorialTextObject.HideText();
        _runningCoroutine = null;
        OnTaskComplete();
    }

    private void OnPerformed(InputAction.CallbackContext obj)
    {
        numberOfJumpts++;
        
    }
}
