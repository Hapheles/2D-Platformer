using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{

    public bool locked;
    public bool KeyPickedUp;

    void Start()
    {
        locked = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key") && KeyPickedUp)
        {
            //locked = false;
            Unlock(other.gameObject);
        }
    }

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Key") && KeyPickedUp)
    //    {
    //        locked = true;
    //    }
    //}

    private void Unlock(GameObject key)
    {
        locked = false;
        KeyPickedUp = false;

        key.GetComponent<KeyManager>().DoorUnlocked();
        Debug.Log("Door Unlocked");
    }
}
