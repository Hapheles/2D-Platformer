using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject player;
    public float yOffset = 1f;
    public float FollowSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}
