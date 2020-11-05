using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteInEditMode]
public class SeasonHandeler : MonoBehaviour
{

    float time = 2;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float rot = transform.eulerAngles.z % 360;

        if (Mathf.Round(rot) % 90 == 0 && time > 0) {
            time -= Time.deltaTime;
        } else {
            time = 2;
            transform.eulerAngles = transform.eulerAngles + new Vector3(0,0,speed) * Time.deltaTime;
        }

        if (rot >= 0 && rot < 90) {
            float t = rot / 90;
            Shader.SetGlobalFloat( "_Season1_t", t);
            Shader.SetGlobalFloat( "_Season2_t", 0);
        }
        else if (rot >= 90 && rot < 180) {
            float t = (rot-90) / 90;
            Shader.SetGlobalFloat( "_Season1_t", 1);
            Shader.SetGlobalFloat( "_Season2_t", t);
        }
        else if (rot >= 180 && rot < 270) {
            float t = (rot-180) / 90;
            Shader.SetGlobalFloat( "_Season1_t", 1-t);
            Shader.SetGlobalFloat( "_Season2_t", 1);
        }
        else if (rot >= 270 && rot < 360) {
            float t = (rot-270) / 90;
            Shader.SetGlobalFloat( "_Season1_t", 0);
            Shader.SetGlobalFloat( "_Season2_t", 1-t);
        }


    }
    
}
