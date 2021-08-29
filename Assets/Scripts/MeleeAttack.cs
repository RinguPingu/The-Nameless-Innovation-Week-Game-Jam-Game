using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField]
    float attackDelay = 2f;
    [SerializeField]
    float attackDuration = .8f;

    float lastTimeAttacked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var target = GameObject.FindGameObjectWithTag("Player");

        var delta = target.transform.position - transform.position;
        var dist = delta.magnitude;
        var dir = delta / dist;

        if (GetComponent<EnemyBase>().canMove)
        {
            if (Mathf.Abs(delta.x) < 1.4f && Mathf.Abs(delta.y) < .6f && Time.time - lastTimeAttacked > attackDelay)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<EnemyBase>().canMove = false;
                GetComponent<EnemyBase>().Animator.SetTrigger("attack");

                lastTimeAttacked = Time.time;
            }
        }
        else
        {
            if (Time.time - lastTimeAttacked > attackDuration)
            {
                GetComponent<EnemyBase>().canMove = true;
            }
        }

    }
}
