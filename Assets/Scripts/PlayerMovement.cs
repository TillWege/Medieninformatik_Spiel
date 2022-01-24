using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Controller;
    public LayerMask GroundMask, WaterMask;
    public Transform OnGroundCheck;
    public AudioSource Wasser;

    private Vector3 Velocity;    
    private Vector2 GroundVelocity;
    private float GroundCheckSphereDiameter = 0.4f;

    private bool OnGround;
    public Light Taschenlampe;

    void LateUpdate()
    {   
        if(!GameControllerScript._instance.GetGameStarted())
            return;

        if (CheckWasserGameOver())
            return;

        UpdateGravity();
        UpdateMovement();
        if(Input.GetKeyDown(KeyCode.F))
        {
            Taschenlampe.gameObject.SetActive(!Taschenlampe.gameObject.activeSelf);
        }
    }

    bool CheckWasserGameOver()
    {
        bool Result = Physics.CheckSphere(OnGroundCheck.position, GroundCheckSphereDiameter, WaterMask);

        if(Result)
        {
            Wasser.Play();
            GameControllerScript._instance.GameOver(false);
        }
            
        return Result;
    }

    void UpdateGravity()
    {
            OnGround = Physics.CheckSphere(OnGroundCheck.position, GroundCheckSphereDiameter, GroundMask);

            if(OnGround && Velocity.y < 0)
            {
                Velocity.y = -2f;
            }

            if(Input.GetButtonDown("Jump") && OnGround)
            {
                Velocity.y = Mathf.Sqrt(GlobalConfig.JumpHeight * -2f * GlobalConfig.Gravity);
            }

            Velocity.y += GlobalConfig.Gravity * Time.deltaTime;
            Controller.Move(Velocity * Time.deltaTime);
    }

    void UpdateMovement()
    {
        float Input_X = Input.GetAxis("Horizontal");
        float Input_Z = Input.GetAxis("Vertical");
        Vector3 move = (transform.right * Input_X) + (transform.forward * Input_Z);
        Controller.Move(move * Time.deltaTime * GlobalConfig.PlayerMaxSpeed);
    }
    public void resetPos(Vector3 pPos, Quaternion pRot)
    {
        transform.position = pPos;
        transform.rotation = pRot;
    }
}
