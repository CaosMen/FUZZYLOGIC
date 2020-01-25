using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Load : MonoBehaviour
{
    public void load_leve(int leveid){
        if(leveid == -1){
            Application.Quit();
            return;
        }
        
        SceneManager.LoadScene(leveid);
    }
}
