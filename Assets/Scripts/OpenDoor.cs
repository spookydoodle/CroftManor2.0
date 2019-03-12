using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator buttonAnim;
    public Animator doorAnim;
    public GameObject door;

    private bool isPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        Animator buttonAnim = gameObject.GetComponent<Animator>();
        Animator doorAnim = door.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool key = Input.GetKeyDown(KeyCode.E);
        
        Animating(key);
        Debug.Log(isPressed);
    }

    // Set parameters used in conditions of transitions in Animator component
    void Animating(bool key)
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
