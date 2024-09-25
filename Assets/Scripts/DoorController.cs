using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorController : MonoBehaviour
{

    private Transform _doorPivot;
    private Vector3 _openDirection;
    private Vector3 _closedDirection;
    public bool doorOpen;
    public bool isDoorLocked;
    // Start is called before the first frame update
    void Start()
    {
        _doorPivot = transform.parent.transform;
        _openDirection = _doorPivot.right;
        _closedDirection = _doorPivot.forward;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public void InteractWithDoor()
    //{
    //    if(!_doorOpen)
    //    {
    //        Vector3 newDirection = Vector3.RotateTowards(_doorPivot.forward, _openDirection, Time.deltaTime, 0f);
    //        _doorPivot.rotation = Quaternion.LookRotation(newDirection);
    //    }
    //    else
    //    {
    //        Vector3 newDirection = Vector3.RotateTowards(_doorPivot.forward, _closedDirection, Time.deltaTime, 0f);
    //        _doorPivot.rotation = Quaternion.LookRotation(newDirection);
    //    }

    //}

    //public void CloseDoor()
    //{
    //    Vector3 newDirection = Vector3.RotateTowards(_doorPivot.forward, _closedDirection, Time.deltaTime, 0f);
    //    _doorPivot.rotation = Quaternion.LookRotation(newDirection);
    //}

    public void BargeDoor(Vector3 barger, float bargePower)
    {
        Debug.Log("SMASH!");
        gameObject.AddComponent<Rigidbody>();
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.mass = 3f;
        rb.drag = 8f;
        rb.angularDrag = 0f;
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(transform.position.x - barger.x, transform.position.y - barger.y, transform.position.z - barger.z).normalized * bargePower, ForceMode.Impulse);
        isDoorLocked = false;
        doorOpen = true;
    }

    public void InteractWithDoor()
    {
        if(!isDoorLocked)
        {
            StopAllCoroutines();
            StartCoroutine("Interact");
        }
        else
            GameObject.Find("UI").transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Door locked";

    }

    IEnumerator Interact()
    {
        if(!doorOpen)
        {
            doorOpen = true;
            while (Vector3.Angle(_doorPivot.forward, _openDirection) > 1f)
            {
                Vector3 newDirection = Vector3.RotateTowards(_doorPivot.forward, _openDirection, Time.deltaTime, 0f);
                _doorPivot.rotation = Quaternion.LookRotation(newDirection);
                yield return new WaitForEndOfFrame();
            }
            
        }
        else
        {
            doorOpen = false;
            while (Vector3.Angle(_doorPivot.forward, _closedDirection) > 1f)
            {
                Vector3 newDirection = Vector3.RotateTowards(_doorPivot.forward, _closedDirection, Time.deltaTime, 0f);
                _doorPivot.rotation = Quaternion.LookRotation(newDirection);
                yield return new WaitForEndOfFrame();
            }
            
        }
    }
}
