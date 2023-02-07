using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField] float easeInTime = 1f;
    [SerializeField] float easeOutTime = 1f;
    bool isStarted = false;
    float time = 0;
    float weight = 0;
    float progress = 0;
    [SerializeField] float scale = 0.1f;
    [SerializeField] float speed = 1f;
    Vector3 orgpos;
    // Start is called before the first frame update
    void Start()
    {
        orgpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted)
        {
            if (progress < 1)
            {
                progress += Time.deltaTime / easeInTime;
            }
            weight = Mathf.Lerp(0,1, progress);
        }
        else
        {
            if (progress < 1)
            {
                progress += Time.deltaTime / easeInTime;
            }
            weight = Mathf.Lerp(1, 0, progress);
        }
        transform.position = orgpos + weight * new Vector3(Mathf.Sin(Time.time * speed * 0.7f) * scale,
                                            Mathf.Sin(Time.time * speed * 1.3f) * scale,
                                            Mathf.Sin(Time.time * speed * 1.0f) * scale);
    }
    [ContextMenu("Shake")]
    public void ShakeStart()
    {
        isStarted = true;
        time = 0;
        progress = 0;
    }

    [ContextMenu("ShakeEnd")]
    public void ShakeEnd()
    {
        isStarted=false;
        time = 0;
        progress = 0;
    }
}
