using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour {
    public string islandTag;

    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

	void Update () {
        CheckLeftMouse();
        CheckRightMouse();
	}

    private void CheckLeftMouse()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == islandTag)
                    hit.transform.GetComponent<Transmitter>().HandleClick(1);
            }
        }
    }
    private void CheckRightMouse()
    {
        if (Input.GetMouseButtonDown(1) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == islandTag)
                    hit.transform.GetComponent<Transmitter>().HandleClick(-1);
            }
        }
    }
}
