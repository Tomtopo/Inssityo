using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    private Transform _lookDirection;
    [SerializeField] private float _interactionDistance;
    [SerializeField] private LayerMask _doorLayer;
    // Start is called before the first frame update
    void Start()
    {
        _lookDirection = GameObject.Find("PlayerCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(_lookDirection.position, _lookDirection.forward, out hit, _interactionDistance, _doorLayer) && !hit.transform.gameObject.GetComponent<DoorController>().doorBarged)
        {         
            GameObject.Find("UI").transform.GetChild(0).gameObject.SetActive(true);               
        }
        else
        {
            GameObject.Find("UI").transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Interact";
            GameObject.Find("UI").transform.GetChild(0).gameObject.SetActive(false);

        }
            
    }

    public void OnInteraction()
    {
        RaycastHit hit;
        if (Physics.Raycast(_lookDirection.position, _lookDirection.forward, out hit, _interactionDistance, _doorLayer) && !hit.transform.gameObject.GetComponent<DoorController>().doorBarged)
        {
            string layer = hit.transform.gameObject.name;

            switch (layer)
            {
                case "Door":
                    hit.transform.GetComponent<DoorController>().InteractWithDoor();
                    break;
                //case "PickUp":
                //    hit.transform.GetComponent<ScriptName>().Collect();
                //    break;
                default:
                    break;
            }

            Debug.Log(hit.transform.name);
        }
        else
        {
            Debug.Log("Did not hit.");
        }
    }
}
