using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Flashscreen : MonoBehaviour
{
   
    Image image;
    Coroutine routine=null;
    void Start()
    {
        image = GetComponent<Image>();
    }
    //maximumopacity is a number between 0 and 1 
    public void Flash(float flashspeed,float maximumopacity, Color color)
    {
        image.color = color;
        //if we are currently in some coroutine stop it 
        if (routine != null)
            StopCoroutine(routine);
        routine = StartCoroutine(flash(flashspeed, maximumopacity));
    }
    //first smoothly flash color in then out 
    IEnumerator flash(float frequency, float maxalpha)
    {
        float flashinoutdur = frequency / 2;
        //make sure we start at alpha 0 
        Color color = image.color;
        color.a = 0;
        image.color = color;
        //flash in
        for (float t=0; t<=flashinoutdur; t += Time.deltaTime)
        {
            Color currentcolor = image.color;
            currentcolor.a = Mathf.Lerp(0, maxalpha, t / flashinoutdur);
            image.color = currentcolor;
            yield return null;
        }
        //flash out
        for(float t=0; t<=flashinoutdur; t += Time.deltaTime)
        {
            Color currentcolor = image.color;
            currentcolor.a = Mathf.Lerp(maxalpha,0 , t / flashinoutdur);
            image.color = currentcolor;
            yield return null;
        }
        image.color = new Color32(0, 0, 0, 0);
        routine = null;
    }
}
