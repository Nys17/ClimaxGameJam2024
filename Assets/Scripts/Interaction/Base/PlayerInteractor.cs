using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : Subject
{
    [SerializeField][Range(0.1f, 10f)] protected float DistanceToPlayer;
    
    private void Update() {
        if (Input.GetKeyDown("e")){
            if (Vector3.Distance(GameObject.FindWithTag("Player").transform.position, this.gameObject.transform.position) <= DistanceToPlayer){
                FireEvent();
            }
        }
    }
}
