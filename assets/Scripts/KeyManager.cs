using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject door;

    public bool isPickedUp;
    private Vector2 vel;
    public float smoothTime;

    void Start()
    {
        
    }

    void Update()
    {
        if (isPickedUp)
        {
            Vector3 offset = new Vector3(0, 1.5f, 0);
            transform.position = Vector2.SmoothDamp(transform.position, player.transform.position + offset, ref vel, smoothTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isPickedUp)
        {
            isPickedUp = true;
            door.GetComponent<Door>().KeyPickedUp = true;
        }
    }

    public void DoorUnlocked()
    {
        gameObject.SetActive(false);
    }
}
