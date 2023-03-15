using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class Keyboard : MonoBehaviour
{
    const string upperRowString = "qwertyuiop";
    const string middleRowString = "asdfghjkl";
    const string bottomRowString = "zxcvbnm";

    [SerializeField]
    private GameObject _keyPrefab;
    [SerializeField]
    private Transform[] _rows;

    private void Start()
    {
        CreateKeyboard();
    }

    private void CreateKeyboard()
    {
        List<char[]> keyboardsLetters = new List<char[]>()
        {
            upperRowString.ToCharArray(),
            middleRowString.ToCharArray(),
            bottomRowString.ToCharArray()
        };

        for (int i = 0; i < keyboardsLetters.Count; i++)
        {
            CreateKeyRow(keyboardsLetters[i], i);
        }
    }

    private void CreateKeyRow(char[] letters, int rowIndex)
    {
        for (int i = 0; i < letters.Length; i++)
        {
            var keyPrefab = Instantiate(_keyPrefab, _rows[rowIndex].transform, false);
            Key key = keyPrefab.GetComponent<Key>();
            key.SetLetter(letters[i]);
        }
    }
}
