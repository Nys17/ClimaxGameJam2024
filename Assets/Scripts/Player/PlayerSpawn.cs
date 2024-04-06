using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject pSpawn;


    private void Start()
    {
        
        Instantiate(pSpawn,this.transform.position, this.transform.rotation);
    }
}
