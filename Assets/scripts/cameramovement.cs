using UnityEngine;

public class cameramovement : MonoBehaviour
{
    //camera script that doesn't follow 1-1 but follows only if player exists small non follow box
    //could be done using cinemachines but I tried to program it myself

    //target and camera borders
    [SerializeField] GameObject target;
    [SerializeField]float horizontalborder;
    [SerializeField]float verticalborder;
    [SerializeField] float maxypos;
    [SerializeField] float minypos;
    
    void Update()
    {
        //if there's nothing to follow return
        if (target == null) return;
        //get x and y distance
        float x = target.transform.position.x - transform.position.x;
        float y = target.transform.position.y- transform.position.y;
        if (Mathf.Abs(x)>horizontalborder||Mathf.Abs(y)>verticalborder)
        {
            //calculate possible camera change
            float xdiff = (x < 0) ? x + horizontalborder : x - horizontalborder;
            float ydiff = (y < 0) ? y + verticalborder : y - verticalborder;
            //if x or y is lower than box borders don't move at all
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
