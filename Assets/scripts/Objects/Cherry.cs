using UnityEngine;

public class Cherry : MonoBehaviour
{
    
    [SerializeField] Gamemanager gamemanager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            FindObjectOfType<audiomanager>().Play("BerryCollect");
            Player_Movement p = collision.transform.GetComponent<Player_Movement>();
            p.increaselife();
            Destroy(gameObject);
        }
    }
}
