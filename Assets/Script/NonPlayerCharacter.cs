using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4.0f;

    public GameObject dialogBox;

    float timerDisplay;

    public GameObject dlgTxtProGameObject;

    TextMeshProUGUI _tmTxtBox;

    int _currentPage = 0;

    int _totalPages;
    // Start is called before the first frame update
    void Start()
    {
        
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;
        _tmTxtBox = dlgTxtProGameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _totalPages = _tmTxtBox.textInfo.pageCount;
        if (timerDisplay > 0)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (_currentPage < _totalPages)
                {
                    _currentPage++;
                }
                else
                {
                    _currentPage = 1;
                }
                _tmTxtBox.pageToDisplay = _currentPage;
            }
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
            }
            //减少资源使用
        }
    }

    public void DisplayDialog() 
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }
}
