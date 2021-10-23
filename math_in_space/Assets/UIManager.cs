using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro m_ScoreBoard;

    public GameObject Prefab_Astroid;

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

    private int AnswerToQuesiton = 0;

    private float gameTime;

    private int answerStreak = 0;

    private int correctAnswers = 0;

    public int difficulty = 0;

    private bool init = true;

    public int Life = 3;

    private List<GameObject> instansiatedObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        m_ScoreBoard.text = "Multiply 2*2";
        gameTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {

        if (Life == 0)
        {
            SceneManager.LoadScene("End_Scene", LoadSceneMode.Single);
        }

        if (init)
        {
            init = false;
            AddScoreObjects();
            AddRandomAstroids();

        }

        var objs = GameObject.FindGameObjectsWithTag("Astroid");

        var removeObjects = new List<GameObject>();
        foreach (var obj in objs)
        {
            if (obj == null)
                continue;

            var astroid = obj.GetComponent<Astroid>();
            if (astroid.IsScoreObject && astroid.HasBeenHit)
            {
                correctAnswers++;
                answerStreak++;
                removeObjects.Add(obj);
                difficulty++;
                ResetGame();
            }
            else if (astroid.HasBeenHit)
            {
                removeObjects.Add(obj);
                Life--;
                answerStreak = 0;
            }
        }

        foreach (var item in removeObjects)
        {
            instansiatedObjects.Remove(item);
            Destroy(item);
        }

        if (Input.GetKeyDown("space"))
        {
            answerStreak++;
            correctAnswers++;
        }
    }


    public void AddScoreObjects()
    {
        (var question, var answer) = RandomMultiplicationQuestion();
        AnswerToQuesiton = answer;
        m_ScoreBoard.text = question;
        Vector3 lowerLeft, upperRight;
        Utilities.getBoundaries(out lowerLeft, out upperRight);
        var firstAstroid = Instantiate(Prefab_Astroid,
                new Vector3(Random.Range(lowerLeft.x*0.4f, upperRight.x*0.4f), 0, 0),
                Quaternion.identity);

        var firstComponent = firstAstroid.GetComponent<Astroid>();
        firstComponent.TextBox.text = AnswerToQuesiton.ToString();
        firstComponent.IsScoreObject = true;

        instansiatedObjects.Add(firstAstroid);

    }

    public void AddRandomAstroids()
    {
        for (int i = 0; i < Random.Range(1, difficulty * 2); i++)
        {
            var gameObject = Instantiate(Prefab_Astroid,
            new Vector3(Random.Range(0, 10), 0, 0),
            Quaternion.identity);

            var astroidComponent = gameObject.GetComponent<Astroid>();
            astroidComponent.TextBox.text = Random.Range(1, AnswerToQuesiton).ToString();
            astroidComponent.IsScoreObject = false;
            instansiatedObjects.Add(gameObject);

        }
    }

    public void ResetGame()
    {
        foreach (var obj in instansiatedObjects)
        {
            Destroy(obj);
        }

        AddScoreObjects();
        AddRandomAstroids();
    }

    private (string, int) RandomMultiplicationQuestion()
    {
        System.Random rand = new System.Random();
        var first = rand.Next(1, 10);
        var second = rand.Next(0, 10);
        var mathOperator = rand.Next(0, difficulty % 3);

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
