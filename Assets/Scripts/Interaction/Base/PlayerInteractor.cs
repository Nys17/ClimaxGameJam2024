using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : Subject
{
    [SerializeField][Range(0.1f, 10f)] protected float DistanceToPlayer;
    
    private void Update() {
        if (Vector3.Distance(GameObject.FindWithTag("Player").transform.position, this.gameObject.transform.position) <= DistanceToPlayer){
            if (Input.GetKeyDown("e")){
                FireEvent();
            }
        }
    }
}
