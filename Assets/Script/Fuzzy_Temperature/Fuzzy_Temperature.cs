using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fuzzy_Temperature : MonoBehaviour
{
    private const string labelText = "{0} TRUE";
    public AnimationCurve Hot;
    public AnimationCurve Warm;
    public AnimationCurve Cold;
    public AnimationCurve Freezing;

    public InputField TemperatureInput;

    public Text HotLabel;
    public Text WarmLabel;
    public Text ColdLabel;
    public Text FreezingLabel;

    private double HotVal = 0f;
    private double WarmVal = 0f;
    private double ColdVal = 0f;
    private double FreezingVal = 0f;
    
    void Start()
    {
        TemperatureInput.characterLimit = 5;
        SetLabel();
    }

    public void EvaluteStatements(){
        if(string.IsNullOrEmpty(TemperatureInput.text)){
            return;
        }

        float InputValue = float.Parse(TemperatureInput.text);

        if(!(InputValue >= -40 && InputValue <= 40)){
            Debug.Log("A Temperatura deve estar entre -40 - 40!");
            return;
        }

        HotVal = System.Math.Round(Hot.Evaluate(InputValue), 2);
        WarmVal = System.Math.Round(Warm.Evaluate(InputValue), 2);
        ColdVal = System.Math.Round(Cold.Evaluate(InputValue), 2);
        FreezingVal = System.Math.Round(Freezing.Evaluate(InputValue), 2);

        SetLabel();
    }

    private void SetLabel(){
        HotLabel.text = string.Format(labelText, HotVal);
        WarmLabel.text = string.Format(labelText, WarmVal);
        ColdLabel.text = string.Format(labelText, ColdVal);
        FreezingLabel.text = string.Format(labelText, FreezingVal);
    }
}
