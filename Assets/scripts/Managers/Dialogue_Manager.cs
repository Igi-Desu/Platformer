using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_Manager : MonoBehaviour
{
    [SerializeField]Gamemanager manager;
    public string[] sentence;
    public float typingspeed;
    public int index=0;
    public Text text;
    [SerializeField]GameObject continueButton;
    [SerializeField] GameObject questcherry;
    [SerializeField] GameObject dialoguebackground;
    private void Start()
    {
        //prepare objects
        dialoguebackground.SetActive(false);
        text.text = "";
        continueButton.SetActive(false);
    }
    public void startdialogue()
    {
        manager.SetCutsceneBool(true);
        StartCoroutine(dialogue());
    }
    public void Continue()
    {
        //continue dialogue - go to the next sentence
        continueButton.SetActive(false);
        text.text = "";
        //indexes mean sentences so I know how many of them there are and when to stop dialogue and when not to
        //another way would be to create structure that has sentence string and some function that we want to call
        //when we start/end that sentence 
        if (index<=2)
        {
            StartCoroutine(dialogue());
        }
        else
        {
            dialoguebackground.SetActive(false);
            manager.SetCutsceneBool(false);
        }
        if (index == 4)
        {
            Destroy(GameObject.Find("InvisiblewallXD").gameObject);
        }
    }
    IEnumerator dialogue()
    {
        dialoguebackground.SetActive(true);
        foreach (var singlechar in sentence[index].ToCharArray())
        {
            text.text += singlechar;
            //sans like talking 
            if (singlechar != ' ') FindObjectOfType<audiomanager>().Play("TutelTalk");
            yield return new WaitForSeconds(typingspeed);
        }
        index++;
        continueButton.SetActive(true);
    }
}
