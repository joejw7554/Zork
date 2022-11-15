using System;
using TMPro;
using UnityEngine;
using Zork.Common;
public class UnityInputService : MonoBehaviour, IInputService
{
    public event EventHandler<string> InputReceived;
    [SerializeField] TMP_InputField InputField;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string inputString = InputField.text;
            if (string.IsNullOrEmpty(inputString) == false)
            {
                InputReceived?.Invoke(this, inputString);
            }

            InputField.text = string.Empty;
        }
    }
}
