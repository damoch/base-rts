using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
    //Inspector visible properties
    public float ScrollingSpeed;
    public string PlayerName;

    //I need this in static :(

    public static string _PlayerName;

    //Not visible in inspector
    public static List<Unit> selectedUnits = new List<Unit>();
    GameObject Map;
    static GameObject _Map;
    Camera PlayerCamera;
    Rect SelectionBox;
    public static bool SelectionBoxActive;
    public static GameObject _Target;
    //Drawing shit
    static Texture2D _whiteTexture;
    
    //selecting units
    public static bool isSelecting = false;
    public static Vector3 mousePosition1;

    void Start () {
        selectedUnits.Clear();
        Map = GameObject.Find("Terrain");
        _Map = Map;
        PlayerCamera = Camera.main;

        SelectionBoxActive = false;
        // Rect rect = new Rect(430, 33, 10, 10);

        _PlayerName = PlayerName;
    }
	
	void Update () {
        if (Input.GetMouseButtonDown(1)) MovementOrder();
        //if(Input.GetMouseButtonDown(1)) AttackOrder();
        //camera height
        /*  if(Input.GetAxis("Mouse ScrollWheel") < 0)
          {
              PlayerCamera.transform.Translate(0, ScrollingSpeed, 0);
          }*/
        //Camera scrolling
        if (Input.mousePosition.x > Screen.width - 50) PlayerCamera.transform.Translate(ScrollingSpeed, 0, 0,0);
        if (Input.mousePosition.x < 50) PlayerCamera.transform.Translate(-ScrollingSpeed, 0, 0,0);
        if (Input.mousePosition.y > Screen.height - 50) PlayerCamera.transform.Translate(0, 0, ScrollingSpeed, 0);
        if (Input.mousePosition.y < 50) PlayerCamera.transform.Translate(0, 0, -ScrollingSpeed, 0);

        //Unit selecting
    
        // If we press the left mouse button, save mouse location and begin selection
        if (Input.GetMouseButtonDown(0))
        {
            //ClearUnitsList();
            isSelecting = true;
            mousePosition1 = Input.mousePosition;
        }

        // If we let go of the left mouse button, end selection
        if (Input.GetMouseButtonUp(0))
            isSelecting = false;


    }
    public static void ClearUnitsList()
    {


        if (selectedUnits.Count > 0)
        {
            foreach(Unit unit in selectedUnits)
            {
                unit.Selected.SetActive(false);
            }
            selectedUnits.Clear();
            Debug.Log("List Empty!");
        }
    }
   
    public static void AttackOrder(GameObject _Target)
    {
        Debug.Log("Kill them fuckers");
       // MovementOrder();
        foreach(Unit unit in selectedUnits)
        {
            unit.Target = _Target;
            // if(unit.targetInRange==false)unit.Move(_Target.transform.position);
            unit.Attack();

        }

    }
    public static void MovementOrder()
    {
        Debug.Log("Moving!");
        //Giving order to each selected unit
        Vector3 OrderCoordinates = new Vector3(0, 0, 0);
        RaycastHit hit;


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (_Map.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
        {
            OrderCoordinates = hit.point;
            foreach (Unit unit in selectedUnits)
            {
                
                unit.Move(OrderCoordinates);
            }
        }
    }
    public static Texture2D WhiteTexture
    {
        get
        {
            if (_whiteTexture == null)
            {
                _whiteTexture = new Texture2D(1, 1);
                _whiteTexture.SetPixel(0, 0, Color.white);
                _whiteTexture.Apply();
            }

            return _whiteTexture;
        }
    }

    public static void DrawScreenRect(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, WhiteTexture);
        GUI.color = Color.white;
    }
    void OnGUI()
    {
        
        if (isSelecting)
        {
            // Create a rect from both mouse positions
            var rect = PlayerController.GetScreenRect(mousePosition1, Input.mousePosition);
            PlayerController.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            PlayerController.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }
    public static void DrawScreenRectBorder(Rect rect, float thickness, Color color)
    {
        // Top
        DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
        // Left
        DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
        // Right
        DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
        // Bottom
        DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
    }

    public static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        // Move origin from bottom left to top left
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;
        // Calculate corners
        var topLeft = Vector3.Min(screenPosition1, screenPosition2);
        var bottomRight = Vector3.Max(screenPosition1, screenPosition2);
        // Create Rect
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }

    public static Bounds GetViewportBounds(Camera camera, Vector3 screenPosition1, Vector3 screenPosition2)
    {
        var v1 = Camera.main.ScreenToViewportPoint(screenPosition1);
        var v2 = Camera.main.ScreenToViewportPoint(screenPosition2);
        var min = Vector3.Min(v1, v2);
        var max = Vector3.Max(v1, v2);
        min.z = camera.nearClipPlane;
        max.z = camera.farClipPlane;

        var bounds = new Bounds();
        bounds.SetMinMax(min, max);
        return bounds;
    }
}

