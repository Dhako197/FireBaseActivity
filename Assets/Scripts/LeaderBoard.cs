using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using System.Linq;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private GameObject _scoreEntryPrefab;

    [SerializeField]private List<ScoreEntry> leaderBoard;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        GetUsersHighestScores();
    }

    public void GetUsersHighestScores()
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("users").OrderByChild("score").LimitToLast(4)
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log(task.Exception);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    int i = 0;

                    foreach (var userDoc in (Dictionary<string,object>)snapshot.Value)
                    {
                        var userObject = (Dictionary<string, object>)userDoc.Value;
                        //userObject = userObject.OrderByDescending(x => x["score"]);
                        
                        Debug.Log(userObject["username"] + ":" + userObject["score"]);
                        ScoreEntry setValues = leaderBoard[i].GetComponent<ScoreEntry>();
                        setValues.SetLable(""+userObject["username"],""+userObject["score"]);

                        i++;

                    }
                    
                }

            });
    }
}
