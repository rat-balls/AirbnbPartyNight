using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Interact : MonoBehaviour
{   
    [Header("Interaction")]
    public LayerMask interactableMask; //Layer that you can interact with
    public int rayLength = 12; //Interact Range
    public string excludeLayerName = "Default"; //Layer that you can't interact through (i.g. walls)

    [Header("Audio")]
    public AudioSource Source; //Player Audio Source
    public AudioClip doorCloseSound; //Sound clips
    public AudioClip doorOpenSound;
    public AudioClip itemGetSound;

    [Header("Notifications")]
    public TMPro.TextMeshProUGUI notificationText; //UI text displayer for notifications


    private bool hasKey = false; //Boolean to check if the player can open locked doors or not
    
    private void Start() {
        DOTween.Init(); //Initialises DOTween (I use it to animate the doors)
    }
    
    void Update()
    {
        RaycastHit hit; //Raycast hit name
        Vector3 fwd = transform.TransformDirection(Vector3.forward); //Raycast direction

        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | interactableMask.value; //Prevents Raycast from hitting through the exluded Layer (walls)

        Debug.DrawRay(transform.position, fwd, Color.green); //Draws Raycasts in the Scene for devs

        if(Physics.Raycast(transform.position, fwd, out hit, rayLength, mask)) //Draws a Raycast and checks what it collides with
        {   
            if(hit.collider.CompareTag("door")) //If the collided object tag is "door", do this
            {   
                GameObject obj = hit.collider.gameObject; //Gets the door GameObject
                
                if(Input.GetKeyDown(KeyCode.E)) //Checks for E Input on the keyboard
                {   
                    if(hasKey) //If player has the key, open the door
                    {   
                        StartCoroutine(doorCD(obj)); //Starts a script in parallel to removed door collider so the player doesn't get pushed + since colliders are used to open door player can't open the door in the meantime

                        Animator animator = obj.GetComponent<Animator>(); //Gets Animator Component from the door
                        AnimatorStateInfo currentAnim = animator.GetCurrentAnimatorStateInfo(0); //Checks current animation on the door, I am using these as a boolean without having to put a script on the doors

                        if(currentAnim.IsName("DoorClose") || currentAnim.IsName("Idle")){ //If the door is closed or has never been Opened, Open it
                            obj.transform.DORotate(new Vector3(0, 0, -90), 0.5f, RotateMode.LocalAxisAdd); //Adds -90° to the door, opening it
                            animator.Play("DoorOpen"); //Switches the "boolean" to opened
                            Source.PlayOneShot(doorOpenSound); //Plays the door opening sound
                        } else if(currentAnim.IsName("DoorOpen")){ //If the door is open, close it
                            obj.transform.DORotate(new Vector3(0, 0, 90), 0.3f, RotateMode.LocalAxisAdd); //Adds 90° to the door, closing it
                            animator.Play("DoorClose"); //Switches the "boolean" to closed
                            Source.PlayOneShot(doorCloseSound); //Plays the door closing sound
                        }
                    } else { //Else notify the player it is locked
                        StartCoroutine(sendNotification("This door appears to be [Locked]", 3)); //Starts a parallel script that displays a notification on the screen
                    }
                }
            } 

            if(hit.collider.CompareTag("key")) //Same principles as above
            {
                GameObject obj = hit.collider.gameObject;
                
                if(Input.GetKeyDown(KeyCode.E))
                {
                    Source.PlayOneShot(itemGetSound);
                    obj.SetActive(false); //Deactivates the key and keyHitbox so the player doesn't use it multiple times
                    StartCoroutine(sendNotification("[Mansion Key] has been added to your inventory.", 3));
                    hasKey = true; //Flips the hasKey boolean
                }    
            }
            
        }
    }

    IEnumerator sendNotification(string text, int time) //Displays a notification on the screen for x amount of time
    {
    notificationText.text = text;
    yield return new WaitForSeconds(time);
    notificationText.text = "";
    }

    IEnumerator doorCD(GameObject obj)
    {   
        obj.GetComponent<MeshCollider>().enabled = !obj.GetComponent<MeshCollider>().enabled; //Removes the colliders from the doors so it doesn't push the player and the player can't use it mid-animation
        yield return new WaitForSeconds(0.5f);
        obj.GetComponent<MeshCollider>().enabled = !obj.GetComponent<MeshCollider>().enabled;
    }

}

