using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScalar : MonoBehaviour
{
    public float camera_offset = -10;
    public float aspect_ratio = 0.625f;
    public float padding=2f;
    private conwayLife conwayLifeVar;

    // Start is called before the first frame update
    void Start()
    {
        conwayLifeVar = FindObjectOfType<conwayLife>();
        if(conwayLifeVar != null){
            reposition_camera(conwayLifeVar.width - 1, conwayLifeVar.height - 1);
        }
    }

    void reposition_camera(float x, float y){
        Vector3 temp_position = new Vector3(x/2,y/2, camera_offset);
        transform.position = temp_position;
        if(conwayLifeVar.width >= conwayLifeVar.height){ 
            Camera.main.orthographicSize = (conwayLifeVar.width / 2 + padding)/aspect_ratio;
        }else{
            Camera.main.orthographicSize = conwayLifeVar.height / 2 + padding;
        }
    }
}
