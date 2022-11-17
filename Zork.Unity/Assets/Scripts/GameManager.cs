using Newtonsoft.Json;
using UnityEngine;
using Zork.Common;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [Header("HeaderTexts")]
    [SerializeField] TextMeshProUGUI LocationText;
    [SerializeField] TextMeshProUGUI MovesText;
    [SerializeField] TextMeshProUGUI ScoreText;

    [Header("Input&Output")]
    [SerializeField] UnityInputService InputService;
    [SerializeField] UnityOutputService OutputService;

    Game _game;

    void Awake()
    {
        TextAsset gameJson = Resources.Load<TextAsset>("GameJson");
        _game = JsonConvert.DeserializeObject<Game>(gameJson.text);

        _game.Player.LocationChanged += Player_LocationChanged;
        _game.Player.PlayerMoved += Player_MovesChanged;
        _game.Player.ScoreChanged += Player_ScoreChanged;

        ReadyDefaultValues();
        _game.Run(InputService, OutputService);
    }

    void Player_LocationChanged(object sender, Room location)
    {
        LocationText.text = location.Name;
    }

    void Player_MovesChanged(object sender, int moves)
    {
        MovesText.text = "Moves: " + moves.ToString();
    }

    void Player_ScoreChanged(object sender, int score)
    {
        ScoreText.text = "Score: " + score.ToString();
    }

    void ReadyDefaultValues()
    {
        MovesText.text = "Moves: " + _game.Player.Moves.ToString();
        ScoreText.text = "Score: " + _game.Player.Score.ToString();
    }

    private void Start()
    {
        InputService.SetFocus();
    }
    void Update()
    {
        LocationText.text = _game.Player.CurrentRoom.Name;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            InputService.ProcessInput();
            InputService.SetFocus();
        }

        if (_game.IsRunning == false)
        {
#if UNITY_EDITOR

            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
