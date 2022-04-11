using UnityEngine;

public class questcherry : MonoBehaviour
{
    //cherry that helps complete quest
    [SerializeField]rzulf qrzulf;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") )
        {
            FindObjectOfType<audiomanager>().Play("BerryCollect");
            qrzulf.FinishCherryQuest();
            Destroy(gameObject);
        }
    }
}
