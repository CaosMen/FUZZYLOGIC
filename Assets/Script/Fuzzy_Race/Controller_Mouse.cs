using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Mouse : MonoBehaviour
{
    public GameObject Marker;
    public GameObject Car;
    private RaycastHit RayHit;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RayHit)){
                if (!GameObject.Find(Car.GetComponent<Player_Script>().Marker_Name) && (Vector3.Distance(Car.transform.position, RayHit.point) > 15)){
                    GameObject Instance = Instantiate(Marker, RayHit.point, Quaternion.LookRotation(RayHit.normal));
                    Instance.name = Car.GetComponent<Player_Script>().Marker_Name;
                }
            }
        }
    }
}
