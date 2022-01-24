using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalConfig : MonoBehaviour
{
    private static GlobalConfig _instance;

    
    public static GlobalConfig Instance { get { return _instance; } }
    public static readonly float MouseYSensitivity = 250f;
    public static readonly float MouseXSensitivity = 250f;
    public static readonly float PlayerMaxSpeed = 30f;
    public static readonly float Gravity = -25f;
    public static readonly float JumpHeight = 5f;
    public static readonly float SandEmitterSpeed = 5f;
    public static readonly float ThrowDistance = 25f;
    public static readonly int GameTime = 60;

    public static readonly int StartTime = 3;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

    }
}
