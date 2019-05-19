using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject player;
    public GameObject door;
    private Animator buttonAnim;
    private Animator doorAnim;

    private bool isPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        buttonAnim = gameObject.GetComponent<Animator>();
        doorAnim = door.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //bool key = Input.GetKeyDown(KeyCode.E);
        bool key = Input.GetButtonDown("Interact");
        bool isWithinRadius = checkDistance(gameObject.transform.position, player.transform.position);

        //Animating(key, isWithinRadius);
        Animating(key, isWithinRadius);
    }


    bool checkDistance(Vector3 buttonPos, Vector3 playerPos)
    {
        return Vector3.Distance(buttonPos, playerPos) < 2;
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
