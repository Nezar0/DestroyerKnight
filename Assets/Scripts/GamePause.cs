using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
	private bool paused = false;
	public GameObject panel;
	private void Start()
	{
		panel.SetActive(false);
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!paused)
			{
				Time.timeScale = 0;
				paused = true;
				panel.SetActive(true);
			}
			else
			{
				Time.timeScale = 1;
				paused = false;
				panel.SetActive(false);
			}
		}
	}
	public void BtnStartAgain()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("Main");
	}
	public void LoginMenu()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("Login");
	}
}