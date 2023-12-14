using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject slime;
    [SerializeField] GameObject bigslime;
    Gamecontroller gamecontroller;
    public float spawntime=3f;
    public bool resttime=false;
    void Start()
    {
        gamecontroller=FindObjectOfType<Gamecontroller>();
        StartCoroutine(Spawn());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Spawn()
    {
        if (resttime == false)
        {
            Instantiate(slime, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            if (gamecontroller.wave >= 3)
            {
                yield return new WaitForSeconds(Random.Range(0.4f, 0.9f));
                Instantiate(bigslime, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(0.4f, 0.9f));
                Instantiate(slime, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            }
            yield return new WaitForSeconds(spawntime);
            StartCoroutine(Spawn());
        }
        else
        {
            yield return new WaitForSeconds(spawntime);
            StartCoroutine(Spawn());
        }
    }
}
