using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrail : MonoBehaviour
{
    [SerializeField]
    GameObject trailProto;

    [SerializeField]
    float distance;

    Vector3 lastSpawnedPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var delta = transform.position - lastSpawnedPos;
        var dist = delta.magnitude;

        if (dist >= distance)
        {
            var nuGo = Instantiate(trailProto);

            nuGo.transform.position = lastSpawnedPos = transform.position;
        }
    }
}
