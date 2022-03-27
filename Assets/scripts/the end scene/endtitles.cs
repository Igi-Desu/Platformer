using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endtitles : MonoBehaviour
{

    [SerializeField]RectTransform titles;
    float speed = 50;
    // Update is called once per frame
    private void Start()
    {
        
    }
    void FixedUpdate()
    {
        titles.transform.position += new Vector3(0,speed * Time.deltaTime,0);
        if (titles.transform.position.y > 300)
        {
            Application.Quit();
        }
    }
}
