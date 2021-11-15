using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    private bool isOpened = false;
    private bool inRange = true;
    [SerializeField] private GameObject chest;
    [SerializeField] private GameObject star;
    [SerializeField] private GameObject starSpawn;
    [SerializeField] private GameObject goObj;
 
    private Animator anim;

    private bool starShootInput = false; 

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }



    // Update is called once per frame
    void Update()
    {
        // when the button is pressed and the player is within the collider range you can open the chest. 
        if (Input.GetKeyDown(KeyCode.E) && inRange)
        {
            isOpened = true;

            // there is probably is a better way to do this-- but quick technique to only consider the input once 
            if (starShootInput == false)
            {
                starShootInput = true;

                // repeating invoke the spawn/StarShoot method 
                // start in 0.1 secs 
                // repeat every 0.4f
                InvokeRepeating("StarShoot", 0.1f, 0.4f);
            }
            anim.SetBool("IsOpened", isOpened);   
        }
        //otherwise opening chest is nono at the moment
        else
        {
            isOpened = false;
        }
    }

    // spawning the stars based the transform positions =))
    private void StarShoot()
    {

        goObj = GameObject.Instantiate(star, starSpawn.transform.position, starSpawn.transform.rotation);
        goObj.transform.Rotate(new Vector3(0, 180, 0));
    }

    // determine if the player is within the range 
    private void OnTriggerEnter2D(Collider2D other)
     {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            inRange = true;
        }
     } 

    // or not!
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            inRange = false;
            isOpened = false;
        }
    } 
}
