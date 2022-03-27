using UnityEngine;

public class continuebutton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]Dialogue_Manager dial;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            dial.Continue();
        }
    }
}
