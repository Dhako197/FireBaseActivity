using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreEntry : MonoBehaviour
{
   [SerializeField] private TMP_Text _labelUSername;
   
   [SerializeField] private TMP_Text _labelScore;

   public void SetLable(string username, string score)
   {
      _labelUSername.text = username;

      _labelScore.text = "" + score;
   }
}
