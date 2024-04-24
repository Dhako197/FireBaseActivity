using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePool : MonoBehaviour
{
    public GameObject PipePrefab;
    public GameObject NightPipePrefab;
    
    public int initialAmount = 10;

    private List<GameObject> pipes = new List<GameObject>();
    private List<GameObject> nightPipes = new List<GameObject>();
    
    private static PipePool instance;
    public static PipePool Instance { get { return instance; } }
    
    
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        FillPoll();
    }

    void FillPoll()
    {
        for (int t = 0; t < initialAmount; t++)
        {
            GameObject go = Instantiate(PipePrefab);
            go.SetActive(false);
            pipes.Add(go);
            
            GameObject go2 = Instantiate(NightPipePrefab);
            go2.SetActive(false);
            nightPipes.Add(go2);
        }
    }

    public GameObject Get()
    {
        GameObject ret;

        if (pipes.Count > 0)
        {
            ret = pipes[pipes.Count - 1];
            pipes.RemoveAt(pipes.Count-1);
        }
        else
        {
            ret = Instantiate(PipePrefab);
        }
        
        ret.SetActive(true);
        return ret;
    }

    public void Return(GameObject go)
    {
        go.SetActive(false);
        pipes.Add(go);
    }

    public GameObject GetNight()
    {
        GameObject ret;

        if (nightPipes.Count > 0)
        {
            ret = nightPipes[nightPipes.Count - 1];
            nightPipes.RemoveAt(nightPipes.Count-1);
        }
        else
        {
            ret = Instantiate(NightPipePrefab);
        }
        ret.SetActive(true);
        return ret;

    }

    public void ReturnNight(GameObject go)
    {
        go.SetActive(false);
        nightPipes.Add(go);
    }
    
}
