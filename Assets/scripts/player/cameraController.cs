using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.Cinemachine;

public class cameraController : NetworkBehaviour
{
    
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject playerMesh;
    public override void OnStartClient()
    {
        //detects if is local player then enables all camera functionality for that player
        if(isOwned) 
        {
            cam.GetComponent<AudioListener>().enabled = true;
            cam.GetComponent<Camera>().enabled = true;
            cam.GetComponent<CinemachineBasicMultiChannelPerlin>().enabled = true;
            cam.GetComponent<CinemachineInputAxisController>().enabled = true;
            cam.GetComponent<CinemachinePanTilt>().enabled = true;
            cam.GetComponent<CinemachineHardLockToTarget>().enabled = true;
            cam.GetComponent<CinemachineCamera>().Priority = 1;

            // playerMesh.SetActive(false);
        }
        else
        {
            cam.GetComponent<CinemachineCamera>().Priority = 0;
        }
        
    }
}
