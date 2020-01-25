using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elliot_Script : MonoBehaviour
{
    public Animator Anim_Elliot;

    public float Life = 100;
    public float Height;
    
    public AnimationCurve Damage_Curve;

    public Text Life_Text;

    public bool Is_Falling;
    public bool Alive;
    private bool ApplyDamage;

    private Rigidbody rigid_b;
    void Start()
    {
        Is_Falling = false;
        Alive = true;
        ApplyDamage = false;

        Life_Text.text = Life.ToString();

        rigid_b = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Life <= 0) Alive = false;
        
        Anim_Elliot.SetBool("Falling", Is_Falling);
        Anim_Elliot.SetBool("Alive", Alive);

        if (rigid_b.velocity.y < -0.1)
        {
            Is_Falling = true;
            ApplyDamage = true;
        }
        else
        {
            if(ApplyDamage){
                Life = Life - Damage_Curve.Evaluate(Height);
                Life_Text.text = Life.ToString();
                ApplyDamage = false;
            }
            Is_Falling = false;
        }
    }
}
