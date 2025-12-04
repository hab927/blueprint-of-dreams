using UnityEngine;
using System.Collections.Generic;

public class door_behaviour : MonoBehaviour
{
    public GameObject[] code;
    public List<GameObject> order = new List<GameObject>();
    public GameObject door;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void checkOrder()
    {
        for (int i = 0; i < code.Length; i++)
        {
            if (order[i] != null)
            {
                if (order[i] != code[i])
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
        UnityEngine.Debug.Log("order matches");
        door.SetActive(false);
    }
}
