using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    public Transform attackPoint;
    public int attackDamage = 40;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        //движение fireball
        if(sr.flipX == false)
        {          
            rb.AddForce(Vector2.right *  50, ForceMode2D.Force);
        }
        else if (sr.flipX == true)
        {
            rb.AddForce(- Vector2.right *  50, ForceMode2D.Force);
        }
        //столкновение с врагом 
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Zombie>().currentHealth > 0)
            {
                enemy.GetComponent<Zombie>().TakeDamage(attackDamage);
            }
            Destroy(gameObject);
        }
        Destroy(gameObject, 1f);
    }
}
 