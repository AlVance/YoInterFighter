using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab;
using PlayFab.ClientModels;
using System.Text;
using System;

namespace Code
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private Button _getLeaderboardButton;
        [SerializeField] private Button _getLeaderboardAroundPlayerButton;
        [SerializeField] private Button _getPlayerScoreButton;
        [SerializeField] private Button _addPlayerScoreButton;
        [SerializeField] private Text _resultsText;

        private const string LeaderboardScore = "high_score";
        private const string LeaderboardName = "names";

        private string _playerId;

        private PlayFabLogin _playFabLogin;
        private PlayFabUpdatePlayerStatistics _playFabUpdatePlayerStatistics;
        private PlayFabGetLeaderboardAroundPlayer _playFabGetLeaderboardAroundPlayer;
        private PlayFabGetLeaderboard _playFabGetLeaderboard;

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

            _playFabGetLeaderboardAroundPlayer = new PlayFabGetLeaderboardAroundPlayer();
            _playFabGetLeaderboardAroundPlayer.OnSuccess += result => _resultsText.text = result;

            _playFabGetLeaderboard = new PlayFabGetLeaderboard();
            _playFabGetLeaderboard.OnSuccess += result => _resultsText.text = result;
        }

        private void DoLogin()
        {
            _playFabLogin.Login();
        }

        private void AddListeners()
        {
            _getLeaderboardButton.onClick.AddListener(OnGetLeaderboardButtonPressed);
            _getLeaderboardAroundPlayerButton.onClick.AddListener(OnGetLeaderboardAroundPlayerButtonPressed);
            _getPlayerScoreButton.onClick.AddListener(OnGetPlayerScoreButtonPressed);
            _addPlayerScoreButton.onClick.AddListener(OnAddPlayerScoreButtonPressed);
        }

        private void OnAddPlayerScoreButtonPressed()
        {
            _playFabUpdatePlayerStatistics
               .UpdatePlayerStatistics(LeaderboardScore, 100);
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
            string name = "pepito";
            Debug.Log(CodifyString(name));
            Debug.Log(DecodifyInt(CodifyString(name)));

            OnDisplayNameUpdate();

            //_playFabGetLeaderboard.GetLeaderboardEntries(0, 5, LeaderboardScore);¡

        }
        void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
        {
            Debug.Log(result);
        }

        public int CodifyString(string stringToNum)
        {
            int code = 0;
            string totalCode = string.Empty;
            char[] charArray = stringToNum.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                totalCode += CharToNum(charArray[i]);
                Debug.Log(Convert.ToInt32(totalCode));
            }
            code = Convert.ToInt32(totalCode);
            return code;
        }

        public string DecodifyInt(int intToStr)
        {
            string encoder = string.Empty;

            string lastStr;
            string intStr = intToStr.ToString();
            for (int i = 0; i < intStr.Length; i=i + 2)
            {
                lastStr = encoder.Substring(i, 2);
                //encoder += NumToChar(Convert.ToInt32(lastStr));
                Debug.Log(encoder);
            }
            return encoder;
        }

        public int CharToNum(char charToNum)
        {
            int num = 0;
            int num2 = 0;

            num = char.ToUpper(charToNum) - 64;
            Debug.Log("Num Char " + num);
            num2 = (int)charToNum % 32;
            Debug.Log("Num Char 2 " +num2);
            return num;
        }


        /*public int CharToNum(char charToNum)
        {
            int num = 0;

            switch (charToNum)
            {
                case 'a':
                    num = 10;
                    break;
                case 'b':
                    num = 11;
                    break;
                case 'c':
                    num = 12;
                    break;
                case 'd':
                    num = 13;
                    break;
                case 'e':
                    num = 14;
                    break;
                case 'f':
                    num = 15;
                    break;
                case 'g':
                    num = 16;
                    break;
                case 'h':
                    num = 17;
                    break;
                case 'i':
                    num = 18;
                    break;
                case 'j':
                    num = 19;
                    break;
                case 'k':
                    num = 20;
                    break;
                case 'l':
                    num = 21;
                    break;
                case 'm':
                    num = 22;
                    break;
                case 'n':
                    num = 23;
                    break;
                case 'ñ':
                    num = 24;
                    break;
                case 'o':
                    num = 25;
                    break;
                case 'p':
                    num = 26;
                    break;
                case 'q':
                    num = 27;
                    break;
                case 'r':
                    num = 28;
                    break;
                case 's':
                    num = 29;
                    break;
                case 't':
                    num = 30;
                    break;
                case 'u':
                    num = 31;
                    break;
                case 'v':
                    num = 32;
                    break;
                case 'w':
                    num = 33;
                    break;
                case 'x':
                    num = 34;
                    break;
                case 'y':
                    num = 35;
                    break;
                case 'z':
                    num = 36;
                    break;
            }
            return num;
        }
        
        public string NumToChar(int charToNum)
        {
            string str = string.Empty;

            switch (charToNum)
            {
                case 10:
                    str = "a";
                    break;
                case 11:
                    str = "b";
                    break;
                case 12:
                    str = "c";
                    break;
                case 13:
                    str = "d";
                    break;
                case 14:
                    str = "e";
                    break;
                case 15:
                    str = "f";
                    break;
                case 16:
                    str = "g";
                    break;
                case 17:
                    str = "h";
                    break;
                case 18:
                    str = "i";
                    break;
                case 19:
                    str = "j";
                    break;
                case 20:
                    str = "k";
                    break;
                case 21:
                    str = "l";
                    break;
                case 22:
                    str = "m";
                    break;
                case 23:
                    str = "n";
                    break;
                case 24:
                    str = "ñ";
                    break;
                case 25:
                    str = "o";
                    break;
                case 26:
                    str = "p";
                    break;
                case 27:
                    str = "q";
                    break;
                case 28:
                    str = "r";
                    break;
                case 29:
                    str = "s";
                    break;
                case 30:
                    str = "t";
                    break;
                case 31:
                    str = "u";
                    break;
                case 32:
                    str = "v";
                    break;
                case 33:
                    str = "w";
                    break;
                case 34:
                    str = "x";
                    break;
                case 35:
                    str = "y";
                    break;
                case 36:
                    str = "z";
                    break;
            }
            return str;
        }

        */
    }
}
