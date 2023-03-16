using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintArea : MonoBehaviour
{
    [SerializeField]
    private GameObject _letterHolderPrefab;

    private const int maxLetters = 12;
    private List<GameObject> _letters;

    private static PrintArea _instance;
    public static PrintArea Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("PrintArea is NULL");
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
        _letters = new List<GameObject>();
        for (int i = 0; i < maxLetters; i++)
        {
            var letter = Instantiate(_letterHolderPrefab, this.transform, false);
            letter.gameObject.SetActive(false);
            _letters.Add(letter);
        }
    }

    public void AssignWord(string word)
    {
        if(word.Length > maxLetters)
        {
            Debug.LogError("word length exceeds max allowed amount");
        }

        foreach(var letter in _letters)
        {
            letter.SetActive(false);
        }

        for (int i = 0; i < word.Length; i++)
        {
            foreach (var letter  in _letters)
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
