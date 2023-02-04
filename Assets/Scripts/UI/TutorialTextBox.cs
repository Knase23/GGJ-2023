using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Text))]
    public class TutorialTextBox : MonoBehaviour
    {
        private Text _textField;
        public GameObject toActivateOrDeactivate;

        public void Start()
        {
            _textField = GetComponent<Text>();
        }

        public void ShowText(string text)
        {
            if (!_textField) _textField = GetComponent<Text>();
            _textField.text = text;

            //TODO: Cool transition Starter here

            if (toActivateOrDeactivate)
                toActivateOrDeactivate.SetActive(true);
            else
                gameObject.SetActive(true);
        }

        public void HideText()
        {
            if (toActivateOrDeactivate)
                toActivateOrDeactivate.SetActive(false);
            else
                gameObject.SetActive(false);
        }
    }
}