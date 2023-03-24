using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Interact : MonoBehaviour
{   
    public int rayLength = 8;
    public LayerMask interactableMask;
    public string excludeLayerName = "Default";
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
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | interactableMask.value;

        Debug.DrawRay(transform.position, fwd, Color.red);

        if(Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {   
            if(hit.collider.CompareTag("door"))
            {   
                GameObject obj = hit.collider.gameObject;
                
                if(Input.GetKeyDown(KeyCode.E) && timer <= 0f)
                {   
                    
                    timer = 1f;
                    Animator animator = obj.GetComponent<Animator>();
                    AnimatorStateInfo currentAnim = animator.GetCurrentAnimatorStateInfo(0);

                    if(currentAnim.IsName("DoorClose") || currentAnim.IsName("Idle")){
                        obj.transform.DORotate(new Vector3(0, 0, -90), 0.5f, RotateMode.LocalAxisAdd);
                        animator.Play("DoorOpen");
                        Source.PlayOneShot(doorOpenSound);
                    } else if(currentAnim.IsName("DoorOpen")){
                        obj.transform.DORotate(new Vector3(0, 0, 90), 0.3f, RotateMode.LocalAxisAdd);
                        animator.Play("DoorClose");
                        Source.PlayOneShot(doorCloseSound);
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
