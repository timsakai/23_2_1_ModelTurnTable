using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PawnState
{
    Focused,
    Standby,
}
public class PawnController : MonoBehaviour
{
    [SerializeField] float turnSpeed = 1;
    [SerializeField] float mouseControllSpeed = 1;
    [SerializeField] float rotateInert = 0.9f;
    [SerializeField]float noControllTime = 2;
    float noControllTimeCurrent = 0;
    bool isTurnMode = true;
    bool canMouseControll = true;
    [SerializeField] Transform modelOrigin;
    [SerializeField] Transform vcam;
    [SerializeField] Transform frontFixCamPos;
    Vector3 rotVel = new Vector3();
    [SerializeField]PawnState state;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state == PawnState.Focused)
        {
            vcam.gameObject.SetActive(true);
            if (isTurnMode)
            {
                if (canMouseControll && Input.GetMouseButtonDown(0))
                {
                    EnterMouseControll();
                }
                else
                {
                    Turn();
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    noControllTimeCurrent = 0;
                    MouseControll();

                }
                else
                {
                    noControllTimeCurrent += Time.deltaTime;
                }
                if (noControllTimeCurrent >= noControllTime)
                {
                    ExitMouseControll();
                }

            }
            vcam.Translate(Vector3.forward * Input.mouseScrollDelta.y, Space.Self);
        }
        else if (state == PawnState.Standby)
        {
            vcam.gameObject.SetActive(false);
            Turn();

        }

        modelOrigin.Rotate(new Vector3(0, rotVel.y, 0));
        vcam.RotateAround(modelOrigin.position, transform.right, rotVel.x);
        if (vcam.localPosition.z > -0.2)
        {
            vcam.RotateAround(modelOrigin.position, transform.right, -rotVel.x);
            rotVel.x = 0;
        }
    }

    private void FixedUpdate()
    {
        rotVel.y = rotVel.y * rotateInert;
        rotVel.x = rotVel.x * rotateInert;

    }
    void EnterMouseControll()
    {
        isTurnMode = false;
    }
    void ExitMouseControll()
    {
        isTurnMode = true;
    }
    void MouseControll()
    {
        rotVel = new Vector3(-Input.GetAxis("Mouse Y") , -Input.GetAxis("Mouse X")) * Time.deltaTime * mouseControllSpeed;
    }

    void Turn()
    {
        rotVel = new Vector3(0, turnSpeed * Time.deltaTime, 0);
    }

    public void SetFocus(bool yes)
    {
        if (yes)
            state = PawnState.Focused;
        else
            state = PawnState.Standby;
    }

    public Transform GetFrontFixCam()
    {
        return frontFixCamPos;
    }

}
