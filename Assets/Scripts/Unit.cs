using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
    //Inspector visible properties
    public string Name;
    public float Speed;
    public float AttackRange;
    public string Owner;
    public GameObject Selected;
    GameObject Target;
    bool isSelected;
    NavMeshAgent Navigator;
    bool attacking;


    void Start()
    {
        attacking = false;
        Selected.SetActive(false);
        Navigator = GetComponent<NavMeshAgent>();
        isSelected = false;

        Navigator.speed = Speed;
    }
	void Update()
    {
        isSelected = IsWithinSelectionBounds();
        if (isSelected) UnitListControll();
    }
    void OnMouseDown()
    {
        UnitListControll();
        if (Input.GetMouseButton(1) && Owner != PlayerController._PlayerName)
        {
            Debug.Log("Attack");
        }
       
    }
    void UnitListControll()
    {
       

            if (!(PlayerController.selectedUnits.Contains(this))&&PlayerController._PlayerName==Owner)
            {
                AddUnitToList();
            }
        
        
    }
    public void Move(Vector3 Target)
    {
        
        Navigator.destination = Target;
        Debug.Log("Going to target " + Target.ToString() );
    }
    public void Attack(GameObject _Target)
    {
        if (Target == null) Target = _Target;
        if (!attacking&&Target!=null) {StartCoroutine("Fire"); }
        
    }
    public IEnumerator Fire()
    {
        
        attacking = true;
        Debug.Log("Boom");
        yield return new WaitForSeconds(3f);
        attacking = false;

        if(Target!=null)Attack(Target);
    }

    void AddUnitToList()
    {
        Selected.SetActive(true);
        PlayerController.selectedUnits.Add(this);
        Debug.Log(PlayerController.selectedUnits.Count);

    }
    void RemoveUnitFromList()
    {
        PlayerController.selectedUnits.Remove(this);
        Selected.SetActive(false);
        Debug.Log(PlayerController.selectedUnits.Count);
    }

    public bool IsWithinSelectionBounds()
    {
        if (!PlayerController.isSelecting)
            return false;

        var camera = Camera.main;
        var viewportBounds =
            PlayerController.GetViewportBounds(camera, PlayerController.mousePosition1, Input.mousePosition);

        return viewportBounds.Contains(
            camera.WorldToViewportPoint(transform.position));
    }
}
