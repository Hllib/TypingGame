using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    private const string UpperRowString = "qwertyuiop";
    private const string MiddleRowString = "asdfghjkl";
    private const string BottomRowString = "zxcvbnm";
    private string _firstFileName = "words0";

    [SerializeField] private PlayerCar _playerCar;
    [SerializeField] private GameObject _keyPrefab;
    [SerializeField] private Transform[] _keyboardRows;

    private WordsGenerator _wordGenerator;
    private List<Key> _keyboardKeys;
    private char _userInput;

    private int _localCorrectHits;
    private int _currentCorrectHits;
    private int _totalHits;
    private float _currentTime;

    private bool _isFirstWordLoaded = false;

    private void Start()
    {
        InitFields();
        CreateKeyboard();

        _wordGenerator.CreateWordsDictionary(_firstFileName);
    }

    private void InitFields()
    {
        _wordGenerator = new WordsGenerator();
        _keyboardKeys = new List<Key>();
        _userInput = '\0';
    }

    private void UpdateCurrentStats()
    {
        _currentCorrectHits += _localCorrectHits;
        _currentTime = Time.timeSinceLevelLoad / 60;
    }

    private void CheckWordCompletion()
    {
        if (_wordGenerator.currentWordLetters.All(letter => letter.WasPrinted == true))
        {
            if (_isFirstWordLoaded)
            {
                UpdateCurrentStats();
                GameManager.Instance.UpdateStats(_currentCorrectHits, _totalHits, _currentTime);
                _localCorrectHits = 0;
            }
            else
            {
                _isFirstWordLoaded = true;
            }
            _wordGenerator.LoadNextWord();
        }
    }

    private void Update()
    {
        if (Input.inputString != string.Empty)
            _userInput = Input.inputString[0];

        CheckWordCompletion();  

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
                    _totalHits += 1;
                    _localCorrectHits += 1;
                    AudioManager.Instance.PlayOneShot(FMODEvents.Instance.correctHit, Vector3.zero);
                    break;
                }
                else
                {
                    _wordGenerator.currentWordLetters[i] = new Letter(_wordGenerator.currentWordLetters[i].Character, true);
                    WordPrinter.Instance.IndicateKeyCorrectHit(i, false);
                    AudioManager.Instance.PlayOneShot(FMODEvents.Instance.missedHit, Vector3.zero);
                    _totalHits += 1;
                    break;
                }
            }
        }
    }

    private void CreateKeyboard()
    {
        List<char[]> keyboardsLetters = new List<char[]>()
        {
            UpperRowString.ToLower().ToCharArray(),
            MiddleRowString.ToLower().ToCharArray(),
            BottomRowString.ToLower().ToCharArray()
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