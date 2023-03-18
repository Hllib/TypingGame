using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class WordsGenerator
{
    private Dictionary<string, bool> _wordsDictionary;
    private Dictionary<string, bool> _wordsFiles;

    public List<Letter> currentWordLetters { get; private set; }

    public WordsGenerator()
    {
        _wordsDictionary = new Dictionary<string, bool>();
        currentWordLetters = new List<Letter>();

        _wordsFiles = new Dictionary<string, bool>()
        {
            {"words0", true},
            {"words1", false },
            {"words2", false },
            {"words3", false },
            {"words4", false },
            {"words5", false },
        };
    }

    public void CreateWordsDictionary(string filename)
    {
        _wordsDictionary.Clear();
        string readFromFilePath = Application.streamingAssetsPath + "/Easy/" + filename + ".txt";
        List<string> lines = File.ReadAllLines(readFromFilePath).ToList();

        foreach (var line in lines)
        {
            _wordsDictionary.Add(line, false);
        }
    }

    private void LoadNextWordsFile()
    {
        var nextWordsFile = _wordsFiles.FirstOrDefault(file => file.Value == false);
        if (nextWordsFile.Key != null)
        {
            _wordsFiles[nextWordsFile.Key] = true;
            CreateWordsDictionary(nextWordsFile.Key);
        }
        else
        {
            ReassignDict(_wordsFiles);
        }
    }

    private void ReassignDict(Dictionary<string, bool> dict)
    {
        foreach (var key in dict.Keys.ToList())
        {
            dict[key] = false;
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
            LoadNextWordsFile();
        }
    }
}
