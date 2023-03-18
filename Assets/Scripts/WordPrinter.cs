using System.Collections.Generic;
using UnityEngine;

public class WordPrinter : MonoBehaviour
{
    private const int MaxLettersInWord = 12;

    [SerializeField]
    private GameObject _letterHolderPrefab;

    private List<GameObject> _letterHolders;
    private List<Key> _printableKeys;

    private static WordPrinter _instance;

    public static WordPrinter Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("WordPrinter is NULL");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _letterHolders = new List<GameObject>();
        _printableKeys = new List<Key>();

        CreateLetterHolders();
    }

    private void CreateLetterHolders()
    {
        for (int i = 0; i < MaxLettersInWord; i++)
        {
            var letter = Instantiate(_letterHolderPrefab, this.transform, false);
            letter.gameObject.SetActive(false);

            _printableKeys.Add(letter.GetComponent<Key>());
            _letterHolders.Add(letter);
        }
    }

    public void IndicateKeyCorrectHit(int index, bool operationState)
    {
        switch(operationState)
        {
            case true: _letterHolders[index].SetActive(false); break;
            case false: break;
        }
    }

    public void AssignWord(string word)
    {
        foreach(var letter in _letterHolders)
        {
            letter.SetActive(false);
        }

        for (int i = 0; i < word.Length; i++)
        {
            foreach (var letter  in _letterHolders)
            {
                if(!letter.activeInHierarchy)
                {
                    Key key = letter.GetComponent<Key>();
                    key.AssignLetter(word[i]);
                    letter.SetActive(true);
                    break;
                }
            }
        }
    }
}
