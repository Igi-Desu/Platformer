using UnityEngine;
using UnityEngine.Playables;

public class trapdiamond : MonoBehaviour
{
    [SerializeField] PlayableAsset cutscene;
    //when player collides with this diamond play end cutscene
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Gamemanager g = FindObjectOfType<Gamemanager>();
        g.playscene(cutscene);
        StartCoroutine(g.FuncAfterx((float)cutscene.duration, g.levelend));
    }
}
