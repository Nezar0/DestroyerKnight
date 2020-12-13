using System;
using UnityEngine;

public class PlayerContrl : MonoBehaviour
{
    public GameObject Fireball;
    public ManaBarScripts manaBar;
    public int maxMana = 100;
    public int currentMana;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    private int exctaJump;

    public float speed = 20f;
    private Rigidbody2D rb;

    private bool faceRight = true;
    Animator anim;
    
    public Transform attackPoint;
    public int attackDamage = 40;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public float miliSecond;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentMana = maxMana;
    }

    void FixedUpdate()
    {
        //регенерация маны 
        if (currentMana < 100)
        {
            miliSecond += 0.02f;
            if (miliSecond >= 1)
            {
                currentMana += 2;
                manaBar.SetMana(currentMana);
                miliSecond = 0;
            }
        }

        //проверка стоит ли герой на полу
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        //передвижение героя 
        float moveX = Input.GetAxis("Horizontal");

        if (moveX == 0 && isGrounded == true)
        {
            anim.SetInteger("anim", 0);
        }

        rb.MovePosition(rb.position + Vector2.right * moveX * speed * Time.deltaTime);
        if (moveX != 0 && isGrounded == true)
        {
            anim.SetInteger("anim", 1);          
        }      
       
        if(isGrounded == true)
        {
            exctaJump = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && exctaJump > 0) 
        {
            exctaJump--;
            anim.SetInteger("anim", 2);
            rb.AddForce(Vector2.up * 16000);
        }

        if (moveX > 0 && !faceRight)
            flip();
        else
            if (moveX < 0 && faceRight)
            flip();
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -180f, 180f),
            Mathf.Clamp(transform.position.y, -21f, 21f),
            transform.position.z);
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnFireball();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }
    }
    //атака героя 
    void Attack()
    {
        anim.SetInteger("anim", 4);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Zombie>().currentHealth > 0)
            {
                enemy.GetComponent<Zombie>().TakeDamage(attackDamage);
            }
        }
    }
    //радиус атаки 
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    //разворот героя
    void flip()
    {
        faceRight = !faceRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    //создание fireball
    void SpawnFireball()
    {
        if (currentMana >= 25)
        {
            currentMana -= 25;
            manaBar.SetMana(currentMana);
            anim.SetInteger("anim", 3);
            if (faceRight == false)
            {
                Fireball.GetComponent<SpriteRenderer>().flipX = true;
                Instantiate(Fireball, new Vector2(transform.position.x - 1.3f, transform.position.y - 1.5f), Quaternion.identity);
            }
            else
            if (faceRight == true)
            {
                Fireball.GetComponent<SpriteRenderer>().flipX = false;
                Instantiate(Fireball, new Vector2(transform.position.x + 1.3f, transform.position.y - 1.5f), Quaternion.identity);
            }
        }
    }
}
