using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
    //Inspector visible properties
    public string Name;
    public float Speed;
    public float AttackRange;
    public string Owner;
    public GameObject BulletType;
    //public float value;

    [HideInInspector]
    public GameObject Selected;
    public GameObject Target;
   // bool isSelected;
    NavMeshAgent Navigator;
    bool attacking;
    public bool targetInRange;

    


    void Start()
    {
        //StartCoroutine("Fire");
        targetInRange = false;
        attacking = false;
        Selected.SetActive(false);
        Navigator = GetComponent<NavMeshAgent>();
        

        Navigator.speed = Speed;
    }
	void Update()
    {
        
        if (IsWithinSelectionBounds()) UnitListControll();
        if(Input.GetMouseButton(0)&&Owner== PlayerController._PlayerName)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity)) { UnitListControll(); }
        }

        if (Input.GetMouseButton(1) && Owner != PlayerController._PlayerName)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity)){ SetMeAsTarget(); }

            
        }
    }
 /*   void OnMouseDown()
    {
        UnitListControll();
        
       
    }*/
    void UnitListControll()
    {
       

            if (!(PlayerController.selectedUnits.Contains(this))&&PlayerController._PlayerName==Owner)
            {
                AddUnitToList();
            }
        
        
    }
    public void SetFacing()
    {
        transform.LookAt(Target.transform.position);
    }
    public void Attack()
    {
        Debug.Log("Yaaaahoooo!");
        if (targetInRange) StartCoroutine("Fire");
        else { Move(Target.transform.position); }
    }
    Quaternion setRotation()
    {
        float a = Target.transform.position.z - transform.position.z;
        if (a < 0) a *= -1;
        float b = Target.transform.position.x - transform.position.x;
        if (b < 0) b *= -1;
        float c = Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2));
        float spinValue = (a / c)*90f;//sinus
       
        Debug.Log(spinValue);
        return Quaternion.Euler(transform.rotation.x, transform.rotation.y - spinValue, transform.rotation.z);
    }
    public IEnumerator Fire()
    {
        Quaternion rot = setRotation();
        //Vector3 rot = new Vector3(transform.rotation.x, transform.rotation.y + 45, transform.rotation.z);
        Vector3 pos = new Vector3(transform.position.x,
            transform.position.y,
            transform.position.z);

        Instantiate(BulletType, pos, rot);
        yield return new WaitForSeconds(3f);
        SetFacing();
        StartCoroutine("Fire");


    }
    public void Move(Vector3 Target)
    {
        
        Navigator.destination = Target;
        Debug.Log("Going to target " + Target.ToString() );
    }
    public void StopIfTargetInRange()
    {
        Navigator.Stop();
    }
    void SetMeAsTarget()
    {
      
        PlayerController._Target=gameObject;
        Debug.Log("Target is "+Name);
        PlayerController.AttackOrder(gameObject);
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
