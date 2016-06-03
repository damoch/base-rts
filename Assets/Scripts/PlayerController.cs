using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
    //Inspector visible properties
    public float ScrollingSpeed;

    //Not visible in inspector
    public static List<Unit> selectedUnits = new List<Unit>();
    GameObject Map;
    Camera PlayerCamera;
    Rect SelectionBox;
    bool SelectionBoxActive;


    void Start () {
        selectedUnits.Clear();
        Map = GameObject.Find("Terrain");
        PlayerCamera = Camera.main;
        //SelectionBox.xMin = Input.mousePosition.x;
        //SelectionBox.yMin = Input.mousePosition.y;
        //SelectionBox.xMax = Input.mousePosition.x+50;
       // SelectionBox.yMax = Input.mousePosition.y+50;
        SelectionBoxActive = false;
       // Rect rect = new Rect(430, 33, 10, 10);


    }
	
	void Update () {
        if (Input.GetMouseButtonDown(1)) MovementOrder();

        //Camera scrolling
        if (Input.mousePosition.x > Screen.width - 50) PlayerCamera.transform.Translate(ScrollingSpeed, 0, 0);
        if (Input.mousePosition.x < 50) PlayerCamera.transform.Translate(-ScrollingSpeed, 0, 0);
        if (Input.mousePosition.y > Screen.height - 50) PlayerCamera.transform.Translate(0, ScrollingSpeed, 0);
        if (Input.mousePosition.y < 50) PlayerCamera.transform.Translate(0, -ScrollingSpeed, 0);

        //Unit selecting
        if (Input.GetMouseButtonDown(0)&&!SelectionBoxActive)
        {
            SelectionBox.xMin = Input.mousePosition.x;
            SelectionBox.yMin = Input.mousePosition.y;
            SelectionBoxActive = true;
        }
        if (SelectionBoxActive)
        {
            SelectionBox.xMax = Input.mousePosition.x;
            SelectionBox.yMax = Input.mousePosition.y;
        }
        if (Input.GetMouseButtonUp(0) && SelectionBoxActive)
        {
            SelectionBoxActive = false;
            Debug.Log(SelectionBox.height);
        }


    }

    void MovementOrder()
    {    
        //Setting coordinates  
        Vector3 OrderCoordinates=new Vector3(0,0,0);
        RaycastHit hit;
       
    
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Map.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
        {
            OrderCoordinates = hit.point;
        }

        //Giving order to each selected unit
        foreach (Unit unit in selectedUnits)
        {
            unit.Move(OrderCoordinates);
        }
    }
}
