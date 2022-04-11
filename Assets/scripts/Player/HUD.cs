using UnityEngine;

public class HUD : MonoBehaviour
{
    
    [SerializeField]GameObject[] Hearts= new GameObject[3];


    public void addlife(int currlife)
    {
        if (currlife == 3) return;
        currlife++;
        Hearts[currlife-1].SetActive(true);
        
    }
    public void removelife(int currlife)
    {
        Hearts[currlife-1].SetActive(false);
        currlife--;
    }
    public void deletealllifes()
    {
        foreach (var heart in Hearts)
        {
            heart.SetActive(false);
        }
    }
}
