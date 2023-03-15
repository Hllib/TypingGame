using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textLetter;
    
    public void SetLetter(char letter)
    {
        _textLetter.text = letter.ToString();
    }
}
