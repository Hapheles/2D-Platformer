using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private int damage = 1;
    private float direction = 1f;
    private bool hit = false;

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hit) return;

        Debug.Log("Projectile collided with: " + collision.gameObject.name);

        Health playerHealth = collision.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.Damage(damage);
            Debug.Log("Player hit by projectile. Damage dealt: " + damage);
            hit = true;
            Destroy(gameObject);
        }
    }

    public void SetDirection(float _direction)
    {
        direction = _direction;
        Debug.Log("Projectile direction set to: " + direction);
        if (direction < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
