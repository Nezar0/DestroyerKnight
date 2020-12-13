using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class ListRecord : MonoBehaviour
{
    private User user;
    private Record record;
	private ScrolView scrolView;

	void Start()
    {
        user = GameObject.FindWithTag("Name").GetComponent<User>();
        record = GameObject.FindWithTag("Record").GetComponent<Record>();
		scrolView = GameObject.Find("Content").GetComponent<ScrolView>();
		StartCoroutine(RecordUser());
		scrolView.StartU();
	}
	//вызывает record.php
	private IEnumerator RecordUser()
	{
		WWWForm form = new WWWForm();
		form.AddField("Name", user.Name);
		form.AddField("Time", record.time);
		form.AddField("Store", record.record);
		WWW www = new WWW("http://localhost/my_php/DestroyerKnight/record.php", form);
		yield return www;
		if (www.error != null)
		{
			Debug.Log("error " + www.error);
			yield break;
		}
		Debug.Log("server onswer: " + www.text);
	}
}
