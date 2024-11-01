using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("NetworkManager").GetComponent<NetworkManagerSteam>().MapLoaded();
        Destroy(gameObject);
    }
}
