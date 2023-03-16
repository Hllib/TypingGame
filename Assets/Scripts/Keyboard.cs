using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    const string upperRowString = "qwertyuiop";
    const string middleRowString = "asdfghjkl";
    const string bottomRowString = "zxcvbnm";

    [SerializeField]
    private GameObject _keyPrefab;
    [SerializeField]
    private Transform[] _keyboardRows;

    private List<Key> _keyboardKeys;
    Dictionary<string, bool> _wordsDict;

    private char _userInput;

    private List<Letter> _currentWordLetters;

    private void Start()
    {
        InitFields();
        CreateKeyboard();
        CreateWordsDictionary("words");
    }

    private void InitFields()
    {
        _keyboardKeys = new List<Key>();
        _wordsDict = new Dictionary<string, bool>();
        _currentWordLetters = new List<Letter>();
        _userInput = '\0';
    }

    private void Update()
    {
        if (Input.inputString != string.Empty)
        {
            _userInput = Input.inputString[0];
        }

        bool isWordFinished = _currentWordLetters.All(letter => letter.WasPrinted == true);
        if (isWordFinished)
        {
            LoadNextWord();
        }

        if (Input.anyKeyDown && _userInput != '\0')
        {
            Key pressedKey = _keyboardKeys.Find(key => key.Name == _userInput);

            if (pressedKey != null)
            {
                StartCoroutine(ShowKeyDown(pressedKey));
            }

            CheckKeyDownResult();
        }
    }

    private void CheckKeyDownResult()
    {
        for (int i = 0; i < _currentWordLetters.Count; i++)
        {
            if (_currentWordLetters[i].WasPrinted == false)
            {
                if (_userInput == _currentWordLetters[i].Character)
                {
                    _currentWordLetters[i] = new Letter(_currentWordLetters[i].Character, true);
                    WordPrinter.Instance.IndicateKeyCorrectHit(i, true);
                    break;
                }
                else
                {
                    WordPrinter.Instance.IndicateKeyCorrectHit(i, false);
                    break;
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
            var keyPrefab = Instantiate(_keyPrefab, _keyboardRows[rowIndex].transform, false);
            Key key = keyPrefab.GetComponent<Key>();
            key.AssignLetter(letters[i]);

            _keyboardKeys.Add(key);
        }
    }

    IEnumerator ShowKeyDown(Key key)
    {
        key.gameObject.transform.localScale += new Vector3(10.2f, 10.2f, 1f);
        yield return new WaitForSeconds(0.09f);
        key.gameObject.transform.localScale -= new Vector3(10.2f, 10.2f, 1f);
    }

    public void CreateWordsDictionary(string filename)
    {
        _wordsDict.Clear();
        string readFromFilePath = Application.streamingAssetsPath + "/" + filename + ".txt";
        List<string> lines = File.ReadAllLines(readFromFilePath).ToList();

        foreach (var line in lines)
        {
            _wordsDict.Add(line, false);
        }
    }

    public void LoadNextWord()
    {
        if (_currentWordLetters != null)
        {
            _currentWordLetters.Clear();
        }

        var anyWordsLeft = _wordsDict.Any(word => word.Value == false);
        if (anyWordsLeft)
        {
            foreach (KeyValuePair<string, bool> word in _wordsDict)
            {
                if (word.Value == false)
                {
                    for (int i = 0; i < word.Key.Length; i++)
                    {
                        _currentWordLetters.Add(new Letter(word.Key[i], false));
                    }

                    _wordsDict[word.Key] = true;
                    WordPrinter.Instance.AssignWord(word.Key);

                    break;
                }
            }
        }
        else
        {
            CreateWordsDictionary("words");
        }

    }
}
