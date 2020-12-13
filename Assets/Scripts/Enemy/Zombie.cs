using UnityEngine;

public class Zombie : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Transform player;

    bool attack = false;
    public float speed = 10f;

    public int attackDamage = 1;
    public Vector3 attackOffset;
    public float attackRange = 1.9f;
    public LayerMask attackMask;

    public int maxHealth = 100;
    public int currentHealth;

    private Store store;
    public int storeValue;

    private int faceRight = 1;

    private void Start()
    {
        store = GameObject.FindWithTag("MainCamera").GetComponent<Store>();
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        //движение врага за игроком 
        Vector2 target = new Vector2(player.position.x, rb.position.y);

        if (player.position.x < rb.position.x)
        {
            sr.flipX = true;
            faceRight *= -1;
        }
        else
        {
            faceRight *= -1;
            sr.flipX = false;
        }
        if (attack == true)
        {
            anim.SetInteger("Zombie", 1);
            Attack();
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
        else
        {
            anim.SetInteger("Zombie", 0);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
    }
    //атака врага 
    void Attack()
    {
        anim.SetInteger("Zombie", 1);
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
       
        Collider2D hitEnemies = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (hitEnemies != null)
        {
            if (hitEnemies.GetComponent<PlayerHealth>().currentHealth > 0)
            {
                hitEnemies.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            }
        }
    }
    //радиус атаки
    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
    //получение урона
    public void TakeDamage(int damage)
    {
        rb.AddForce(Vector2.right * 1000 * faceRight);
        currentHealth -= damage;
        anim.SetInteger("Zombie", 2);
        Debug.Log(currentHealth);     
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    //смерть 
    void Die()
    {
        store.AddStore(1);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            attack = true;
        }
        else
        {
            attack = false;
        }
    }
}
