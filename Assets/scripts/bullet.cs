using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;//initialized from  owner
    public float gravity=20.8f;
    public float elapsed_time = 0f;
    private float vZnot;
    private float vYnot;
    private float vXnot;
    public float ynot;//initialized from  owner
    public float znot;//initialized from  owner
    public float xnot;//initialized from  owner
    public bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        vZnot = speed * transform.forward.z;
        vXnot = speed * transform.forward.x;
        vYnot = speed * transform.forward.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!done)
        {
            elapsed_time += Time.fixedDeltaTime;
            transform.position = Calculate_point_on_parabola(elapsed_time);
            Vector3 next_point = Calculate_point_on_parabola(elapsed_time + Time.fixedDeltaTime);
            do_ray(next_point);
        }
    }
    void do_ray(Vector3 point2)
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, (point2-transform.position).normalized, out hit, Vector3.Magnitude(transform.position-point2)))
        {
           if(gameObject.name=="bullets(Clone)")
           
            {
                
                GameObject find = GameObject.Find("bullet_impact_particle");
                GameObject effect = GameObject.Instantiate(find);
                effect.transform.position = hit.point;
                effect.transform.forward = hit.normal;
                effect.transform.localScale = new Vector3(1f, 1f, 1f);
                effect.transform.localScale = Vector3.one * 0.1f;
                done = true;
                Destroy(effect, 2f);
            }
            else
            {
                GameObject find = GameObject.Find("bomb_impact_particle");
                GameObject effect = GameObject.Instantiate(find);
                effect.transform.position = hit.point;
                effect.transform.forward = hit.normal;
                effect.transform.localScale = new Vector3(1f, 1f, 1f);
                effect.transform.localScale = Vector3.one *3f;
                done = true;
                Destroy(effect, 4f);

            }
            
        }
       
    }
    Vector3 Calculate_point_on_parabola(float e_time)
    {
        float y1 = ynot + (vYnot * e_time) - (0.5f *gravity* Mathf.Pow(e_time, 2f)); //new position
        float z1 = znot + (vZnot * e_time);
        float x1 = xnot + (vXnot * e_time);
        return new Vector3(x1, y1, z1);
    }
}

