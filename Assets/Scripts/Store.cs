using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public Text storeText;
    public int store;
    
    void Start()
    {
        store = 0;
        UpdateStore();
    }
    void UpdateStore()
    {
        storeText.text = "Store: " + store;
    }

    public void AddStore(int newStoreValue)
    {
        store += newStoreValue;
        UpdateStore();
    }
}
