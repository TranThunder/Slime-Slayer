using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject invetory;
    [SerializeField] Text gemdisplay;
    [SerializeField] Text damagedisplay;
    [SerializeField] Text hpdisplay;
    [SerializeField] Text ammodisplay;
    [SerializeField] Text reloadspeeddisplay;
    [SerializeField] GameObject menudisplay;
    [SerializeField] GameObject howtoplaydisplay;
    public GameObject[] htp;
    int htps;
    AudioSource audioSource;
    int gem;
    int damage;
    int hp;
    int ammo;
    public float reloadspeed;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gem = PlayerPrefs.GetInt("Gem");
        damage = PlayerPrefs.GetInt("Damage");
        hp = PlayerPrefs.GetInt("Health");
        ammo = PlayerPrefs.GetInt("Ammo");
        reloadspeed = PlayerPrefs.GetFloat("Reloadspeed");
        firstload();
    }

    // Update is called once per frame
    void Update()
    {
        gemdisplay.text = $"Gem:{gem}";
        damagedisplay.text = $"Damage:{damage}\n200 gem = +2 damage";
        ammodisplay.text = $"Ammo:{ammo}\n1000 gem = +1 ammo";
        hpdisplay.text = $"Health:{hp}\n400 gem = +5 hp";
        string s=reloadspeed.ToString();
        if(s.Length>3)
        {
            s=s.Substring(0,3);
        }    
        reloadspeeddisplay.text=$"Reload speed:{s}s\n1000 gem = -0.1s";
       

    }
    public void startgame()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }
    public void shopscene()
    {
        SceneManager.LoadScene("Shop");
    }
    public void invetoryscene()
    {
        invetory.SetActive(true);
        menudisplay.SetActive(false);

    }
    public void backtomenu()
    {
        invetory.SetActive(false);
        menudisplay.SetActive(true);
    }
    public void howtoplay()
    {
        howtoplaydisplay.SetActive(true);

    }
    public void exitgame()
    {
        Application.Quit();
    }
    public void upgradedamage()
    {
        if(gem>=200)
        {
            damage += 1;
            gem -= 200;
            PlayerPrefs.SetInt("Damage", damage);
            PlayerPrefs.SetInt("Gem", gem);
          

        }
        

    }
    public void upgradehealth()
    {
        if (gem >= 400)
        {
            hp += 5;
            gem -= 400;
            PlayerPrefs.SetInt("Health", hp);
            PlayerPrefs.SetInt("Gem", gem);
          
        }
    }
    public void upgradeammo()
    {
        if (gem >= 1000)
        {
            ammo+=1;
            gem -= 1000;
            PlayerPrefs.SetInt("Ammo", ammo);
            PlayerPrefs.SetInt("Gem", gem);
       
        }
    }
    public void upgradereloadspeed()
    {
        if(gem>=1000&&reloadspeed>= 1.1)
        {
            reloadspeed -= 0.1f;
            gem -= 1000;
            PlayerPrefs.SetFloat("Reloadspeed", reloadspeed);
            PlayerPrefs.SetInt("Gem", gem);
          
        }
    }
    void firstload()
    {
        if(damage==0)
        {
            damage = 3;
            PlayerPrefs.SetInt("Damage",damage);
        }
        if(hp==0)
        {
            hp = 100;
            PlayerPrefs.SetInt("Health", hp);
        }
        if(ammo==0)
        {
            ammo = 30;
            PlayerPrefs.SetInt("Ammo", ammo);
        }
        if(reloadspeed==0)
        {
            reloadspeed = 3f;
            PlayerPrefs.SetFloat("Reloadspeed", reloadspeed);
        }
    }
    public void next()
    {
        htps += 1;
        if(htps>3)
        {
            htps = 0;
        }
        for(int i=0;i<htp.Length;i++)
        {
            htp[i].gameObject.SetActive(false);
            htp[htps].gameObject.SetActive(true);
        }
    }
    public void previous()
    {
        htps -= 1;
        if(htps<0)
        {
            htps = 3;
        }
        for (int i = 0; i < htp.Length; i++)
        {
            htp[i].gameObject.SetActive(false);
            htp[htps].gameObject.SetActive(true);
        }
    }
}
