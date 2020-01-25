using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fuzzy_Fall : MonoBehaviour
{
    public GameObject Elliot;

    public InputField MaxHeight;
    public InputField Height;

    public GameObject ScreenOptions;
    public GameObject ScreenHp;
    
    void Start()
    {
        ScreenOptions.SetActive(true);
        ScreenHp.SetActive(false);
    }

    public void SendValues(){
        if(Height.text == "" || MaxHeight.text == ""){
            Debug.Log("Todos os campos devem ser preenchidos!");
            return;
        }

        float InputHeight = float.Parse(Height.text);
        float InputMaxHeight = float.Parse(MaxHeight.text);

        Elliot.GetComponent<Elliot_Script>().Height = InputHeight;

        Elliot.GetComponent<Elliot_Script>().Damage_Curve.AddKey(0, 0);
        Elliot.GetComponent<Elliot_Script>().Damage_Curve.AddKey(InputMaxHeight, 100);

        transform.position = new Vector3(transform.position.x, InputHeight, transform.position.z);

        ScreenOptions.SetActive(false);
        ScreenHp.SetActive(true);
    }

    public void restartCurrentScene(){
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
     }
}
