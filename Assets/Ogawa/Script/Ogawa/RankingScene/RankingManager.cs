using UnityEngine;
using System;
using System.IO;

public class RankingManager : MonoBehaviour
{
    public JsonReader jsonReader;
    public NumberImageChange numberImageChange; // NumberImageChangeの参照を追加

    private int[] scoreToShow;
    private string[] nameToShow;

    private void Start()
    {
        //test
        /*string testName = "Murakami";
        int testScore = 1000000;*/

        scoreToShow = new int[5];//初期化
        nameToShow = new string[5];//初期化

        // JsonReaderを使ってランキングデータを読み込む
        PlayerScoresData playerScoresData = ReadJsonFile();
        if (playerScoresData != null)
        {
            // スコアデータを表示する
            DisplayScoreImages(playerScoresData);
            //ランキングのアップデート
            //test
            //UpdateRanking(testName,testScore);
        }
        else
        {
            Debug.LogWarning("Failed to load ranking data!");
        }
    }

    private PlayerScoresData ReadJsonFile()
    {
        string filePath = "Assets/Resources/Ranking.json"; // パスを指定

        if (System.IO.File.Exists(filePath))
        {
            string jsonContent = System.IO.File.ReadAllText(filePath);

            Debug.Log("Loaded JSON data: " + jsonContent);

            PlayerScoresData playerScoresData = JsonUtility.FromJson<PlayerScoresData>(jsonContent);

            if (playerScoresData != null)
            {
                // デバッグログにplayerScoresDataの内容を表示
                //Debug.Log("Loaded player score data: "+jsonContent);
                for (int i = 0; i < playerScoresData.playerScores.Length; i++)
                {
                    //Debug.Log("Player Name: " + playerScoresData.playerScores[i].PlayerName + ", Score: " + playerScoresData.playerScores[i].score);

                }
                return playerScoresData;
            }
            else
            {
                Debug.LogWarning("Failed to parse JSON data!");
                return null;
            }
        }
        else
        {
            Debug.LogWarning("Json file does not exist at path: " + filePath);
            return null;
        }
    }

    // ランキングの更新処理
    public void UpdateRanking(string playerName, int score)
    {
        // 新しいスコアを持つPlayerScoreオブジェクトを作成
        PlayerScore newScore = new PlayerScore();
        newScore.PlayerName = playerName;
        newScore.score = score;

        // ランキングデータを読み込む
        PlayerScoresData playerScoresData = ReadJsonFile();

        if (playerScoresData != null)
        {
            // 新しいスコアが上位5位に入るかどうかを確認
            bool scoreAdded = false;

            // 新しいスコアが上位5位に入る場合の処理
            if (playerScoresData.playerScores.Length < 5)
            {
                // ランキングが5人未満の場合は常に追加
                Array.Resize(ref playerScoresData.playerScores, playerScoresData.playerScores.Length + 1);
                playerScoresData.playerScores[playerScoresData.playerScores.Length - 1] = newScore;
                scoreAdded = true;
            }
            else
            {
                // 新しいスコアが上位5位に入るかチェック
                for (int i = 0; i < playerScoresData.playerScores.Length; i++)
                {
                    if (newScore.score > playerScoresData.playerScores[i].score)
                    {
                        // ランキングに追加
                        InsertScore(playerScoresData.playerScores, newScore, i);
                        scoreAdded = true;
                        break;
                    }
                }
            }

            // 新しいスコアがランキング内に入った場合の処理
            if (scoreAdded)
            {
                // ランキングをソート
                Array.Sort(playerScoresData.playerScores, (x, y) => y.score.CompareTo(x.score));

                // 6番目以降のデータを削除
                if (playerScoresData.playerScores.Length > 5)
                {
                    Array.Resize(ref playerScoresData.playerScores, 5);
                }

                // ランキングを更新してJSONファイルに書き込む
                SaveUpdatedRanking(playerScoresData);

                //gamemanagerにランクインしたことを伝える

            }
        }
        else
        {
            Debug.LogWarning("Failed to load ranking data!");
        }
    }

    // 更新されたランキングをJSONファイルに保存する
    private void SaveUpdatedRanking(PlayerScoresData playerScoresData)
    {
        string filePath = "Assets/Resources/Ranking.json"; // パスを指定

        // JSON形式に変換してファイルに書き込む
        string json = JsonUtility.ToJson(playerScoresData);
        File.WriteAllText(filePath, json);

        Debug.Log("Ranking update");
    }

    private void InsertScore(PlayerScore[] scores, PlayerScore newScore, int index)
    {
        for (int i = scores.Length - 1; i > index; i--)
        {
            scores[i] = scores[i - 1];
        }
        scores[index] = newScore;
    }

    private void DisplayScoreImages(PlayerScoresData playerScoresData)
    {
        if (numberImageChange != null)
        {
            if (playerScoresData != null && playerScoresData.playerScores.Length > 0)
            {

                // 上位５人のスコアを取得し、NumberImageChangeに渡す
                for (int i = 0; i < 5; i++)
                {
                    scoreToShow[i] = playerScoresData.playerScores[i].score;
                    nameToShow[i] = playerScoresData.playerScores[i].PlayerName;
                    Debug.Log($"Rank {i + 1}: Player Name: {nameToShow[i]}, Score: {scoreToShow[i]}");
                    //Debug.Log(nameToShow[i]);
                    numberImageChange.ShowScoreImages(scoreToShow[i], i);
                    numberImageChange.ShowName(nameToShow[i], i);
                }


            }
            else
            {
                Debug.LogWarning("PlayerScoresData is null or empty!");
            }
        }
        else
        {
            Debug.LogWarning("NumberImageChange reference is missing!");
        }
    }
}
