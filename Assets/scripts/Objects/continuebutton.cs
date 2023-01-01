using UnityEngine;

public class continuebutton : MonoBehaviour
{
    
    [SerializeField]Dialogue_Manager dial;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            dial.Continue();
        }
    }
}
