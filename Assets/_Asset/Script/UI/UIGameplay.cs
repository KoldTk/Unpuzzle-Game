using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIGameplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WaypointClick();
    }
    private void WaypointClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int waypointLayer = LayerMask.GetMask("Block");
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Find target
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, waypointLayer);
            if (hit.collider != null)
            {
                EventDispatcher<GameObject>.Dispatch(Event.MoveBlock.ToString(), hit.transform.gameObject);
            }
        }
    }
}
