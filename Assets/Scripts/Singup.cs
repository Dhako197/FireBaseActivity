using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;
using Firebase.Database;


public class Singup : MonoBehaviour
{
    [SerializeField]
    private Button _registrationButton;
    private Coroutine _signupCoroutine;
    
    private DatabaseReference mDatabaseRef;

    [SerializeField] private TMP_InputField _usernameInputField;

    // Start is called before the first frame update
    void Reset()
    {
        _registrationButton = GetComponent<Button>();
        _usernameInputField = GameObject.Find("InputFieldUsername").GetComponent<TMP_InputField>();
    }

    private void Start()
    {
       // _registrationButton.clicked += HandleRegisterButtonClicked;
       _registrationButton.onClick.AddListener(HandleRegisterButtonClicked);
       mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void HandleRegisterButtonClicked()
    {
        string email = GameObject.Find("InputFieldEmail").GetComponent<TMP_InputField>().text;
        string password = GameObject.Find("InputFieldPassword").GetComponent<TMP_InputField>().text;
        _signupCoroutine = StartCoroutine(RegisterUser(email, password));
    }

    private IEnumerator RegisterUser(string email, string password)
    {
        var auth = FirebaseAuth.DefaultInstance;
        var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => registerTask.IsCompleted);

        if (registerTask.IsCanceled)
        {
            Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled");
        }
        else if (registerTask.IsFaulted)
        {
            Debug.LogError("CreateUserWithEmailAndPasswordAsync encoureterd an error "+ registerTask.Exception);
        }
        else
        {
            // Se creo
            AuthResult result = registerTask.Result;
            Debug.LogFormat("Firebase user created successfuly: {0} ({1})", result.User.DisplayName, result.User.UserId);

            mDatabaseRef.Child("users").Child(result.User.UserId).Child("username")
                .SetValueAsync(_usernameInputField.text);
        }
    }

    
}
