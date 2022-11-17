using UnityEngine;
using Zork.Common;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnityOutputService : MonoBehaviour, IOutputService
{
    [SerializeField] TextMeshProUGUI TextLinePrefab;
    [SerializeField] Image NewLinePrefab;
    [SerializeField] Transform Content;
    public void Write(object obj) => ParseAndWriteLine(obj.ToString());
    public void Write(string message) => ParseAndWriteLine(message);
    public void WriteLine(object obj) => ParseAndWriteLine(obj.ToString());
    public void WriteLine(string message) => ParseAndWriteLine(message);

    void ParseAndWriteLine(string message)
    {
        var textLine = Instantiate(TextLinePrefab, Content.transform);
        textLine.text = message;
        _entries.Add(textLine.gameObject);
    }

    static List<GameObject> _entries = new List<GameObject>();
}
