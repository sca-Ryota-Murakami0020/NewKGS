using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalScoreManager : MonoBehaviour
{
    [Header("残機"),SerializeField] 
    private int _remain;
    //スコア
    private int _score = 0;
    //獲得金額
    private int _money = 0;

    public int Remain
    {
        get { return _remain;}
    }

    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //残機、スコア、金額の更新
    public void ChangeScores(int updateRemain, int updateScore, int updateMoney)
    {
        _remain = updateRemain;
        _score = updateScore;
        _money = updateMoney;
    }
}
