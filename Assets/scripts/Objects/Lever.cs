using UnityEngine;
using UnityEngine.Playables;

public class Lever : MonoBehaviour
{

    [SerializeField]GameObject Attachedblock;
    [SerializeField]GameObject Exclamation;
    [SerializeField] GameObject Player;
    [SerializeField] Sprite lever2;
    [SerializeField] Gamemanager gm;
    [SerializeField] PlayableAsset cutscene;
    bool isplayernear = false;
    void Start()
    {
        Exclamation.SetActive(false);
    }
    private void Update()
    {
        if (isplayernear)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                gm.playscene(cutscene);
                Destroy(Attachedblock, 1.5f);
                GetComponent<SpriteRenderer>().sprite = lever2;
                Exclamation.SetActive(false);
                this.enabled = false;
            }
        }
    }
    void FixedUpdate()
    {
        if (Player == null) return;
        if (Vector2.Distance(Player.transform.position, transform.position) < 1)
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
}
