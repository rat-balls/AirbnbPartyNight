using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FlashlightFunction : MonoBehaviour
{
    private bool OnOrOff = false;
    private bool hasBeenSwitched = false;
    public AudioSource source;
    public AudioClip clip;
    private int RandomFlashlightFunny = 0;

    void Update()
    {
        if (gameObject.activeSelf == true)
        {


            if (Input.GetMouseButtonDown(0))
            {
                if (OnOrOff == false)
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    hasBeenSwitched = true;
                    OnOrOff = true;
                    source.PlayOneShot(clip);
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (hasBeenSwitched == false)
                {
                    if (OnOrOff == true)
                    {
                        gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        OnOrOff = false;
                        source.PlayOneShot(clip);
                    }
                }
            }

            RandomFlashlightFunny = Random.Range(1, 100000);
            if (RandomFlashlightFunny == 69)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                OnOrOff = false;
                source.PlayOneShot(clip);
            }

            hasBeenSwitched = false;
        }

    }
}
