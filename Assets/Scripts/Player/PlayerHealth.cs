using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    Animator anim;

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBarScript healthBar;

    private Stopwatch stopwatch;
    private Store store;
    private Record record;

    public float miliSecond;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        //регенерация здоровья
        if (currentHealth < 100)
        {
            miliSecond += 0.02f;
            if (miliSecond >= 1)
            {
                currentHealth += 2;
                healthBar.SetHealth(currentHealth);
                miliSecond = 0;
            }
        }
    }
    //получение урона 
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        StartCoroutine(DamageAnimation());
        if (currentHealth <= 0)
        {
            Die();
            Time.timeScale = 0;
        }
    }
    //смерть и загрузка таблицы рекордов
    void Die()
    {
        anim.SetInteger("anim", 5);
        Destroy(gameObject, 0.8f);

        record = GameObject.FindWithTag("Record").GetComponent<Record>();
        stopwatch = GameObject.FindWithTag("MainCamera").GetComponent<Stopwatch>();
        store= GameObject.FindWithTag("MainCamera").GetComponent<Store>();
        record.SetStoreAndTime(store.store.ToString(),stopwatch.time);
        Time.timeScale = 1;
        SceneManager.LoadScene("Record");
    }
    //анимация получения урона 
    IEnumerator DamageAnimation()
    {
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < 3; i++)
        {
            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 0;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);

            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 1;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);
        }
    }
}
