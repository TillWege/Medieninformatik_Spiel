using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MausController : MonoBehaviour
{

    public Transform Player;
    private float xRotation = 0f;
    void Start()
    {
        
    }

    void Update()
    {
        if(GameControllerScript._instance.GetGameStarted() && !GameControllerScript._instance.MouseUnlocked){

            float Mouse_X = Input.GetAxis("Mouse X") * GlobalConfig.MouseXSensitivity * Time.deltaTime;
            float Mouse_Y = Input.GetAxis("Mouse Y") * GlobalConfig.MouseYSensitivity * Time.deltaTime;

            xRotation -= Mouse_Y;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            this.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            Player.Rotate(Vector3.up * Mouse_X);
        }

        
    }
}
