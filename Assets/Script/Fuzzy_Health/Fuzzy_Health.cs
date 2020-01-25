using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fuzzy_Health : MonoBehaviour
{
    private const string labelText = "{0} TRUE";
    public AnimationCurve Healthy;
    public AnimationCurve Hurt;
    public AnimationCurve Critical;

    public InputField HealthInput;

    public Text HealthyLabel;
    public Text HurtLabel;
    public Text CriticalLabel;

    private float HealthyVal = 0f;
    private float HurtVal = 0f;
    private float CriticalVal = 0f;
    
    void Start()
    {
        HealthInput.characterLimit = 5;
        SetLabel();
    }

    public void EvaluteStatements(){
        if(string.IsNullOrEmpty(HealthInput.text)){
            return;
        }

        float InputValue = float.Parse(HealthInput.text);

        if(!(InputValue >= 0 && InputValue <= 100)){
            Debug.Log("A vida deve estar entre 0 - 100!");
            return;
        }

        HealthyVal = Healthy.Evaluate(InputValue);
        HurtVal = Hurt.Evaluate(InputValue);
        CriticalVal = Critical.Evaluate(InputValue);

        SetLabel();
    }

    private void SetLabel(){
        HealthyLabel.text = string.Format(labelText, HealthyVal);
        HurtLabel.text = string.Format(labelText, HurtVal);
        CriticalLabel.text = string.Format(labelText, CriticalVal);
    }
}
