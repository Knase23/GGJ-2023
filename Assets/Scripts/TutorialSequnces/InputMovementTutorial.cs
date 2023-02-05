using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace TutorialSequnces
{
    public class InputMovementTutorial : Task
    {
        public bool left, right;

        public TutorialTextBox tutorialTextObject;
        [TextArea]
        public string textToShow;
        private Vector2 _currentInput;

        private Coroutine _runningCoroutine;
        public override void StartTask()
        {
            _runningCoroutine = StartCoroutine(CheckInput());
        }

        private IEnumerator CheckInput()
        {
            tutorialTextObject.ShowText(textToShow);
            while (!left || !right)
            {
                _currentInput = Player.Instance.Movement.movementInput.ToInputAction().ReadValue<Vector2>();

                //TODO: Here we could do other things to show, witch input has been completed
                if (!left && _currentInput.x > 0)
                    left = true;
                
                if (!right && _currentInput.x < 0)
                    right = true;

                yield return new WaitForEndOfFrame();
            }
            tutorialTextObject.HideText();
            _runningCoroutine = null;
            OnTaskComplete();
        }
    }
}