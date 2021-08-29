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

    [SerializeField]
    [Tooltip("Tags to follow by order of priority")]
    string[] tagsToFollow;

    [SerializeField]
    [Tooltip("Tags to follow by order of priority")]
    float randomTargetOffsetDistance = 0f;

    Vector3 targetPos;
    Vector3 randomTargetOffset;
    float lastUpdatedRandomTargetOffset;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<EnemyBase>().canMove)
            return;

        if (Time.time - lastUpdatedRandomTargetOffset > 2f)
        {
            randomTargetOffset = Random.insideUnitCircle * randomTargetOffsetDistance;
            lastUpdatedRandomTargetOffset = Time.time;
        }



        var rb = GetComponent<Rigidbody2D>();

        foreach (var tag in tagsToFollow)
        {
            var target = GameObject.FindGameObjectWithTag(tag);

            var newTargetPos = target.transform.position + randomTargetOffset;
            newTargetPos.z = 0f;

            var mouseScreen = Input.mousePosition;
            var mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
            mouseWorld.z = 0f;

            var delta = newTargetPos - transform.position;
            var dist = delta.magnitude;
            var dir = delta / dist;

            bool follow = true;

            if (followIfVisible)
            {
                var layerMask = ~(1 << gameObject.layer);

                var raycastHit = Physics2D.Raycast(transform.position, dir, maxDistance, layerMask);

                var perp = Vector3.Cross(dir, Vector3.forward) * .2f;
                var raycastHit2 = Physics2D.Raycast(transform.position, (newTargetPos + perp - transform.position).normalized, maxDistance, layerMask);
                var raycastHit3 = Physics2D.Raycast(transform.position, (newTargetPos - perp - transform.position).normalized, maxDistance, layerMask);

                follow = raycastHit.transform == target.transform
                    || raycastHit2.transform == target.transform
                    || raycastHit3.transform == target.transform;

                Debug.DrawLine(transform.position, newTargetPos);
                Debug.DrawLine(transform.position, newTargetPos + perp);
                Debug.DrawLine(transform.position, newTargetPos - perp);
            }

            //follow &= dist < maxDistance;
            //follow &= dist > minDistance;

            if (follow)
            {
                targetPos = newTargetPos;
                //GetComponent<EnemyBase>().targetDir = dir;
                break;
            }
            else
            {
                //Debug.DrawLine(transform.position, raycastHit.point, Color.grey);
                //rb.AddForce(-rb.velocity * brakeForce * Time.deltaTime);
            }

            Debug.DrawLine(transform.position, newTargetPos);
        }

        if (targetPos == default(Vector3))
            return;

        var dir2 = (targetPos - transform.position).normalized;
        dir2 = Quaternion.AngleAxis(followAngle, Vector3.forward) * dir2;

        rb.AddForce(dir2 * forceMagnitude * Time.deltaTime);
    }
}
