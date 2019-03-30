using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenEntranceDoor : MonoBehaviour
{
    public Animator buttonAnim;
    public GameObject door;

    private bool isPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        Animator buttonAnim = gameObject.GetComponent<Animator>();
        GameObject door = gameObject.GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        bool key = Input.GetKeyDown(KeyCode.E);

        Animating(key);
    }

    // Set parameters used in conditions of transitions in Animator component
    void Animating(bool key)
    {

        if (key && !isPressed)
        {
            buttonAnim.SetTrigger("Press");
            
            door.transform.Rotate(0, -90, 0, Space.Self);

            isPressed = true;
        }
        else if (key && isPressed)
        {
            buttonAnim.SetTrigger("Release");

            door.transform.Rotate(0, 90, 0, Space.Self);

            isPressed = false;
        }
    }
}
