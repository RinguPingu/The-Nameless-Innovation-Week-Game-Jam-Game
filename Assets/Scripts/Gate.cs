using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    [SerializeField]
    Image progressBar;

    [SerializeField]
    float duration = 1.5f;

    [System.NonSerialized]
    public float progress;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float Open(float amount)
    {
        progress = Mathf.Clamp01(progress + amount / duration);

        if (progress >= 1f)
        {
            Destroy(progressBar);
            Destroy(transform.parent.gameObject);
        }

        return progress;
    }

    // Update is called once per frame
    void Update()
    {
        progressBar.fillAmount = 1f - progress;
    }
}
