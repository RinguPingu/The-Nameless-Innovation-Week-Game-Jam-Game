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

        var raycastHit = Physics2D.Raycast(transform.position, dir, dist, ~(1 << LayerMask.NameToLayer("Enemy")));

        var perp = Vector3.Cross(dir, Vector3.forward) * .2f;
        var raycastHit2 = Physics2D.Raycast(transform.position, (mouseWorld + perp - transform.position).normalized, dist, ~(1 << LayerMask.NameToLayer("Enemy")));
        var raycastHit3 = Physics2D.Raycast(transform.position, (mouseWorld - perp - transform.position).normalized, dist, ~(1 << LayerMask.NameToLayer("Enemy")));

        bool visible = !raycastHit.collider || !raycastHit2.collider || !raycastHit3.collider;


        Debug.DrawLine(transform.position, mouseWorld);
        Debug.DrawLine(transform.position, mouseWorld + perp);
        Debug.DrawLine(transform.position, mouseWorld - perp);

        if (visible)
        {
            Debug.DrawLine(transform.position, mouseWorld);

            dir = Quaternion.AngleAxis(followAngle, Vector3.forward) * dir;

            var rb = GetComponent<Rigidbody2D>();
            rb.AddForce(dir * forceMagnitude * Time.deltaTime);
            //GetComponent<EnemyBase>().targetDir = dir;
        }
        else
        {
            //Debug.DrawLine(transform.position, raycastHit.point, Color.grey);
        }
    }
}
