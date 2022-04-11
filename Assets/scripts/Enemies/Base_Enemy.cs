using UnityEngine;

public class Base_Enemy : MonoBehaviour
{
    //base enemy has only deathanim
    [SerializeField] protected GameObject DeathAnim;

     virtual protected void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            FindObjectOfType<audiomanager>().Play("EnemyDeath");
            Instantiate(DeathAnim, transform.position, DeathAnim.transform.rotation);
        }
    }

}
