using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Interact : MonoBehaviour
{   
    public Transform interactCheck;
    public float interactDistance = 7.14f;
    public LayerMask interactableMask;
    public AudioSource Source;
    public AudioClip doorCloseSound;
    public AudioClip doorOpenSound;

    
    public bool inRange;

    private void Start() {
        DOTween.Init();
    }
    // Update is called once per frame
    void Update()
    {
        inRange = Physics.CheckSphere(interactCheck.position, interactDistance, interactableMask);

        

        Collider[] objects = Physics.OverlapSphere(interactCheck.position, interactDistance, interactableMask);
        foreach (var obj in objects)
        {   
            if(obj.tag == "door -z facing")
            {   
                
                Animator animator = obj.GetComponent<Animator>();
                
                if(Input.GetKeyDown(KeyCode.E))
                {   
                    Vector3 Openangle = new Vector3(-90f, 0, -90f);
                    Vector3 Closeangle = new Vector3(-90f, 0, 0);
                    AnimatorStateInfo currentAnim = animator.GetCurrentAnimatorStateInfo(0);
                    if(currentAnim.IsName("DoorClose") || currentAnim.IsName("Idle")){
                        obj.transform.DORotate(Openangle, 1f);
                        animator.Play("DoorOpen");
                        // Source.PlayOneShot(doorOpenSound);
                    } else if(currentAnim.IsName("DoorOpen")){
                        Debug.Log("Close");
                        obj.transform.DORotate(Closeangle, 1f);
                        animator.Play("DoorClose");
                        // Source.PlayOneShot(doorCloseSound);
                    } 
                }
            } else if(obj.tag == "door -x facing")
            {   
                
                Animator animator = obj.GetComponent<Animator>();

                
                if(Input.GetKeyDown(KeyCode.E))
                {   
                    
                    Vector3 Openangle = new Vector3(-90f, 0, 0f);
                    Vector3 Closeangle = new Vector3(-90f, 0, 90f);
                    AnimatorStateInfo currentAnim = animator.GetCurrentAnimatorStateInfo(0);

                    if(currentAnim.IsName("DoorClose") || currentAnim.IsName("Idle")){
                        obj.transform.DORotate(Openangle, 1f);
                        animator.Play("DoorOpen");
                        // Source.PlayOneShot(doorOpenSound);
                    } else if(currentAnim.IsName("DoorOpen")){
                        Debug.Log("Close");
                        obj.transform.DORotate(Closeangle, 1f);
                        animator.Play("DoorClose");
                        // Source.PlayOneShot(doorCloseSound);
                    }
                }
            } else if(obj.tag == "door x facing")
            {   
                
                Animator animator = obj.GetComponent<Animator>();
                
                if(Input.GetKeyDown(KeyCode.E))
                {   
                    Vector3 Openangle = new Vector3(-90f, 0, -270f);
                    Vector3 Closeangle = new Vector3(-90f, 0, -180f);
                    AnimatorStateInfo currentAnim = animator.GetCurrentAnimatorStateInfo(0);

                    if(currentAnim.IsName("DoorClose") || currentAnim.IsName("Idle")){
                        obj.transform.DORotate(Openangle, 1f);
                        animator.Play("DoorOpen");
                        // Source.PlayOneShot(doorOpenSound);
                    } else if(currentAnim.IsName("DoorOpen")){
                        Debug.Log("Close");
                        obj.transform.DORotate(Closeangle, 1f);
                        animator.Play("DoorClose");
                        // Source.PlayOneShot(doorCloseSound);
                    } 
                }
            } else if(obj.tag == "door z facing")
            {   
                
                Animator animator = obj.GetComponent<Animator>();
                
                if(Input.GetKeyDown(KeyCode.E))
                {   
                    Vector3 Openangle = new Vector3(-90f, 0, 0f);
                    Vector3 Closeangle = new Vector3(-90f, 0, -90f);
                    AnimatorStateInfo currentAnim = animator.GetCurrentAnimatorStateInfo(0);
                    if(currentAnim.IsName("DoorClose") || currentAnim.IsName("Idle")){
                        obj.transform.DORotate(Openangle, 1f);
                        animator.Play("DoorOpen");
                        // Source.PlayOneShot(doorOpenSound);
                    } else if(currentAnim.IsName("DoorOpen")){
                        Debug.Log("Close");
                        obj.transform.DORotate(Closeangle, 1f);
                        animator.Play("DoorClose");
                        // Source.PlayOneShot(doorCloseSound);
                    } 
                }
            }
            
        }

        
    }
}
