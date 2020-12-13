using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class User : MonoBehaviour
{
    public string Name;
    void Start()
    {
        DontDestroyOnLoad(this);
        
    }
    public void SetName(string user)
    {
        Name = user;
    }
}
