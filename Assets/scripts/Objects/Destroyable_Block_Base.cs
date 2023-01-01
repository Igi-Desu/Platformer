using UnityEngine;

public class Destroyable_Block_Base : MonoBehaviour
{
    [SerializeField] GameObject Destroy_Anim;

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            Instantiate(Destroy_Anim, transform.position, transform.rotation);
        }
    }
}
