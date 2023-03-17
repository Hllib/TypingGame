using TMPro;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textLetter;

    public char Name { get; set; }
    
    public void AssignLetter(char letter)
    {
        _textLetter.text = letter.ToString();
        Name = letter;
    }
}
