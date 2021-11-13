using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plane_controll : MonoBehaviour
{
    public Rigidbody rb;
    public float mousesensitivity = 300f;
    public float rot_speed = 1.0f;
    public float f_speed = 30.0f;
    public float accelaration=20.0f;
    public float cam_smooth_time = 0.09f;
    private Vector3 velocity = Vector3.zero;
    Vector3 transition= Vector3.up;
    [SerializeField] private float cinematic_amount = 0.02f;
    [SerializeField] private float Camera_offset_f=1f;
    [SerializeField] private float Camera_offset_up = 5f;
    private float time_going_down=0f;
    private float time_going_up = 0f;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Gravity_sim();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rot_speed += Time.fixedDeltaTime*2f;
            if (rot_speed > 2.01f)
                rot_speed = 2.01f;
        }
        else
        { if (rot_speed > 1.5f)
            {
                rot_speed -= Time.fixedDeltaTime * 5f;
            }
        }
        

        float mouse_cntrl =((Input.mousePosition.x - 1920/ 2) / 1920) * Time.fixedDeltaTime * mousesensitivity;
        float pitch_ctrl= Input.GetAxis("Vertical") * rot_speed;
        float roll_ctrl = -Input.GetAxis("Horizontal") * rot_speed;
        transform.Rotate(pitch_ctrl, mouse_cntrl, roll_ctrl);
        transform.position = transform.position + transform.forward *Time.fixedDeltaTime * f_speed;

        camera_update();
        f_speed -= transform.forward.y * Time.fixedDeltaTime * accelaration;
        speed_on_cmd();
        speed_tune_up();
        Vector3 rott = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);

    }

    void camera_update()
    {
        //camera
        if(Input.GetKey(KeyCode.C))
        {
            Vector3 cameratarget1 = transform.position + (20 * transform.forward) + Vector3.up;
            Camera.main.transform.position = cameratarget1;
            Camera.main.transform.LookAt(transform.position,transform.up);
        }
        else
        {
            Vector3 cameratarget = transform.position - (Camera_offset_f * transform.forward) + Vector3.up * Camera_offset_up;
            Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, cameratarget, ref velocity, cam_smooth_time);
            transition = Vector3.SmoothDamp(transition, transform.up, ref velocity, cinematic_amount);
            Camera.main.transform.LookAt(transform.position + transform.forward * 30f, transition);
            // i need to store the current transiton value and delver the value for next frame
        }
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
        if (f_speed < 20.0f)//30
            f_speed = 20.0f;
        if (f_speed > 80.0f)//80
            f_speed = 80.0f;
    }
    void Gravity_sim()
    {

        float displace = 0; ;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (time_going_down <= 0)
            {
                time_going_up += Time.fixedDeltaTime;
                // making changes
                if (time_going_up > 0.2f)
                    time_going_up = 0.2f;
                //done
                displace = 0.01f * time_going_up;
                transform.position = new Vector3(transform.position.x, (transform.position.y + displace), transform.position.z);
            }
            else
            {
                time_going_down -= Time.fixedDeltaTime;
                displace = 0.01f * time_going_down;
                transform.position = new Vector3(transform.position.x, (transform.position.y - displace), transform.position.z);
            }

        }
        else
        {
            if (time_going_up <= 0)
            {
                time_going_down += Time.fixedDeltaTime;
                displace = 0.01f * time_going_down;
                transform.position = new Vector3(transform.position.x, (transform.position.y - displace), transform.position.z);

            }
            else
            {
                time_going_up -= Time.fixedDeltaTime;
                displace = 0.01f * time_going_up;
                transform.position = new Vector3(transform.position.x, (transform.position.y + displace), transform.position.z);
            }


        }
    }
   

}
