using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PressEvent
{
    public string axisName;
    public UnityEvent unityEvent;
    public float preValue { get; set; }
}
public class AxisPressEvent : MonoBehaviour
{
    [SerializeField]List<PressEvent> pressEvents;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in pressEvents)
        {
            if (Input.GetAxis(item.axisName) >= 1 && item.preValue < 1)
            {
                item.unityEvent.Invoke();
                //Debug.Log(item.axisName.ToString() + "is pressed");
            }
            item.preValue = Input.GetAxis(item.axisName);
        }
    }
}
