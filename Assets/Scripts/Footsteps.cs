using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class Footsteps : MonoBehaviour
{
    public AudioSource source;
    public AudioClip footstep1;
    public AudioClip footstep2;
    public AudioClip footstep3;
    public AudioClip footstep4;
    public AudioClip footstep5;
    public AudioClip footstep6;
    public AudioClip footstep7;
    public AudioClip footstep8;
    CharacterController cc;
    public bool isCrouched = false;
    private bool isSprinting = false;
    private bool hasSprinted = false;
    private bool hasWalked = false;
    private bool isTired = false;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        isTired = (GameObject.Find("Player").GetComponent<PlayerController>().isTired);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouched = true;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            isCrouched = false;
        }

        UpdateFootsteps();
    }

    IEnumerator walkingSteps(){
        while (isSprinting == false && isCrouched == false && cc.velocity.magnitude >2f) {
            var random = new System.Random();
            var list = new List<AudioClip>{ footstep1, footstep2, footstep3, footstep4, footstep5, footstep6, footstep7, footstep8 };
            source.PlayOneShot(list[random.Next(list.Count)]);
            yield return new WaitForSeconds (0.6f);
        }
        hasWalked = false;

    }

    IEnumerator sprintingSteps(){
        while (isSprinting == true && isCrouched == false && cc.velocity.magnitude >2f && isTired == false) {
            var random = new System.Random();
            var list = new List<AudioClip>{ footstep1, footstep2, footstep3, footstep4, footstep5, footstep6, footstep7, footstep8 };
            source.PlayOneShot(list[random.Next(list.Count)]);
            yield return new WaitForSeconds (0.4f);
        }
        hasSprinted = false;
    }

    void UpdateFootsteps() {
        if (isSprinting == false && hasWalked == false) {
            hasWalked = true;
            StartCoroutine("walkingSteps");
        }
        if (isSprinting == true && hasSprinted == false) {
            hasSprinted = true;
            StartCoroutine("sprintingSteps");
        }
    }
}