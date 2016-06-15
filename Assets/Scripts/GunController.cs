using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {
    public GameObject Bullet;
    Unit Parent;
   
    // Use this for initialization
    void Start () {
        Parent = GetComponentInParent<Unit>();
        StartCoroutine("Fire");

    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public IEnumerator Fire()
    {

        Instantiate(Bullet,transform.position,transform.rotation);
        yield return new WaitForSeconds(3f);
        StartCoroutine("Fire");
        

    }
}
