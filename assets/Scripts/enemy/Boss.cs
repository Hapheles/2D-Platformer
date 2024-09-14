using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectilePos;

    [SerializeField] private int maxHealth = 10;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Boss initialized with " + currentHealth + " health");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        Debug.Log("Boss took " + damage + " damage. Current health: " + currentHealth);

        if (IsDead())
        {
            Die();
        }
    }

    public void Attack()
    {
        Debug.Log("Boss is attacking");
        GameObject projectileObj = Instantiate(projectilePrefab, projectilePos.position, Quaternion.identity);
        Projectile projectile = projectileObj.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.SetDirection(transform.localScale.x);
            Debug.Log("Projectile launched");
        }
        else
        {
            Debug.LogError("Projectile component not found on instantiated object");
        }
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    private void Die()
    {
        Debug.Log("Boss defeated");
        gameObject.SetActive(false);
    }
}