using UnityEngine;
using System.Collections;

public class UnitRangeController : MonoBehaviour
{
    GameObject DetectedUnit;
    Unit Parent;
    void Start()
    {
        Parent = GetComponentInParent<Unit>();
    }
    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.tag == "Unit")
        {
            if (col.gameObject.GetComponent<Unit>().Owner !=Parent.Owner)
            {
                Debug.Log("Hostile spotted!");
                DetectedUnit = col.gameObject;
                if (Parent.Target == DetectedUnit&&!Parent.targetInRange) { Parent.SetFacing(); Debug.Log("Gotya!"); Parent.StopIfTargetInRange();Parent.targetInRange = true; }
                else { }
            }
            
        }
    }
    void OnTriggerExit(Collider col)
    {

        Debug.Log(col.gameObject.tag);
        if (col.tag == "Unit")
        {
            //Debug.Log("sasasa");
           // if (col.gameObject.GetComponent<Unit>().Owner != PlayerController._PlayerName) GetComponentInParent<Unit>().Attack(null);
           if(col.gameObject== (Parent.Target))
            {
                Parent.Target = null;
                Parent.targetInRange=false;
            }

        }
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            PlayerController.ClearUnitsList();
        }
    }
}
