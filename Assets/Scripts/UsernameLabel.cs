using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEngine;

public class UsernameLabel : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    // Start is called before the first frame update
    void Reset()
    {
        _label = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Start()
    {
        FirebaseAuth.DefaultInstance.StateChanged += HandleAuthChange;
    }

    private void HandleAuthChange(object sender, EventArgs e)
    {
        var currntUser = FirebaseAuth.DefaultInstance.CurrentUser;

        if (currntUser != null)
        {
            SetLabelUSername(currntUser.UserId);
        }
        
    }

    private void SetLabelUSername(string userId)
    {
        FirebaseDatabase.DefaultInstance.GetReference($"users/{userId}/username").GetValueAsync()
            .ContinueWithOnMainThread(task =>
                {
                    if (task.IsFaulted)
                    {

                    }
                    else if (task.IsCompleted)
                    {
                        DataSnapshot snapshot = task.Result;
                        _label.text = (string)snapshot.Value;
                    }
                }
            );
    }
}
