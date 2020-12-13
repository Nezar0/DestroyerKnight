using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour
{
    public string record = "";
    public string time = "";
    void Start()
    {
        DontDestroyOnLoad(this);

    }
    public void SetStoreAndTime(string store, string userTime)
    {
        record = store;
        time = userTime;
    }
}
