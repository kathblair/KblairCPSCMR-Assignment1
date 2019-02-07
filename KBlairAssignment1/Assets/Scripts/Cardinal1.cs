using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cardinal1 : MonoBehaviour
{
    public float speed = 10;
    public string word = "Hello World";
    public string rly = "why World";
    public bool currentlyFlying; // to see if it's flying
    public bool currentlyRotating; // to see if it's flying
    public GameObject firstSpot;
    public GameObject secondSpot;
    public GameObject thirdSpot;
    public GameObject fourthSpot;
    Animator animator;

    public bool reachedDestination = false;

   // public AudioClip successSound;

    //private AudioSource source;
    private Vector3 moveTarget;

    // Start is called before the first frame update

    private void Awake()
    {
       // source = GetComponent<AudioSource>();
    }

    void Start()
    {
        print("started Cardinal");
        //set movetarget to the position of the first spot.
        moveTarget = firstSpot.transform.position;
        // rotate the bird to be looking at the right spot.
        //transform.Rotate(0, 90, 0);
        currentlyFlying = true;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {   // happens only when cardinal is active.
        float step = speed * Time.deltaTime;
        if(currentlyFlying == true)
        {
            //going to need to be able to tell when I want to do this, and whether I should be rotating, etc.
            transform.position = Vector3.MoveTowards(transform.position, moveTarget, step);
        }else if(currentlyRotating == true)
        {

        }
        
    }

    void FixedUpdate()
    {
        
    }

    public void CardinalEasyAR()
    {
        print("called from EasyAR");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spot1"))
        {
            
            
            //set the target to spot2.
            moveTarget = secondSpot.transform.position;
            transform.LookAt(moveTarget);

        }else if (other.gameObject.CompareTag("Spot2"))
        {
            moveTarget = thirdSpot.transform.position;
            transform.LookAt(moveTarget);
        }
        else if (other.gameObject.CompareTag("Spot3"))
        {
            moveTarget = fourthSpot.transform.position;
            transform.LookAt(moveTarget);
        }
        else if (other.gameObject.CompareTag("Spot4"))
        {
            print("in final position");
            reachedDestination = true;
            GetComponent<AudioSource>().Play();
            animator.SetTrigger("Flap");
        }
    }

}
