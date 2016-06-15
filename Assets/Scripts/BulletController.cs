using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
    public int BulletSpeed;
    public Vector3 Target;
	// Use this for initialization
	void Start () {
        StartCoroutine("Destroy");
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(BulletSpeed/10f, 0, 0);
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
