using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class RootTutorial : Task
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
        
        
    }

    protected override void OnTaskComplete()
    {
        base.OnTaskComplete();
    }

    private IEnumerator TaskWaiter()
    {
        tutorialTextObject.ShowText(textToShow);
        while (numberOfJumpts < numberOfJumpsToComplete)
        {
            if (Player.Instance.Rooting.IsRooted && !active)
            {
                numberOfJumpts++;
            }
            active = Player.Instance.Rooting.IsRooted; 
            yield return new WaitForEndOfFrame();
        }
        tutorialTextObject.HideText();
        _runningCoroutine = null;
        OnTaskComplete();
    }
    
}
