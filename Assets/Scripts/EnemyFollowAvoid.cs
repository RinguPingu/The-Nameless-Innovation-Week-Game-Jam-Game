using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowAvoid : MonoBehaviour
{
    [SerializeField]
    bool followIfVisible = true;

    [SerializeField]
    float forceMagnitude = 1f;

    [SerializeField]
    [Tooltip("0 = follow, 180 = avoid")]
    float followAngle = 0f;

    [SerializeField]
    [Tooltip("0 = follow, 180 = avoid")]
    float brakeForce = 10f;

    [SerializeField]
    [Range(0f, 25f)]
    float minDistance = 0f;

    [SerializeField]
    [Range(0f, 25f)]
    float maxDistance = 25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        var mouseScreen = Input.mousePosition;
        var mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0f;

        var delta = mouseWorld - transform.position;
        var dist = delta.magnitude;
        var dir = delta / dist;

        bool follow = true;

        if (followIfVisible)
        {
            var layerMask = ~(1 << LayerMask.NameToLayer("Enemy"));

            var raycastHit = Physics2D.Raycast(transform.position, dir, dist, layerMask);

            var perp = Vector3.Cross(dir, Vector3.forward) * .2f;
            var raycastHit2 = Physics2D.Raycast(transform.position, (mouseWorld + perp - transform.position).normalized, dist, layerMask);
            var raycastHit3 = Physics2D.Raycast(transform.position, (mouseWorld - perp - transform.position).normalized, dist, layerMask);

            follow = !raycastHit.collider || !raycastHit2.collider || !raycastHit3.collider;

            Debug.DrawLine(transform.position, mouseWorld);
            Debug.DrawLine(transform.position, mouseWorld + perp);
            Debug.DrawLine(transform.position, mouseWorld - perp);
        }

        follow &= dist < maxDistance;
        follow &= dist > minDistance;

        var rb = GetComponent<Rigidbody2D>();
        if (follow)
        {
            Debug.DrawLine(transform.position, mouseWorld);

            dir = Quaternion.AngleAxis(followAngle, Vector3.forward) * dir;

            rb.AddForce(dir * forceMagnitude * Time.deltaTime);
            //GetComponent<EnemyBase>().targetDir = dir;
        }
        else
        {
            //Debug.DrawLine(transform.position, raycastHit.point, Color.grey);
            rb.AddForce(-rb.velocity * brakeForce * Time.deltaTime);
        }
    }
}
