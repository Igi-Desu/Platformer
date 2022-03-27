using UnityEngine;

public class Cherry : MonoBehaviour
{
    // Start is called before the first frame update
    bool help = false;
    [SerializeField] Gamemanager gamemanager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player")&&!help)
        {
            FindObjectOfType<audiomanager>().Play("BerryCollect");
            help = true;
            Player_Movement p = collision.transform.GetComponent<Player_Movement>();
            p.increaselife();
            Destroy(gameObject);
        }
    }
}
