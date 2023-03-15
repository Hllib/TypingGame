using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private List<Key> _keys;

    private void Start()
    {
        _keys = new List<Key>();
        CreateKeyboard();
    }

    [SerializeField]
    private TextMeshProUGUI _textMeshPro;

    private void Update()
    {
        string input = "";
        if (Input.inputString != string.Empty)
        {
            input = Input.inputString;
            Debug.Log(Input.inputString.ToString());
        }

        if (Input.anyKeyDown && !Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            foreach (var key in _keys)
            {
                if (key.Name == input[0])
                {
                    _textMeshPro.text += key.Name;
                    StartCoroutine(HideKey(key));
                    break;
                }
            }
            if(Input.GetKeyDown(KeyCode.Space)) 
            {
                _textMeshPro.text += " ";
            }
        }

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
            key.AssignLetter(letters[i]);

            _keys.Add(key);
        }
    }

    IEnumerator HideKey(Key key)
    {
        key.gameObject.transform.localScale += new Vector3(10.2f, 10.2f, 1f);
        yield return new WaitForSeconds(0.3f);
        key.gameObject.transform.localScale -= new Vector3(10.2f, 10.2f, 1f);
    }
}
