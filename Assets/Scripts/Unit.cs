using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
    //Inspector visible properties
    public string Name;
    public float Speed;

    bool isSelected;
    NavMeshAgent Navigator;
    


    void Start()
    {
        Navigator = GetComponent<NavMeshAgent>();
        isSelected = false;

        Navigator.speed = Speed;
    }
	
    void OnMouseDown()
    {
        //Adding unit to player's selected units list
        if (!(PlayerController.selectedUnits.Contains(this))){
            PlayerController.selectedUnits.Add(this);
            Debug.Log(PlayerController.selectedUnits.Count);
            isSelected = true;
        }
        else//removing if unit already is on that list
        {
            PlayerController.selectedUnits.Remove(this);
            isSelected = false;
            Debug.Log(PlayerController.selectedUnits.Count);
        }
    }
    public void Move(Vector3 Target)
    {

        Navigator.destination = Target;
        Debug.Log("Going to target " + Target.ToString() );
    }
}
