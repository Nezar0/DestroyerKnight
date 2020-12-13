using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour
{
    public float miliSecond;
    public float second;
    public float minute;

    public string time;

    public Text text;
    private void FixedUpdate()
    {
        miliSecond += 0.02f;
        if(miliSecond >= 1)
        {
            second++;
            miliSecond = 0;
        }
        if(second == 60)
        {
            minute++;
            second = 0;
        }
        time = $"{minute} : {second}";
        text.text = $"Time: {minute} : {second}";
    }
}
