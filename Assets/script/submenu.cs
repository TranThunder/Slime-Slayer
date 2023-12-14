using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class submenu : MonoBehaviour
{
    [SerializeField] GameObject Submenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Pause()
    {
        Submenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Continue()
    {
        Submenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Surrender()
    {
        SceneManager.LoadScene("Menu");
    }
}
