using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField]
    GameObject projectileProto;

    [SerializeField]
    [Range(0f, 25f)]
    float maxDistance = 25f;

    [SerializeField]
    [Range(0f, 25f)]
    float aimDelay = 1f;

    [SerializeField]
    [Range(0f, 25f)]
    float shootDelay = 1f;

    [SerializeField]
    [Range(0f, 15f)]
    float projectileSpeed = 5f;

    float lastTimeActed;


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

        if (true)
        {
            var layerMask = ~(1 << gameObject.layer);

            var raycastHit = Physics2D.Raycast(transform.position, dir, maxDistance, layerMask);

            if (GetComponent<EnemyBase>().canMove)
            {
                if (raycastHit.collider && raycastHit.collider.gameObject == target && (Time.time - lastTimeActed) > shootDelay)
                {
                    lastTimeActed = Time.time;
                    GetComponent<EnemyBase>().canMove = false;
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                    GetComponent<EnemyBase>().Animator.SetTrigger("attack");
                }
            }
            else
            {
                if (raycastHit.collider && raycastHit.collider.gameObject == target && (Time.time - lastTimeActed) > aimDelay)
                {
                    lastTimeActed = Time.time;
                    Shoot(dir);
                    GetComponent<EnemyBase>().canMove = true;
                }
            }


        }
    }

    void Shoot(Vector3 dir)
    {
        var proj = Instantiate(projectileProto);
        proj.transform.position = transform.position + new Vector3(dir.x * .85f, dir.y * .45f);
        proj.GetComponent<Rigidbody2D>().velocity = dir * projectileSpeed;
    }
}
