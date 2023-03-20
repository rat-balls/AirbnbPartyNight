using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] Transform[] Points;

    [SerializeField] private float moveSpeed;

    private Vector3 lastpos;
    public GameObject player;
    public UnityEngine.AI.NavMeshAgent pixelle;
    public Camera carCam;
    public AudioSource radio;
    public AudioSource gravel;
    public AudioLowPassFilter bassFilter;

    private bool Cinematic = true;
    private int pointsIndex;
    // Start is called before the first frame update
    void Start()
    {   
        transform.position = Points[pointsIndex].transform.position;
        lastpos = transform.position;
        player.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (pointsIndex <= Points.Length - 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, Points[pointsIndex].transform.position, moveSpeed * Time.deltaTime);

            if (transform.position == Points[pointsIndex].transform.position)
            {
                pointsIndex += 1;
            }
            Vector3 direction = transform.position - lastpos;
            transform.forward = Vector3.Lerp(transform.forward, direction, 0.1f);
        } else if(Cinematic == true){
            player.SetActive(true);
            carCam.enabled = !carCam.enabled;
            gravel.enabled = !gravel.enabled;
            pixelle.enabled = !pixelle.enabled;
            radio.spatialBlend = 1;
            bassFilter.cutoffFrequency = 2786f;
            bassFilter.lowpassResonanceQ = 4.8f;
            Cinematic = false;
        }
        lastpos = transform.position;
    }
}
