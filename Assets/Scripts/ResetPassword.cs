using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResetPassword : MonoBehaviour
{
    [SerializeField]private Button _sendMailButton;
    [SerializeField] private TMP_InputField _mailInputField;
    [SerializeField] private GameObject _sucessMessage;
    [SerializeField] private GameObject _parentPanel;
    
    private void Reset()
    {
        _sendMailButton = GetComponent<Button>();
        _mailInputField = GameObject.Find("InputFieldSendEmail").GetComponent<TMP_InputField>();
    }

    private void Start()
    {
        _sendMailButton.onClick.AddListener(HandleResetPasswordButtonClicked);
    }

    private void HandleResetPasswordButtonClicked()
    {
        string email = _mailInputField.text;
        var auth = FirebaseAuth.DefaultInstance;

        if (email != null)
        {
            auth.SendPasswordResetEmailAsync(email).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled) {
                    Debug.LogError("SendPasswordResetEmailAsync was canceled.");
                    return;
                }
                if (task.IsFaulted) {
                    Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + task.Exception);
                    return;
                }
                
                _sucessMessage.GameObject().SetActive(true);
                _parentPanel.GameObject().SetActive(false);
                Debug.Log("Password reset email sent successfully.");

            });
        }
    }
}
