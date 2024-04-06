using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// / !!!! ADD tag "Player" to the player character !!!!!!!!
/// </summary>
public class CollectibleKey : MonoBehaviour
{
    public GM GMRef;
    public KeyData dataRef;
    void Start()
    {
        GMRef = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){ 
            GMRef.CollectedKeys.Add(dataRef.KeyID);

            Destroy(this.gameObject);
        }
    }


}
