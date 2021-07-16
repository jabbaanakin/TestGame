using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public GameObject GameObjectMethod;
    private ButtonScript buttonScript;
    private int _level;

    private void Start()
    {
        buttonScript = GameObjectMethod.GetComponent<ButtonScript>();
    }

    //Start game with more cells
    public void LevelUp()
    {
        gameObject.SetActive(false);
        _level = ButtonScript._level;
        _level++;
        buttonScript.StartLevel(_level);
        buttonScript.fadeIn();
    }
}
