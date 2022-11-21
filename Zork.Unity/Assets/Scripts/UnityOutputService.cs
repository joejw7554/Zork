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
    [SerializeField] int maxLines = 10;
    public void Write(object obj) => ParseAndWriteLine(obj.ToString());
    public void Write(string message) => ParseAndWriteLine(message);
    public void WriteLine(object obj) => ParseAndWriteLine(obj.ToString());
    public void WriteLine(string message) => ParseAndWriteLine(message);
    void ParseAndWriteLine(string message)
    {
        var textLine = Instantiate(TextLinePrefab, Content.transform);
        textLine.text = message;
        _entries.Enqueue(textLine.gameObject);

        if (_entries.Count > maxLines)
        {
            for (int i = 0; i < maxLines; i++)
            {
                _entries.Dequeue();
                //Destroy(Content.transform.GetChild(i));
            }
        }

    }
    static Queue<GameObject> _entries = new Queue<GameObject>();
}
