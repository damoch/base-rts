using UnityEngine;
using System.Collections;

public class TerrainController : MonoBehaviour {

	void Update()
    {
       if(Input.GetMouseButton(0)) PlayerController.ClearUnitsList();
       else if (Input.GetMouseButton(1)) PlayerController.MovementOrder();
    }
}
