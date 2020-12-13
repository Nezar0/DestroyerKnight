using System.Text.RegularExpressions;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Register : MonoBehaviour
{
    public GameObject username;
    public GameObject email;
    public GameObject password;
    public GameObject confPassword;
    private string Username;
    private string Email;
    private string Password;
    private string ConfPassword;

    //для клика по кнопке “Register” которая вызывает RegisterUser.
    public void RegisterButton()
    {
        bool UN = false;
        bool EM = false;
        bool PW = false;
        bool CPW = false;

        if (Username != "")
        {
            UN = true;
        }
        else
        {
            Debug.LogWarning("Username field Empty");
        }
        if (Email != "")
        {
            if (isValid(Email))
            {
                EM = true;
            }
            else
            {
                Debug.LogWarning("Email is Incorrect");
            }
        }
        else
        {
            Debug.LogWarning("Email Field Empty");
        }
        if (Password != "")
        {
            if (Password.Length > 5)
            {
                PW = true;
            }
            else
            {
                Debug.LogWarning("Password Must Be atleast 6 Characters long");
            }
        }
        else
        {
            Debug.LogWarning("Password Field Empty");
        }
        if (ConfPassword != "")
        {
            if (ConfPassword == Password)
            {
                CPW = true;
            }
            else
            {
                Debug.LogWarning("Passwords Don't Match");
            }
        }
        else
        {
            Debug.LogWarning("Confirm Password Field Empty");
        }
        if (UN == true && EM == true && PW == true && CPW == true)
        {
            username.GetComponent<InputField>().text = "";
            email.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
            confPassword.GetComponent<InputField>().text = "";
            StartCoroutine(RegisterUser());
        }
    }
    //вызывает register.php для регистрации пользователя 
    private IEnumerator RegisterUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", Username);
        form.AddField("Pass", Password);
        form.AddField("Email", Email);
        WWW www = new WWW("http://localhost/my_php/DestroyerKnight/register.php", form);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("error " + www.error);
            yield break;
        }
        Debug.Log("server onswer: " + www.text);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (Password != "" && Email != "" && ConfPassword != "" && Username != "")
            {
                RegisterButton();
            }
        }
        Username = username.GetComponent<InputField>().text;
        Email = email.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        ConfPassword = confPassword.GetComponent<InputField>().text;
    }
    //проверка для почты 
    bool isValid(string email)
    {
        string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
        Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
        return isMatch.Success;
    }
}
