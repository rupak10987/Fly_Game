using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plane_controll : MonoBehaviour
{
    public float mousesensitivity = 300.0f;
    public float rot_speed = 1.0f;
    public float f_speed = 30.0f;
    public float accelaration=20.0f;
    public float cam_smooth_time = 0.09f;
    [SerializeField] private float cinematic_amount=0.02f;
    private Vector3 velocity = Vector3.zero;
    Vector3 transition= Vector3.up;
   [SerializeField] private float Camera_offset_f=1f;
    [SerializeField] private float Camera_offset_up = 5f;
    void Start()
    {
    }

    void FixedUpdate()
    {
       
        if(Input.GetKey(KeyCode.LeftShift))
        {
            rot_speed += Time.fixedDeltaTime*2f;
            if (rot_speed > 2f)
                rot_speed = 2f;
        }
        else
        { if (rot_speed > 1.0f)
            {
                rot_speed -= Time.fixedDeltaTime * 5f;
            }
        }
     
        float mouse_cntrl = ((Input.mousePosition.x - 1920 / 2) / 1920) * Time.fixedDeltaTime * mousesensitivity;
        transform.Rotate(Input.GetAxis("Vertical") * rot_speed, mouse_cntrl, -Input.GetAxis("Horizontal")*rot_speed);
        transform.position = transform.position + transform.forward *Time.fixedDeltaTime * f_speed;

        camera_update();
        f_speed -= transform.forward.y * Time.fixedDeltaTime * accelaration;
        speed_on_cmd();
        speed_tune_up();
    }

    void camera_update()
    {
        //camera
        Vector3 cameratarget = transform.position - (Camera_offset_f * transform.forward) + Vector3.up * Camera_offset_up;
        Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, cameratarget, ref velocity, cam_smooth_time);
        transition= Vector3.SmoothDamp(transition, transform.up, ref velocity,cinematic_amount);
        Camera.main.transform.LookAt(transform.position + transform.forward * 30f,transition);
        // i need to store the current transiton value and delver the value for next frame

    }

    void speed_on_cmd()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            f_speed -= Time.fixedDeltaTime * accelaration;
        }
        if(Input.GetKey(KeyCode.LeftShift) && transform.forward.y>0f)
        {
            f_speed += Time.fixedDeltaTime* accelaration;
        }
    }

    void speed_tune_up()
    {
        if (f_speed < 10.0f)
            f_speed = 10.0f;
        if (f_speed > 100.0f)
            f_speed = 100.0f;
    }
}
