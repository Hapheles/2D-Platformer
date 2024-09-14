using System;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("MovementParameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingleft;

    [Header("EnemyAnimation")]
    [SerializeField] private Animator anim;

    [Header("Idle Time")]
    [SerializeField] private float idleDuration;
    private float idleTimer;


    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void Update()
    {
        if (movingleft)
        {
            if (enemy.position.x <= pointB.position.x)
            {
                MoveInDirection(1);
            }
                
            else
            {
                Flip();
            }
        }
        else
        {
            if (enemy.position.x >= pointA.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        anim.SetBool("isWalking", false);
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
        {
            movingleft = !movingleft;
        }
    }

    private void MoveInDirection(int direction)
    {
        idleTimer = 0;

        anim.SetBool("isWalking", true);

        //Makes enemy face a specific direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);

        //Makes enemy move in the specific direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed,
            enemy.position.y, enemy.position.z);
    }
}
