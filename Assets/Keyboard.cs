using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    const string upperRowString = "qwertyuiop";
    const string middleRowString = "asdfghjkl";
    const string bottomRowString = "zxcvbnm";

    [SerializeField]
    private GameObject _keyPrefab;
    [SerializeField]
    private Transform[] _rows;

    private void Start()
    {
        var upperRowKeys = upperRowString.ToCharArray();
        var middleRowKeys = middleRowString.ToCharArray();
        var bottomRowKeys = bottomRowString.ToCharArray();

        for (int i = 0; i < upperRowKeys.Length; i++)
        {
            var keyPrefab = Instantiate(_keyPrefab, _rows[0].transform, false);
            Key key = keyPrefab.GetComponent<Key>();
            key.SetLetter(upperRowKeys[i]);
        }
        for (int i = 0; i < middleRowKeys.Length; i++)
        {
            var keyPrefab = Instantiate(_keyPrefab, _rows[1].transform, false);
            Key key = keyPrefab.GetComponent<Key>();
            key.SetLetter(middleRowKeys[i]);
        }
        for (int i = 0; i < bottomRowKeys.Length; i++)
        {
            var keyPrefab = Instantiate(_keyPrefab, _rows[2].transform, false);
            Key key = keyPrefab.GetComponent<Key>();
            key.SetLetter(bottomRowKeys[i]);
        }
    }
}
