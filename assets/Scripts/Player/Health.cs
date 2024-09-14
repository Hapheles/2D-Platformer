using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator animator;
    private bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
    }

    public void Damage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        
        if (currentHealth > 0)
        {
            //player hurt
            animator.SetTrigger("Hurt");
        }
        else
        {
            if(!dead)
            {
                Die();
            }           
        }
    }

    public void Die()
    {
        if (!dead)
        {
            animator.SetTrigger("Die");
            GetComponent <PlayerMovement>().enabled = false;
            dead = true;
        }
    }

    public void Respawn()
    {
        currentHealth = startingHealth;
        dead = false;
        animator.SetTrigger("Respawn");
        GetComponent<PlayerMovement>().enabled = true;
    }

    public bool IsDead()
    {
        return dead;
    }
}

