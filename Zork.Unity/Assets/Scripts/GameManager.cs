using Newtonsoft.Json;
using UnityEngine;
using Zork.Common;
using TMPro;
public class GameManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI LocationText;
    [SerializeField] TextMeshProUGUI MovesText;
    [SerializeField] TextMeshProUGUI ScoreText;


    [SerializeField] UnityInputService Input;
    [SerializeField] UnityOutputService Output;
    private void Awake()
    {
        TextAsset gameJson = Resources.Load<TextAsset>("GameJson");
        _game = JsonConvert.DeserializeObject<Game>(gameJson.text);
        _game.Run(Input, Output);

    }
    private Game _game;
}
