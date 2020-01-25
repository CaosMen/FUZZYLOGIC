using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class Player_Script : MonoBehaviour
{
    [Header("Game Object's")]
    public GameObject Planet;
    private GameObject Marker;
    public string Marker_Name;

    [Header("Curves")]
    public AnimationCurve Curve_Speed;
    public AnimationCurve Curve_Side;

    [Header("Float Variables")]
    public float Distance;
    public float Side;

    [Header("Text")]
    public Text Distance_Text;
    public Text Side_Text;
    private const string Label_Distance = "DISTANCE: {0}";
    private const string Label_Side = "SIDE: {0}";

    // Gravity things
    private float Gravity = 980;
    private float DistancetoGround;
    bool OnGround = false;
    Vector3 Groundnormal;
    private Rigidbody Rigid_B;
 
    void Start()
    {
        Rigid_B = GetComponent<Rigidbody>();
        Rigid_B.freezeRotation = true;
    }

    void Update()
    {
        Marker = GameObject.Find(Marker_Name);

        if(Marker != null){
            Distance = SphericalDistance(transform.position, Marker.transform.position);

            Side = Vector3.Dot(transform.right.normalized, Marker.transform.position.normalized);

            rotate_car(Curve_Side.Evaluate(Side), Mathf.Abs(Side));

            move_car(Curve_Speed.Evaluate(Distance));
        } else {
            Distance = 0;
            Side = 0;
        }

        Distance_Text.text = string.Format(Label_Distance, Mathf.Round(Distance));
        Side_Text.text = string.Format(Label_Side, ((Mathf.Round(Side * 100f)) / 100f));
    }

    private void FixedUpdate() {
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(transform.position, -transform.forward, out hit, 10)) {
            DistancetoGround = hit.distance;
            Groundnormal = hit.normal;
 
            if (DistancetoGround <= 0.2f){
                OnGround = true;
            } else {
                OnGround = false;
            }
        }
 
        Vector3 gravDirection = (transform.position - Planet.transform.position).normalized;
 
        if (OnGround == false){
            Rigid_B.AddForce(gravDirection * - Gravity);
        }
 
        Quaternion toRotation = Quaternion.FromToRotation(transform.forward, Groundnormal) * transform.rotation;
        transform.rotation = toRotation;
    }
 
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform != Planet.transform && collision.tag != Marker_Name) {
            Planet = collision.transform.gameObject;
 
            Vector3 gravDirection = (transform.position - Planet.transform.position).normalized;
 
            Quaternion toRotation = Quaternion.FromToRotation(transform.forward, gravDirection) * transform.rotation;
            transform.rotation = toRotation;
 
            Rigid_B.velocity = Vector3.zero;
            Rigid_B.AddForce(gravDirection * Gravity);
        }
    }

    float SphericalDistance(Vector3 Car_Pos, Vector3 Marker_Pos)
    {
        return Mathf.Acos(Vector3.Dot(Car_Pos.normalized, Marker_Pos.normalized))*100;
    }

    private void rotate_car(float value_side, float side){
        Transform from = transform;
        Transform to = transform;

        to.Rotate(0, 0, Mathf.Clamp((60 / side), 0, 480)  * Time.deltaTime * value_side);

        transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, Time.deltaTime);
    }

    private void move_car(float speed){
        transform.Translate(0, -1 * Time.deltaTime * speed, 0);
    }
}