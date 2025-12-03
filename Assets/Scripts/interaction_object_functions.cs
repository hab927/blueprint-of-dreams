using System;
using System.Collections;
using TreeEditor;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class interaction_object_functions : MonoBehaviour
{
    public Material outline_factor;
    public int OutlineID;
    public UnityEvent On_Item_Grabbed;
    public UnityEvent On_Item_Placed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //public Action interaction;

    void Start()
    {
        OutlineID = Shader.PropertyToID("_size");
        outline_factor = transform.GetComponent<MeshRenderer>().materials[1];
        outline_factor.SetFloat(OutlineID, 1f);
    }
    public void pickupItem(Transform player_hand, Vector3 hand_position)
    {
        transform.SetParent(player_hand);
        transform.localRotation = Quaternion.identity;
        transform.localPosition = hand_position;
        if(On_Item_Grabbed != null)
        {
            On_Item_Grabbed.Invoke();
        }
    }

    public bool placeItem(Transform item, Vector3 position_offset)
    {
        if(transform.childCount > 0)
        {
            return false;
        }
        item.parent = transform;
        item.localPosition = position_offset;
        item.localRotation = quaternion.identity;
        if (On_Item_Placed != null)
        {
            On_Item_Placed.Invoke();
        }

        return true;
    }

    public void dropItem(Transform item, Vector3 position_offset)
    {
        item.parent = null;
        item.localPosition = position_offset;
        item.localRotation = Quaternion.identity;
    }

    public void lerpOutlineInHelper()
    {
        StartCoroutine(lerpOutline(1f, 1.05f, 0.2f));
    }
    
    public void lerpOutlineOutHelper()
    {
        StartCoroutine(lerpOutline(1.05f, 1f, 0.2f));
    }

    public IEnumerator lerpOutline(float start_val, float end_val, float lerp_time)
    {
        float timer = 0f;
        while (timer < lerp_time)
        {
            float lerp_value = Mathf.Lerp(start_val, end_val, timer / lerp_time);
            outline_factor.SetFloat(OutlineID, lerp_value);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        outline_factor.SetFloat(OutlineID, end_val);
    }

    public void Debug_Event_Test(string eventText)
    {
        UnityEngine.Debug.Log("this is a test, "+ eventText);
    }
}
