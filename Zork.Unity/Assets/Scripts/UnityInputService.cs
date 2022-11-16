using System;
using TMPro;
using UnityEngine;
using Zork.Common;
public class UnityInputService : MonoBehaviour, IInputService
{
    [SerializeField] TMP_InputField InputField;

    public event EventHandler<string> InputReceived;

    public void ProcessInput()
    {

        if (string.IsNullOrEmpty(InputField.text) == false)
        {
            InputReceived?.Invoke(this, InputField.text.Trim());
        }
        InputField.text = string.Empty;
        SetFocus();
    }
    public void SetFocus()
    {
        InputField.Select();
        InputField.ActivateInputField();
    }
}
