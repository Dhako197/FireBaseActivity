using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;

public class Login : MonoBehaviour
{

    [SerializeField] private Button _loginButton;

    [SerializeField] private TMP_InputField _emailInputField;

    [SerializeField] private TMP_InputField _passwordInputField;
    
    private void Reset()
    {
        _loginButton = GetComponent<Button>();
        _emailInputField = GameObject.Find("InputFieldEmail").GetComponent<TMP_InputField>();
        _passwordInputField = GameObject.Find("InputFieldPassword").GetComponent<TMP_InputField>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _loginButton.onClick.AddListener(HandleLoginButtonClicked);
    }

    private void HandleLoginButtonClicked()
    {
        var auth = FirebaseAuth.DefaultInstance;

        auth.SignInWithEmailAndPasswordAsync(_emailInputField.text, _passwordInputField.text).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled");
                return;
            }

            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: "+ task.Exception);
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", result.User.DisplayName, result.User.UserId);

        });
    }

    
}
