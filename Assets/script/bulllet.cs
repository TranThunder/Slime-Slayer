using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class bulllet : MonoBehaviour
{
    player Player;
    [SerializeField] GameObject playerobject;
    BoxCollider2D boxCollider;

    Animator animator;
    bool alive=true;
    public float speed= 12f;
    
    bool left = false;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider=GetComponent<BoxCollider2D>();
        
        animator = GetComponent<Animator>();
        playerobject = GameObject.Find("Player");
        if (playerobject.transform.localScale.x > 0)
        {
            left = true;
        }
        movement();
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }
    void movement()
    {
        if (alive == true)
        {


            if (left == true)
            {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            }
            else transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }
    void Bullethitanimation()
    {
        animator.SetTrigger("hit");
    }
    
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    IEnumerator Hitanimation()
    {
        boxCollider.isTrigger = true;
        animator.SetTrigger("hit");
        yield return new WaitForSeconds(0.33f);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        alive = false;
        StartCoroutine(Hitanimation());
    }
}
