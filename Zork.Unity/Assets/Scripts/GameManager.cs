using Newtonsoft.Json;
using UnityEngine;
using Zork.Common;
public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        TextAsset gameJson = Resources.Load<TextAsset>("GameJson");
        _game = JsonConvert.DeserializeObject<Game>(gameJson.text);
    }
    private Game _game;
}
