using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;


public class Singup : MonoBehaviour
{
    [SerializeField]
    private Button _registrationButton;
    private Coroutine _signupCoroutine;
    

    // Start is called before the first frame update
    void Reset()
    {
        _registrationButton = GetComponent<Button>();
    }

    private void Start()
    {
       // _registrationButton.clicked += HandleRegisterButtonClicked;
       _registrationButton.onClick.AddListener(HandleRegisterButtonClicked);
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
        }
    }

    
}
