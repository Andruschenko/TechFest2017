using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoSomething : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {

        anim = this.gameObject.GetComponent<Animator>();
        StartCoroutine(Delayed());
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator Delayed()
    {
        yield return new  WaitForSeconds(10.0f);
        anim.SetTrigger("DoSomething");
    }
}
