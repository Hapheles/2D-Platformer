using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float fallDamage = 10f;
    [SerializeField] private float respawnDelay = 1.0f;

    private Vector3 respawnPoint;
    private Health healthComponent;

    void Start()
    {
        respawnPoint = transform.position;
        healthComponent = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathDetector") && !healthComponent.IsDead())
        {
            PlayerFall();
        }
        else if (collision.CompareTag("Checkpoint"))
        {
            UpdateCheckpoint(collision.transform.position);
        }
    }

    private void PlayerFall()
    {
        healthComponent.Damage(fallDamage);

        if (healthComponent.IsDead())
        {
            Invoke("Respawn", respawnDelay);
        }

    }

    private void Respawn()
    {
        transform.position = respawnPoint;
        healthComponent.Respawn();
    }

    private void UpdateCheckpoint(Vector3 newCheckpoint)
    {
        respawnPoint = newCheckpoint;
    }
}
