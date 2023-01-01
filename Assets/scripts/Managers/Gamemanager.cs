using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class Gamemanager : MonoBehaviour
{
    public static bool cutscene;
    [SerializeField] Player_Movement player;
    public PlayableDirector dir;
    private void Start()
    {
        cutscene = false;
    }
    public void Respawn()
    {
        StartCoroutine(Resp());
    }
    public void playscene(PlayableAsset asset)
    {
        dir.Play(asset);
        cutscene = true;
        StartCoroutine(ChangeCutsceneAfter((float)asset.duration));
        player.GetComponent<Player_Movement>().ResetVariables();
    }
    //setter for cutscene it also resets player variables meaning speed etc, so there won't be a situation
    //where we start cutscene while speed != 0 and keep that speed through 
    public void SetCutsceneBool(bool b)
    {
        if (b == true) player.ResetVariables();
        cutscene = b;
    }
    public void levelend()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    
    IEnumerator Resp()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }
     public IEnumerator ChangeCutsceneAfter(float x)
    {
        yield return new WaitForSeconds(x);
        cutscene = false;
    }
    public IEnumerator FuncAfterx(float x , Action func)
    {
        yield return new WaitForSeconds(x);
        func();
    }
}
