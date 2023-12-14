using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bigslime : MonoBehaviour
{
    // Start is called before the first frame update
    Gamecontroller gamecontroller;
    player Player;
    public int hp = 20;
    public int damage = 10;
    int money = 0;
    public float movespeed = 1f;
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    Animator animator;
    bool isattacking = false;
    [SerializeField] Slider slider;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        gamecontroller = FindObjectOfType<Gamecontroller>();
        Player = FindObjectOfType<player>();
        hp = hp + (gamecontroller.wave * 14);
        if (gamecontroller.wave >= 3)
        {
            money = Random.Range(10, 40);
        }
        money = Random.Range(30, 100);
        slider.maxValue= hp;
        slider.value = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (isattacking == false)
        {
            transform.position = new Vector2(transform.position.x - movespeed * Time.deltaTime, transform.position.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Player.TakeDamage(damage);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            Takedamage(Player.damage);
            slider.value = hp;

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            StartCoroutine(Colldown());

        }
    }
    void Takedamage(int incomedamage)
    {
        if (Player.x2damage == true)
        {
            incomedamage += incomedamage;
        }
        if (hp - incomedamage > 0)
        {
            hp = hp - incomedamage;
        }
        else
        {
            hp = 0;
            movespeed = 0;
            Player.money += money;
            boxCollider.isTrigger = true;
            rb.isKinematic = true;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            animator.SetTrigger("death");
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);

    }
    IEnumerator Colldown()
    {
        movespeed = 0;
        yield return new WaitForSeconds(0.5f);
        isattacking = false;
        movespeed = 2;


    }
}
