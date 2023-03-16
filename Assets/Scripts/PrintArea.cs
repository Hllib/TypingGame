using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrintArea : MonoBehaviour
{
    [SerializeField]
    private GameObject _letterHolderPrefab;

    private const int maxLetters = 12;
    public List<GameObject> letters;
    public List<Key> keys;

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
        letters = new List<GameObject>();
        keys = new List<Key>();

        for (int i = 0; i < maxLetters; i++)
        {
            var letter = Instantiate(_letterHolderPrefab, this.transform, false);
            letter.gameObject.SetActive(false);
            keys.Add(letter.GetComponent<Key>());
            letters.Add(letter);
        }
    }

    public void IndicatePrintLetter(int index, bool operationState)
    {
        switch(operationState)
        {
            case true: letters[index].SetActive(false); break;
            case false: break;
        }
    }

    public void AssignWord(string word)
    {
        if(word.Length > maxLetters)
        {
            Debug.LogError("word length exceeds max allowed amount");
        }

        foreach(var letter in letters)
        {
            letter.SetActive(false);
        }

        for (int i = 0; i < word.Length; i++)
        {
            foreach (var letter  in letters)
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
