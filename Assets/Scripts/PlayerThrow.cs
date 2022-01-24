using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    public ParticleSystem Sand;
    public LayerMask HäuserLayer;
    public AudioSource SandSound;
    void Start()
    {
        
    }

    void Update()
    {
        if (GameControllerScript._instance.GetGameStarted() && Input.GetMouseButtonDown(0))
            fire();
    }

    void fire()
    {
        Sand.Clear();
        Sand.Play();
        SandSound.Play(); 

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, GlobalConfig.ThrowDistance, HäuserLayer))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            GameControllerScript._instance.HouseHit(hit.collider);
        }
    }
}
