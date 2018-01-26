using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
    public string transmitterTag;

    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

	void Update () {
        CheckLeftMouse();
	}

    private void CheckLeftMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == transmitterTag)
                    hit.transform.GetComponent<Transmitter>().HandleClick();
            }
        }
    }
}
