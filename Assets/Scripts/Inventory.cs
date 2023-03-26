using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Inventory : MonoBehaviour
{   
    [Header("Selection")]
    public LayerMask itemMask; //Layer that you can interact with
    public int rayLength = 4; //Interact Range
    public string excludeLayerName = "Default"; //I kept this cuz i cant be bothered to change the code

    [Header("Objects")]
    public GameObject MainCam;
    public GameObject InvCam;
    public GameObject key;
    public AudioSource Source;
    public GameObject monster;
    public Transform invWheel;
    public Interact inter;

    [Header("Text")]
    public TMPro.TextMeshProUGUI notificationText;

    private bool inventory = false;
    private bool rotCD = false;
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
    }

    // Update is called once per frame
    void Update()
    {   
        if(inter.hasKey)
        {
            key.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(!inventory)
            {   
                inventory = true;
                GetComponent<PlayerController>().enabled = false;
                GetComponent<Footsteps>().enabled = false;
                Source.enabled = false;
                monster.SetActive(false);
                MainCam.SetActive(false);
                InvCam.SetActive(true);
            } else {
                inventory = false;
                GetComponent<PlayerController>().enabled = true;
                GetComponent<Footsteps>().enabled = true;
                Source.enabled = true;
                monster.SetActive(true);
                MainCam.SetActive(true);
                InvCam.SetActive(false);
                notificationText.text = "";
            }
        }

        if(inventory && !rotCD)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartCoroutine(RotateWheel(90f));
            } else if(Input.GetKeyDown(KeyCode.RightArrow)){
                StartCoroutine(RotateWheel(-90f));
            }

            RaycastHit hit; //Raycast hit name
            Vector3 fwd = InvCam.transform.TransformDirection(Vector3.forward); //Raycast direction

            int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | itemMask.value; //Prevents Raycast from hitting through the exluded Layer (walls)

            Debug.DrawRay(InvCam.transform.position, fwd, Color.green); //Draws Raycasts in the Scene for devs

            if(Physics.Raycast(InvCam.transform.position, fwd, out hit, rayLength, mask)) //Draws a Raycast and checks what it collides with
            {   
                if(hit.collider.CompareTag("key"))
                {   
                    notificationText.DOFade(100f, 0f);
                    notificationText.text = "[Mansion Key] <br> Key to the mysterious mansion"; //Changes the text of the notification
                } else if(hit.collider.CompareTag("flashlight")) {
                    notificationText.DOFade(100f, 0f);
                    notificationText.text = "[Flashlight] <br> Your 'trusty' flashlight";
                } else if(hit.collider.CompareTag("item1") || hit.collider.CompareTag("item2")){
                    notificationText.DOFade(100f, 0f);
                    notificationText.text = "[Placeholder item] <br> Item to hold the place of the holder";
                } else if(hit.collider.CompareTag("misc")){
                    notificationText.DOFade(0f, 0.5f);
                }

                if(Input.GetKeyDown(KeyCode.E))
                {
                Debug.Log(hit.collider.tag);
                }
            }
        }

        
    }

    IEnumerator RotateWheel(float z)
    {   
        rotCD = true;
        invWheel.DORotate(new Vector3(0f, 0f, z), 0.3f, RotateMode.LocalAxisAdd);
        yield return new WaitForSeconds(0.3f);
        rotCD = false;
    }
}
