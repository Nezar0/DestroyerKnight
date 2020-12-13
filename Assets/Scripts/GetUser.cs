using System;
using UnityEngine;
using UnityEngine.UI;

public class GetUser : MonoBehaviour
{
    public Text nameText;
    public Text timeText;
    public Text storeText;
    private User user;
    private Record record;
    void Start()
    {
        user = GameObject.FindWithTag("Name").GetComponent<User>();
        record = GameObject.FindWithTag("Record").GetComponent<Record>();
        nameText.text = user.Name;
        try
        {
            storeText.text = record.record;
            timeText.text = record.time;
        }
        catch(NullReferenceException)
        {
            Debug.Log("error");
        }
    }
}
