using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text m_ScoreBoard;

    private Dictionary<int, string> operation = new Dictionary<int, string>()
    {
        { 0, "*" },
        { 1, "+" },
        //{ 3, "/" },
        { 2, "-" }
    };

    private Dictionary<int, int> levels = new Dictionary<int, int>()
    {
        { 0, 10 },
        { 1, 20 },
        { 2, 30 },
        { 3, 40 },
        { 4, 50 },
        { 5, 100 },
        { 6, 200 },
        { 7, 300 },
        { 8, 300 },
        { 9, 300 },
        { 10, 300 }
    };

    protected bool update = true;

    private int questionAnswer = 0;

    private float gameTime;

    private int answerStreak = 0;

    private int correctAnswers = 0;

    public int difficulty = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_ScoreBoard.text = "Multiply 2*2";
        gameTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        if (update)
        {
            (var question, var answer) = RandomMultiplicationQuestion();
            questionAnswer = answer;
            m_ScoreBoard.text = question;
            update = false;
        }

        if (Input.GetKeyDown("space"))
        {
            update = true;
            answerStreak++;
            correctAnswers++;
        }

        //switch (correctAnswers)
        //{
        //    case 10:
        //        difficulty = 1;
        //        break;
        //    case 15:
        //        difficulty = 2;
        //        break;
        //    case 20:
        //        difficulty = 3;
        //        break;
        //}


    }

    public void CheckAnswer(int inputAnswer)
    {
        if (inputAnswer == questionAnswer)
        {
            correctAnswers++;
        }
    }



    private (string, int) RandomMultiplicationQuestion()
    {
        System.Random rand = new System.Random();
        var first = rand.Next(1, 10);
        var second = rand.Next(0, 10);
        var mathOperator = rand.Next(0, difficulty);

        //var answer = first * second;
        var answer = mathOperator switch
        {
            0 => first * second,
            1 => first + second,
            //3 => first / second,
            2 => first - second,
            _ => 0
        };

        return ($"Calculate: {first}{operation[mathOperator]}{second}", answer);

    }

}
