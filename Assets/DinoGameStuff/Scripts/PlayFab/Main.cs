using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab;
using PlayFab.ClientModels;
using System.Text;
using System;
using System.Collections;

namespace Code
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private Button _getLeaderboardButton;
        [SerializeField] private Button _getLeaderboardAroundPlayerButton;
        [SerializeField] private Button _getPlayerScoreButton;
        [SerializeField] private Button _submitNameButton;
        [SerializeField] private Text _resultsText;
        [SerializeField] private DinoMainManager _dinoMngr;
        [SerializeField] private LoginResult loginRslt;

        private const string LeaderboardScore = "high_score";
        private const string LeaderboardName = "names";

        private string _playerId;

        public PlayFabLogin _playFabLogin;
        private PlayFabUpdatePlayerStatistics _playFabUpdatePlayerStatistics;
        private PlayFabGetLeaderboardAroundPlayer _playFabGetLeaderboardAroundPlayer;
        private PlayFabGetLeaderboard _playFabGetLeaderboard;

        public InputField nameField;

        private void Start()
        {
            AddListeners();
            CreatePlayFabServices();
            DoLogin();
        }

        private void CreatePlayFabServices()
        {
            _playFabLogin = new PlayFabLogin();
            _playFabLogin.OnSuccess += playerId => _playerId = playerId;

            _playFabUpdatePlayerStatistics = new PlayFabUpdatePlayerStatistics();
            _playFabUpdatePlayerStatistics.OnSuccess += Delay;
            

            _playFabGetLeaderboardAroundPlayer = new PlayFabGetLeaderboardAroundPlayer();
            _playFabGetLeaderboardAroundPlayer.OnSuccess += result => _resultsText.text = result;

            _playFabGetLeaderboard = new PlayFabGetLeaderboard();
            _playFabGetLeaderboard.OnSuccess += GetLeaderboardOnSuccess;
            //_playFabGetLeaderboard.OnSuccess += result => _resultsText.text = result;
        }

        void GetLeaderboardOnSuccess(string result)
        {
            //string[] array = result.Split('|');
            Debug.Log(result.Split('|').Length);
            _dinoMngr.ShowScore(result.Split('|'));
        }

        private void DoLogin()
        {
            string ip = string.Empty;
            ip = UnityEngine.Random.Range(0, 1000000000).ToString();
            Debug.Log(ip);
            _playFabLogin.Login(ip);
        }

        private void AddListeners()
        {
            _getLeaderboardButton.onClick.AddListener(() => Delay(""));
            _getLeaderboardAroundPlayerButton.onClick.AddListener(OnGetLeaderboardAroundPlayerButtonPressed);
            _getPlayerScoreButton.onClick.AddListener(OnGetPlayerScoreButtonPressed);
            //_addPlayerScoreButton.onClick.AddListener(OnAddPlayerScoreButtonPressed);
        }
        public void Delay(string empty)
        {
            Debug.Log("Delay");
            StartCoroutine(DelayShowScore());
        }

        IEnumerator DelayShowScore()
        {
            Debug.Log("Incio rutina");
            yield return new WaitForSecondsRealtime(.3f);
            Debug.Log("Continua rutina");
            _playFabGetLeaderboard.GetLeaderboardEntries(0, 5, LeaderboardScore);
            //OnGetLeaderboardButtonPressed();

        }


        public void SubmitName(InputField namefield)
        {
            if (namefield.text != string.Empty)
            {
                _playFabLogin.SubmitNameButton(namefield);
            }
        }

        public bool CheckName()
        {
            return _playFabLogin.CheckNamed();
        }

        public void OnAddPlayerScoreButtonPressed(int score)
        {
            _playFabUpdatePlayerStatistics
               .UpdatePlayerStatistics(LeaderboardScore, score);
        }

        private void OnGetPlayerScoreButtonPressed()
        {
            _playFabGetLeaderboardAroundPlayer
               .GetLeaderboardAroundPlayer(_playerId, 1, LeaderboardScore);
        }

        private void OnGetLeaderboardAroundPlayerButtonPressed()
        {
            _playFabGetLeaderboardAroundPlayer
               .GetLeaderboardAroundPlayer(_playerId, 3, LeaderboardScore);
        }

        private void OnGetLeaderboardButtonPressed()
        {
            Debug.Log("Checkea Score");
            _playFabGetLeaderboard.GetLeaderboardEntries(0, 5, LeaderboardScore);

        }
    }
}
