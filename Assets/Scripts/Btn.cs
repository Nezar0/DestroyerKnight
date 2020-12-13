using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Btn : MonoBehaviour
{
    public void BtnStartAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }
    public void LoginMenu()
    {
        Time.timeScale = 1;
        Debug.Log("Down");
        SceneManager.LoadScene("Login");
    }
}
