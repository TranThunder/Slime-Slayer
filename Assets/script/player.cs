    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Bullet;
    Gamecontroller gamecontroller;
    Rigidbody2D rb;
    public int hp = 100;
    public int money = 0;
    public int damage = 3;
    public int ammo = 30;
    public int currentammo;
    public float reloadspeed = 3f;
    int gem;
    bool isreloading = false;
    public bool isjumping = true;
    public bool x2damage = false;
    bool isalive = true;
    public float jumpspeed = 5f;
    public float moveSpeed = 5f;
    bool skillcooldown;
    Animator animator;
    [SerializeField] GameObject deathmenu;
    AudioSource Audiosource;
    [SerializeField] AudioClip shootting;
    [SerializeField] AudioClip gethit;
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip walking;
    [SerializeField] AudioClip landing;
    [SerializeField] AudioClip reloadclip;
    [SerializeField] Slider slider;
    
    private void Awake()
    {
       
    }
    void Start()
    {
        Audiosource = GetComponent<AudioSource>();
        reloadspeed = PlayerPrefs.GetFloat("Reloadspeed");
        gem = PlayerPrefs.GetInt("Gem");
        damage = PlayerPrefs.GetInt("Damage");
        hp = PlayerPrefs.GetInt("Health");
        ammo = PlayerPrefs.GetInt("Ammo");
        animator = GetComponent<Animator>();
        gamecontroller = FindObjectOfType<Gamecontroller>();
        rb = GetComponent<Rigidbody2D>();
        currentammo = ammo; 
        slider.maxValue = hp;
        slider.value = hp;

    }
    void Update()
    {
        if (isalive ==  true)
        {
            checkrun();
            checkreload();
            playermove();
            shoot();
            Upragrade();
            
        }
    }
    void playermove()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);

            }
            else transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {

            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);

            }
            else transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isjumping == false)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
                isjumping = true;
                Audiosource.PlayOneShot(jump);
            }
        }
    }
    
  
    
    void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentammo > 0 && isreloading == false)
            {
                if (transform.localScale.x > 0)
                {
                    Instantiate(Bullet, new Vector2(transform.position.x + 0.7f, transform.position.y - 0.18f), Quaternion.identity);
                }
                else
                {
                    Instantiate(Bullet, new Vector2(transform.position.x - 0.7f, transform.position.y - 0.18f), Quaternion.identity);
                }
                currentammo -= 1;
                Audiosource.PlayOneShot(shootting);
            }
        }
    }
    void checkreload()
    {
        if (currentammo == 0 || Input.GetKeyDown(KeyCode.R))
        {
            if (gamecontroller.playerammo > 0 && isreloading == false)
            {
                StartCoroutine(reload());
            }

        }
    }
    void checkrun()
    {
        if (rb.velocity.x < 0 || rb.velocity.x > 0)
        {
            animator.SetTrigger("run");
            animator.ResetTrigger("Stand");
        }
        else
        {
            animator.ResetTrigger("run");
            animator.SetTrigger("Stand");

        }
    }
    IEnumerator reload()
    {
        isreloading = true;
        Audiosource.PlayOneShot(reloadclip);
        yield return new WaitForSeconds(reloadspeed);
        if (gamecontroller.playerammo >ammo)
        {
            gamecontroller.playerammo -= (ammo - currentammo);
            currentammo = ammo;

        }
        if (gamecontroller.playerammo <= ammo && gamecontroller.playerammo != 0)
        {
            if((ammo-currentammo)>gamecontroller.playerammo)
            {
                currentammo += gamecontroller.playerammo;
                gamecontroller.playerammo = 0;
            }
            else
            {
                gamecontroller.playerammo=gamecontroller.playerammo-(ammo-currentammo);
                currentammo = ammo;
            }

        }
        isreloading = false;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Ground")
        {
            isjumping = false;
            Audiosource.PlayOneShot(landing);
        }
        
        
    }
    void Upragrade()
    {
        if(money>=500 && Input.GetKeyDown(KeyCode.Alpha1))
        {
           
            damage += 2;
            money -= 500;            
        }
        if (money >= 250 && Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(gamecontroller.playerammo<120)
            {
               
                if (gamecontroller.playerammo > 90)
                {
                    gamecontroller.playerammo = 120;
                }
                else gamecontroller.playerammo += 30;
                money -= 200;
            }
        }
        if (money >= 500 && Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(x2damage==false)
            {
                
                money -= 500;
                StartCoroutine(DoubleDamage());
            }
        }
    }
 

    IEnumerator DoubleDamage()
    {
        x2damage = true;
        yield return new WaitForSeconds(10);
        x2damage = false;
        
    }
    public void TakeDamage(int incomedamage)
    {
        if (hp - incomedamage <= 0)
        {
            hp = 0;
            Audiosource.PlayOneShot(gethit);
            isalive = false;
            slider.value = 0;
            Time.timeScale = 0;
            deathmenu.SetActive(true);
            PlayerPrefs.SetInt("Gem", gem + gamecontroller.wave * 10);
        }
        else
        {
            slider.value = hp;
            hp -= incomedamage;
            Audiosource.PlayOneShot(gethit);
            rb.velocity = new Vector2(-5, 4);   
        }
    }
}
