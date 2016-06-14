using UnityEngine;
using System.Collections;

public class Terrain : MonoBehaviour {

	void OnMouseDown()
    {
        PlayerController.ClearUnitsList();
    }
}
