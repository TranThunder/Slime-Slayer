using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgrounscale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float height = Camera.main.orthographicSize * 2f;
        float width = height * screenWidth / screenHeight;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float imagewidth = sr.bounds.size.x;
        float imageheight = sr.bounds.size.y;
        float newimagewidth = width / imagewidth;
        float newimageheight = height / imageheight;
        transform.localScale = new Vector3(newimagewidth, newimageheight, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
