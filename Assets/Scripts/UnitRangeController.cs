using UnityEngine;
using System.Collections;

public class UnitRangeController : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.tag == "Unit")
        {
            GameObject DetectedUnit = col.gameObject;
            //Debug.Log("sasasa");
            if (DetectedUnit.GetComponent<Unit>().Owner != PlayerController._PlayerName) GetComponentInParent<Unit>().Attack(DetectedUnit);
        }
        /* if(GetComponent<Collider>().gameObject.GetComponent<Unit>().Owner!=PlayerController._PlayerName)
         GetComponentInParent<Unit>().Attack(GetComponent<Collider>().gameObject);*/
    }
    void OnTriggerExit(Collider col)
    {

        Debug.Log(col.gameObject.tag);
        if (col.tag == "Unit")
        {
            //Debug.Log("sasasa");
            if (col.gameObject.GetComponent<Unit>().Owner != PlayerController._PlayerName) GetComponentInParent<Unit>().Attack(null);

        }
    }
}
