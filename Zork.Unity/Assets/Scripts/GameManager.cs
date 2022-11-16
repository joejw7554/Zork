using Newtonsoft.Json;
using UnityEngine;
using Zork.Common;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Header")]
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
        _game.Run(InputService, OutputService);
    }

    void Player_LocationChanged(object sender,Room location)
    {
        LocationText.text = location.Name;
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
