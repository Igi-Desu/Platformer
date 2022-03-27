using UnityEngine;

public class cameramovement : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField]float horizontalborder;
    [SerializeField]float verticalborder;
    [SerializeField] float maxypos;
    [SerializeField] float minypos;
    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        float x = player.transform.position.x - transform.position.x;
        float y = player.transform.position.y- transform.position.y;
        if (Mathf.Abs(x)>horizontalborder||Mathf.Abs(y)>verticalborder)
        {
            float xdiff = (x < 0) ? x + horizontalborder : x - horizontalborder;
            float ydiff = (y < 0) ? y + verticalborder : y - verticalborder;
            if(Mathf.Abs(x) < horizontalborder)
            {
                xdiff = 0;
            } 
            if(Mathf.Abs(y) < horizontalborder)
            {
                ydiff = 0;
            }
            transform.position = new Vector3(transform.position.x+xdiff, Mathf.Clamp(transform.position.y+ydiff, minypos, maxypos), transform.position.z);
            
            return;
        }
        
    }

}
