using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boss : MonoBehaviour
{
    Rigidbody2D rb;
    public float movespeed =2f;
    public int hp = 100;
    player Player;
    public int damage = 20;
    BoxCollider2D boxCollider;
    Animator animator;
    Gamecontroller gamecontroller;
    [SerializeField] Slider slider;
    //them 
    // Transform player;

    // Start is called before the first frame update
    void Start()
    {
        gamecontroller = FindObjectOfType<Gamecontroller>();
        Player = FindObjectOfType<player>();
        rb=GetComponent<Rigidbody2D>();
        boxCollider=GetComponent<BoxCollider2D>();
        animator=GetComponent<Animator>();
        hp = hp + ((gamecontroller.wave / 3)*60);
        slider.maxValue = hp;
        slider.value = hp;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x-movespeed*Time.deltaTime,transform.position.y);
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1);
        animator.ResetTrigger("attack");

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Player.hp -= damage;
            animator.SetTrigger("attack");
            StartCoroutine(Attack());
        }
        


        if (collision.gameObject.tag == "Bullet")
        {
            Takedamage(Player.damage);
            slider.value = hp;
            StartCoroutine(Gethit());
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        animator.ResetTrigger("gethit");
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
            animator.SetTrigger("gethit");
        }
        else
        {
            hp = 0;
            movespeed = 0;
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
    IEnumerator Gethit()
    {
        movespeed = 0;
        yield return new WaitForSeconds(0.2f);
        movespeed = 2;
    }   
    
    //phần thành viên thêm 
    /* public void OaStateEnter(Animation animation,AnimationState stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animation.GetComponent<Rigidbody2D>();
    }
    public void OaStateUpdate(Animation animation, AnimationState stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos  = Vector2.MoveTowards(rb.position, target, movespeed);
        rb.MovePosition(newPos);
    }
    public void OaStateExit(Animation animation, AnimationState stateInfo, int layerIndex)
    {

    }
    */
}
