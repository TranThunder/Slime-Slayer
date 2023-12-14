using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamecontroller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text moneytext;
    [SerializeField] Text Ammotext;
    [SerializeField] Text Wavetext;
    [SerializeField] Text prize;
    public int timeperwave;
    public int wave = 1;
    player Player;
    Spawner spawn;
    public int playerammo=120;
    [SerializeField] GameObject bosses;
    [SerializeField] GameObject spawner;
    AudioSource audioSource;
    
   
    void Start() 
    {
        spawn=FindObjectOfType<Spawner>();
        audioSource = GetComponent<AudioSource>();
        spawner=GameObject.Find("SpawnSlime");
        Player =FindObjectOfType<player>();
        StartCoroutine(waveincrease());
        audioSource.Play();
        prize.text = $"x{wave * 10}";
    }   

    // Update is called once per frame
    void Update()
    {
        DisplayText();       
    }
    IEnumerator waveincrease()
    {
        yield return new WaitForSeconds(timeperwave);
        
        if (spawn.spawntime >= 2f)
        {
            spawn.spawntime -= 0.25f;
        }
        wave++;
        prize.text = $"x{wave * 10}";
        if (wave % 3 == 0)
        {
            spawn.resttime = true;
            yield return new WaitForSeconds(4);
            Instantiate(bosses, new Vector2(spawner.transform.position.x, spawner.transform.position.y), Quaternion.identity);
            spawn.resttime = false;
        }
       
        StartCoroutine(waveincrease());
        
        
    }
    void DisplayText()
    {
        moneytext.text = $"Money:{Player.money}";
        Ammotext.text = $"{Player.currentammo}/{playerammo}";
        Wavetext.text = $"Wave {wave}";
        
    }
    
    
   
    public void restart()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }
    public void menu()
    {
        SceneManager.LoadScene("Menu");
    }
}