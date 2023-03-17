using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class WordsGenerator
{
    private Dictionary<string, bool> _wordsDictionary = new Dictionary<string, bool>();
    public List<Letter> currentWordLetters { get; private set; } = new List<Letter>();

    public void CreateWordsDictionary(string filename)
    {
        _wordsDictionary.Clear();
        string readFromFilePath = Application.streamingAssetsPath + "/" + filename + ".txt";
        List<string> lines = File.ReadAllLines(readFromFilePath).ToList();

        foreach (var line in lines)
        {
            _wordsDictionary.Add(line, false);
        }
    }

    public void LoadNextWord()
    {
        currentWordLetters.Clear();

        var nextWord = _wordsDictionary.FirstOrDefault(word => word.Value == false);
        if (nextWord.Key != null)
        {
            for (int i = 0; i < nextWord.Key.Length; i++)
            {
                currentWordLetters.Add(new Letter(nextWord.Key[i], false));
            }
            _wordsDictionary[nextWord.Key] = true;
            WordPrinter.Instance.AssignWord(nextWord.Key);
        }
        else
        {
            CreateWordsDictionary("words0");
        }
    }
}
