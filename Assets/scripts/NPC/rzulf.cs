using UnityEngine;

public class rzulf : MonoBehaviour
{
    [SerializeField]Dialogue_Manager dialogue;
    [SerializeField]GameObject Player;
    [SerializeField]GameObject Exclamation;
    [SerializeField]GameObject questcherry;
   
    bool isplayernear = false;
    bool hasquest = true;

    private void Start()
    {
        questcherry.SetActive(false);
    }
    private void Update()
    {
        if (isplayernear&&Input.GetKeyDown(KeyCode.F))
        {
            //spawn cherry when we first start talking 
            if (dialogue.index == 0)
            {
                questcherry.SetActive(true);
            }
            hasquest = false;
            isplayernear = false;
            dialogue.startdialogue();
        }
    }

    void FixedUpdate()
    {
        if (Player == null) return;
        //we can talk when he has quest and player is near
        if (hasquest&&Vector2.Distance(Player.transform.position, transform.position) < 2)
        {
            Exclamation.SetActive(true);
            isplayernear = true;
        }
        else
        {
            Exclamation.SetActive(false);
            isplayernear = false;
        }
    }
    //method called when we collect cherry 
    public void FinishCherryQuest()
    {
        hasquest = true;
        
    }
}
