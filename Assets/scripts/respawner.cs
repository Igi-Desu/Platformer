using UnityEngine;

public class respawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject whatToRespawn;
    public GameObject animationbefore;
    private void Start()
    {
        Invoke("anim", 4);
        Invoke("spawn", 5);
    }
    void anim()
    {
        Instantiate(animationbefore, transform.position, transform.rotation);
    }
    void spawn()
    {
        Instantiate(whatToRespawn, transform.position,transform.rotation);
        Destroy(gameObject);
    }
}
