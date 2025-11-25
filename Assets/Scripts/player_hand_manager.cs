using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class player_hand_manager : MonoBehaviour
{
    [Header("interactable layers")]
    [SerializeField] private LayerMask interactableLayers;
    List<GameObject> interactables = new List<GameObject>();
    [Header("item grab settings")]
    public Vector3 hand_offset;
    [Header("item grab references")]
    public Transform grabbed_item;
    [Header("item grab flags")]
    public bool item_held = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if ((interactableLayers.value & (1 << other.gameObject.layer)) != 0)
        {
            interactables.Add(other.gameObject);
            interaction_object_functions IOF = other.transform.GetComponent<interaction_object_functions>();
            if(IOF != null)
            {
                IOF.lerpOutlineInHelper();
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        if ((interactableLayers.value & (1 << other.gameObject.layer)) != 0)
        {
            interactables.Remove(other.gameObject);
            interaction_object_functions IOF = other.transform.GetComponent<interaction_object_functions>();
            if(IOF != null)
            {
                IOF.lerpOutlineOutHelper();
            }
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactables.Count > 0)
        {
            //UnityEngine.Debug.Log("interacted with " + interactables[0] + "!");
            interaction_object_functions object_behavior = interactables[0].GetComponent<interaction_object_functions>();

            if(object_behavior != null)
            {
                switch (object_behavior.transform.tag)
                {
                    case "item":
                        //UnityEngine.Debug.Log("picked up an item!");
                        object_behavior.pickupItem(transform, hand_offset);
                        grabbed_item = object_behavior.transform;
                        item_held = true;
                        break;
                    case "placement":
                        //UnityEngine.Debug.Log("object is a pedestal");
                        if (grabbed_item != null)
                        {
                            //UnityEngine.Debug.Log("placed an item!");
                            object_behavior.placeItem(grabbed_item, new Vector3(0, 0.5f, 0));
                            grabbed_item = null;
                            break;
                        }
                        else
                        {
                            object_behavior = object_behavior.transform.GetChild(0).GetComponent<interaction_object_functions>();
                            if (object_behavior != null)
                            {
                                object_behavior.pickupItem(transform, hand_offset);
                                grabbed_item = object_behavior.transform;
                            }
                        }
                        break;
                    default:
                        UnityEngine.Debug.Log("dropped on ground");
                        break;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && item_held)
        {
            if (grabbed_item != null)
            {
                transform.GetComponentInChildren<interaction_object_functions>().dropItem(grabbed_item, transform.position - new Vector3(0, 1.3f, 0));
                grabbed_item = null;
                item_held = false;
            }
        }
    }
}
