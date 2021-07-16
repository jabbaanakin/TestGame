using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;
using DG.Tweening;

public class ButtonScript : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public GameObject[] levels;
    private GameObject[] _buttons;
    public GameObject _levelCompleteCanvas;
    public Canvas canvas;
    public Image panel;
    public GameObject _nextLevelButton;

    private char[] _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789".ToCharArray();
    private char c;
    public static int _level = 1;
    private int _correctCellIndex;
    private char[] _checkWords = new char[9];

    void Start()
    {
        canvas = GetComponent<Canvas>();
        _level = 1;
        Text.canvasRenderer.SetAlpha(0.0f);
        _buttons = GameObject.FindGameObjectsWithTag("Button");
        StartLevel(_level);
        Text.CrossFadeAlpha(1f, 2f, false);
        StartCoroutine(SlowScale());

    }

    //Event, when we click the button
    public void Clicked()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name;
        if (buttonName == c.ToString())
        {
            if (_level == 3)
            {
                _nextLevelButton.SetActive(false);
            }
            _levelCompleteCanvas.SetActive(true);
            fadeOut();
        }
        else
            EventSystem.current.currentSelectedGameObject.transform.DOShakePosition(1.5f, strength: new Vector3(5, 0, 0), vibrato: 5, randomness: 5, snapping: false, fadeOut: true);
    }
    //Fill the cells and pick random letter to find
    public void StartLevel(int _levelCount)
    {
        _level = _levelCount;
        c = _alphabet[Random.Range(0, _alphabet.Length)];
        Text.text = "Find " + c; //Fill the string with letter, we have to find

        for (int i = 0; i < _levelCount; i++)
            levels[i].SetActive(true);
            
        for (int i = _levelCount; i < 3; i++)
            levels[i].SetActive(false);
        for (int i = 0; i < _levelCount * 3; i++)
        {
            char x = _alphabet[Random.Range(0, _alphabet.Length)];
            if (_checkWords.Contains(x) || x == c) //check if char exists in game already
            {
                while(_checkWords.Contains(x) || x == c)
                    x = _alphabet[Random.Range(0, _alphabet.Length)];
            }
            _checkWords[i] = x;
            _buttons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(x.ToString()); //Fill the answers
        }
        _correctCellIndex = Random.Range(0, _levelCount * 3); //pick place, where we fill correct answer
        _buttons[_correctCellIndex].GetComponent<Image>().sprite = Resources.Load<Sprite>(c.ToString()); //Filling the cells with correct answer
    }

    //Fade in effect for game canvas
    public void fadeIn()
    {
        for (int i = 0; i < 9; i++)
            _buttons[i].GetComponent<Button>().interactable = true;
        panel.GetComponent<Image>().CrossFadeAlpha(1f, 1f, false);
    }

    //Fade out effect for game canvas
    public void fadeOut()
    {
        for (int i = 0; i < 9; i++)
            _buttons[i].GetComponent<Button>().interactable = false;
        panel.GetComponent<Image>().CrossFadeAlpha(0.2f, 1f, false);
    }

    //Bounce effect for cells
    IEnumerator SlowScale() 
    {
        for (int i = 0; i < 3; i++)
        {
            for (float q = 1f; q < 2f; q += .1f)
            {
                _buttons[i].transform.localScale = new Vector3(q, q, q);
                yield return new WaitForSeconds(.01f);
            }
            for (float q = 2f; q > 1f; q -= .1f)
            {
                _buttons[i].transform.localScale = new Vector3(q, q, q);
                yield return new WaitForSeconds(.01f);
            }
        }
    }
}
