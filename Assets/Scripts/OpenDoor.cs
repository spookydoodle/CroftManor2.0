using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private Animator buttonAnim;
    private Animator doorAnim;
    public GameObject button;
    public GameObject door;
    public GameObject player;

    private bool isPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject button = gameObject.GetComponent<GameObject>();
        Animator buttonAnim = gameObject.GetComponent<Animator>();
        Animator doorAnim = door.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool key = Input.GetKeyDown(KeyCode.E);
        bool isWithinRadius = checkDistance(button, player);
        Debug.Log(isWithinRadius);
        
        Animating(key, isWithinRadius);
    }


    bool checkDistance(GameObject button, GameObject player)
    {
        return Vector3.Distance(button.transform.position, player.transform.position) < 1;
    }


    // Set parameters used in conditions of transitions in Animator component
    void Animating(bool key, bool isWithinRadius)
    {
        if (isWithinRadius)
        {
            if (key && !isPressed)
            {
                buttonAnim.SetTrigger("Press");
                doorAnim.SetTrigger("Open");
                isPressed = true;
            }
            else if (key && isPressed)
            {
                buttonAnim.SetTrigger("Release");
                doorAnim.SetTrigger("Close");
                isPressed = false;
            }
        }
    }
}
