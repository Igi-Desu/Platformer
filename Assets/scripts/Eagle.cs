using UnityEngine;

public class Eagle : Base_Enemy
{
    // Start is called before the first frame update
    [SerializeField] GameObject Respawner;

    // Update is called once per frame
    override protected void OnDestroy() 
    {
        if (this.gameObject.scene.isLoaded)
        {
            GameObject g = Instantiate(Respawner, transform.position, transform.rotation);
            g.GetComponent<respawner>().whatToRespawn = (GameObject)Resources.Load("Prefabs/Eagle");
            g.GetComponent<respawner>().beginrepsawning();
        }
        base.OnDestroy();
    }
}
