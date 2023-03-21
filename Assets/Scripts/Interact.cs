using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{   
    public Transform interactCheck;
    public float interactDistance = 0.4f;
    public LayerMask interactableMask;
    public AudioSource Source;
    public AudioClip doorCloseSound;
    public AudioClip doorOpenSound;

    
    public bool inRange;


    // Update is called once per frame
    void Update()
    {
        inRange = Physics.CheckSphere(interactCheck.position, interactDistance, interactableMask);

        

        Collider[] objects = Physics.OverlapSphere(interactCheck.position, interactDistance, interactableMask);
        foreach (var obj in objects)
        {   
            if(obj.tag == "door")
            {   
                
                Animator animator = obj.GetComponent<Animator>();
                
                if(Input.GetKeyDown(KeyCode.E))
                {   
                    AnimatorStateInfo currentAnim = animator.GetCurrentAnimatorStateInfo(0);
                    if(currentAnim.IsName("DoorClose")){
                        Debug.Log("Open");
                        animator.Play("DoorOpen");
                        obj.transform.Rotate(0, 0, -90);
                        Source.PlayOneShot(doorOpenSound);
                    } else if(currentAnim.IsName("DoorOpen")){
                        Debug.Log("Close");
                        animator.Play("DoorClose");
                        obj.transform.Rotate(0, 0, 90);
                        Source.volume = 0.1f;
                        Source.PlayOneShot(doorCloseSound);
                        Source.volume = 1f;
                    }
                    
                }
            }
            
        }

        
    }
}
