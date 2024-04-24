using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveBestScore : MonoBehaviour
{

    public static SaveBestScore Instance;

    public int BestScore= 0;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
