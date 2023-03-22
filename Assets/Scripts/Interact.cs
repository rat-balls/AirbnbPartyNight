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

    public float timer = 0f;

    
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
            if(obj.tag == "door")
            {   
                
                Animator animator = obj.GetComponent<Animator>();
                
                if(Input.GetKeyDown(KeyCode.E) && timer <= 0f)
                {   
                    
                    timer = 1f;
                    AnimatorStateInfo currentAnim = animator.GetCurrentAnimatorStateInfo(0);

                    if(currentAnim.IsName("DoorClose") || currentAnim.IsName("Idle")){
                        obj.transform.DORotate(new Vector3(0, 0, -90), 0.5f, RotateMode.LocalAxisAdd);
                        animator.Play("DoorOpen");
                        // Source.PlayOneShot(doorOpenSound);
                    } else if(currentAnim.IsName("DoorOpen")){
                        Debug.Log("Close");
                        obj.transform.DORotate(new Vector3(0, 0, 90), 0.5f, RotateMode.LocalAxisAdd);
                        animator.Play("DoorClose");
                        // Source.PlayOneShot(doorCloseSound);
                    } 
                }
            } 
            
        }
        timer -= 1.5f * Time.deltaTime;
        if(timer < 0){
            timer = 0;
        }
    }
}
