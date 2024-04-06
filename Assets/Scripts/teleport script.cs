using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportscript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Teleport;
    public GameObject player;
    private bool canTeleport;


    private void Start()
    {
        canTeleport = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&& canTeleport == true)
        {
            player.transform.position = Teleport.transform.position;
            canTeleport = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTeleport = true;
        }
        else
        {
            return;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTeleport = false;
        }
        else
        {
            return;
        }
    }
}

