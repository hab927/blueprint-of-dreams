using UnityEngine;

public class pedestal_component : MonoBehaviour
{
    public door_behaviour door_manager;
    public int index_order;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void editOrder()
    {
        GameObject child = transform.GetChild(0).gameObject;
        if(child != null)
        {
            door_manager.order[index_order] = child;
        }
        
    }

}
