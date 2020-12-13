using UnityEngine;
using UnityEngine.UI;

public class ManaBarScripts : MonoBehaviour
{
    public Slider slider;

    public Image fill;

    public void SetMaxMana(int mana)
    {
        slider.maxValue = mana;
        slider.value = mana;
    }

    public void SetMana(int mana)
    {
        slider.value = mana;
    }

}
