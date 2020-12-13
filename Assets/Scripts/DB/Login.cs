using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
	public GameObject username;
	public GameObject password;
	private string Username;
	private string Password;
	private User user;

	private void Start()
	{
		username.GetComponent<InputField>().text = "";
		password.GetComponent<InputField>().text = "";
		StartCoroutine(OldLoginUser());
	}
	//для клика по кнопке “Login” которая вызывает LoginUser.
	//LoginUser вызывает login.php.и если login.php возвращает имя пользователя загружается сцена “Main” (сама игра)
	public void LoginButton()
	{
		bool UN = false;
		bool PW = false;
		if (Username != "")
		{
			UN = true;
		}
		else
		{
			Debug.LogWarning("Username Field Empty");
		}
		if (Password != "")
		{
			PW = true;
		}
		else
		{
			Debug.LogWarning("Password Field Empty");
		}
		if (UN == true && PW == true)
		{
			StartCoroutine(LoginUser());
			user = GameObject.FindWithTag("Name").GetComponent<User>();
			user.SetName(Username);
		}
	}
	//вызывает login.php скрипт который авторизорует пользователя
	private IEnumerator LoginUser()
	{
		WWWForm form = new WWWForm();
		form.AddField("Name", Username);
		form.AddField("Pass", Password);
		WWW www = new WWW("http://localhost/my_php/DestroyerKnight/login.php", form);
		yield return www;
		if (www.error != null)
		{
			Debug.Log("error " + www.error);
			yield break;
		}
		Debug.Log("server onswer: " + www.text);
		if (www.text == user.Name)
		{
			print("Login Sucessful");
			SceneManager.LoadScene("Main");
		}
	}
	//вызывает cookie.php скрипт который хранит в себе данные прошлого авторизованного пользователя.
	private IEnumerator OldLoginUser()
	{
		WWWForm form = new WWWForm();
		WWW www = new WWW("http://localhost/my_php/DestroyerKnight/cookie.php");
		yield return www;
		if (www.error != null)
		{
			Debug.Log("error " + www.error);
			yield break;
		}
		if (www.text != "")
		{
			Debug.Log("server onswer: " + www.text);
			username.GetComponent<InputField>().text = www.text;
		}
	}

	void Update()
	{
		Username = username.GetComponent<InputField>().text;
		Password = password.GetComponent<InputField>().text;
	}
}
