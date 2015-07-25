using UnityEngine;
using System.Collections;

public class CamManager : MonoBehaviour
{
    bool isSecondary;
    public GameObject camera;
    public GameObject camera2;

    void Start()
    {
        isSecondary = false;
    }

    void Update()
    {
  
        if(Input.GetKeyUp("c"))
        {
            isSecondary = !isSecondary;
        }                
        //GameObject camera = transform.Find("CameraMarketSquare").gameObject;
        camera.SetActive(isSecondary);
        //GameObject camera2 = transform.Find("MainCamera").gameObject;
        camera2.SetActive(!isSecondary);
    }


}

/*
public class camEvents : MonoBehaviour
{
    public Camera[] cams;  // 1 

    public void moveCameraOne()
    { // 2
        cams[0].enabled = false;
        cams[1].enabled = true;
        cams[2].enabled = false;
    }

    public void moveCameraTwo()
    {  // 3
        cams[0].enabled = false;
        cams[1].enabled = false;
        cams[2].enabled = true;
    }

    public void moveCameraMain()
    {  // 4
        cams[0].enabled = true;
        cams[1].enabled = false;
        cams[2].enabled = false;
    }
}
*/