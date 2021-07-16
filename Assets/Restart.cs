using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    public GameObject GetGameObject;
    private ButtonScript buttonScript = new ButtonScript();
    private int _level;

    private void Start()
    {
        buttonScript = GetGameObject.GetComponent<ButtonScript>();
    }

    //Restarting game with same cells
    public void RestartLevel()
    {
        gameObject.SetActive(false);
        _level = ButtonScript._level;
        buttonScript.StartLevel(_level);
        buttonScript.fadeIn();
    }
}
