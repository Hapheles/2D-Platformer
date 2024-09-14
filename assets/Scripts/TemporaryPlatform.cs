using UnityEngine;

public class BreakableFloor : MonoBehaviour
{
    public float breakDelay = 0.5f; // Time before the floor breaks
    public float destroyDelay = 1f; // Time before the floor is destroyed

    private bool isBreaking = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isBreaking)
        {
            isBreaking = true;
            StartCoroutine(BreakFloor());
        }
    }

    private System.Collections.IEnumerator BreakFloor()
    {
        yield return new WaitForSeconds(breakDelay);

        yield return new WaitForSeconds(destroyDelay);

        Destroy(gameObject);
    }
}
