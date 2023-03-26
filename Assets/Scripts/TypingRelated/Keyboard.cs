using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    const string upperRowString = "qwertyuiop";
    const string middleRowString = "asdfghjkl";
    const string bottomRowString = "zxcvbnm";
    string firstFileName = "words0";

    [SerializeField]
    private GameObject _keyPrefab;
    [SerializeField]
    private Transform[] _keyboardRows;

    private WordsGenerator _wordGenerator;
    private List<Key> _keyboardKeys;
    private char _userInput;

    private int _correctHits = 0;

    bool isFirstWordLoaded = false;

    private void Start()
    {
        InitFields();
        CreateKeyboard();

        _wordGenerator.CreateWordsDictionary(firstFileName);
    }

    private void InitFields()
    {
        _wordGenerator = new WordsGenerator();
        _keyboardKeys = new List<Key>();
        _userInput = '\0';
    }

    private void Update()
    {
        if (Input.inputString != string.Empty)
            _userInput = Input.inputString[0];

        if (_wordGenerator.currentWordLetters.All(letter => letter.WasPrinted == true))
        {
            if (isFirstWordLoaded)
            {
                GameManager.Instance.MovePlayerCar(_correctHits);
                _correctHits = 0;
            }
            else
            {
                isFirstWordLoaded = true;
            }
            _wordGenerator.LoadNextWord();
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
        for (int i = 0; i < _wordGenerator.currentWordLetters.Count; i++)
        {
            if (_wordGenerator.currentWordLetters[i].WasPrinted == false)
            {
                if (_userInput == _wordGenerator.currentWordLetters[i].Character)
                {
                    _wordGenerator.currentWordLetters[i] = new Letter(_wordGenerator.currentWordLetters[i].Character, true);
                    WordPrinter.Instance.IndicateKeyCorrectHit(i, true);
                    _correctHits += 1;
                    break;
                }
                else
                {
                    _wordGenerator.currentWordLetters[i] = new Letter(_wordGenerator.currentWordLetters[i].Character, true);
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
}