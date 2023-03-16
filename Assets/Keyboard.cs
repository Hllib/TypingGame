using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    const string upperRowString = "qwertyuiop";
    const string middleRowString = "asdfghjkl";
    const string bottomRowString = "zxcvbnm";

    [SerializeField]
    private GameObject _keyPrefab;
    [SerializeField]
    private Transform[] _rows;

    [SerializeField]
    private TextMeshProUGUI _wordTMP;

    private List<Key> _keys;
    Dictionary<string, bool> _words;
    private List<Letter> _currentWord;

    private void Start()
    {
        _keys = new List<Key>();
        _words = new Dictionary<string, bool>();
        _currentWord = new List<Letter>();

        CreateKeyboard();
        CreateWordsDictionary();
        LoadNextWord();
    }


    private void Update()
    {
        char input = '\0';
        if (Input.inputString != string.Empty)
        {
            input = Input.inputString.ToLower()[0];
        }

        var isWordFinished = _currentWord.All(letter => letter.State == true);
        if (isWordFinished)
        {
            LoadNextWord();
        }

        if (Input.anyKeyDown && input != '\0')
        {
            Key key = _keys.Find(key => key.Name == input);
            if (key != null)
            {
                StartCoroutine(IndicateClick(key));
            }

            for (int i = 0; i < _currentWord.Count; i++)
            {
                if (_currentWord[i].State == false)
                {
                    _currentWord[i] = new Letter(_currentWord[i].LetterSymbol, true);

                    if (input == _currentWord[i].LetterSymbol)
                    {
                        Debug.Log("Got it");
                        break;
                    }
                    else
                    {
                        Debug.Log("Missed it");
                        break;
                    }
                }
            }
        }
    }

    private void CreateKeyboard()
    {
        List<char[]> keyboardsLetters = new List<char[]>()
        {
            upperRowString.ToLower().ToCharArray(),
            middleRowString.ToLower().ToCharArray(),
            bottomRowString.ToLower().ToCharArray()
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

    IEnumerator IndicateClick(Key key)
    {
        key.gameObject.transform.localScale += new Vector3(10.2f, 10.2f, 1f);
        yield return new WaitForSeconds(0.09f);
        key.gameObject.transform.localScale -= new Vector3(10.2f, 10.2f, 1f);
    }

    public void CreateWordsDictionary()
    {
        string readFromFilePath = Application.streamingAssetsPath + "/" + "words" + ".txt";
        List<string> lines = File.ReadAllLines(readFromFilePath).ToList();

        foreach (var line in lines)
        {
            _words.Add(line, false);
        }
    }

    public void LoadNextWord()
    {
        if (_currentWord != null)
            _currentWord.Clear();

        var anyWordsLeft = _words.Any(word => word.Value == false);
        if (anyWordsLeft)
        {
            foreach (KeyValuePair<string, bool> word in _words)
            {
                if (word.Value == false)
                {
                    for (int i = 0; i < word.Key.Length; i++)
                    {
                        _currentWord.Add(new Letter(word.Key[i], false));
                    }

                    _words[word.Key] = true;
                    _wordTMP.text = word.Key;

                    break;
                }
            }
        }
        else
        {
            Debug.Log("Level finished!!!");
        }

    }
}
