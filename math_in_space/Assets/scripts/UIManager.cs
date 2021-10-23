using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro m_ScoreBoard;

    public GameObject Prefab_Astroid;

    public GameObject asteroidSound;
    public GameObject lifeLostSound;
    public GameObject correctEffect;
    public GameObject explosionEffect;
    public GameObject explosionEffectCorrect;
    public GameObject hearts;

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
                //
                Object.Instantiate(correctEffect, new Vector3(), Quaternion.identity);
                for (int i = 0; i < 30; i++)
                {
                    Object.Instantiate(
                        explosionEffectCorrect, astroid.transform.position, Quaternion.identity);
                }
                //
                correctAnswers++;
                answerStreak++;
                removeObjects.Add(obj);
                difficulty++;
                ResetGame();
            }
            else if (astroid.HasBeenHit)
            {
                // Effects.
                Object.Instantiate(lifeLostSound, new Vector3(), Quaternion.identity);
                for(int i = 0; i < 30; i++)
                {
                    Object.Instantiate(explosionEffect, astroid.transform.position, Quaternion.identity);
                    //Object.Instantiate(explosionEffect, new Vector3(0,0,0), Quaternion.identity);
                }

                //
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

        //
        hearts.transform.GetChild(0).gameObject.SetActive(false);
        hearts.transform.GetChild(1).gameObject.SetActive(false);
        hearts.transform.GetChild(2).gameObject.SetActive(false);
        if (Life == 1) {
            int a = 0;
        }
        if (Life-1 >= 0 && Life-1 <= 2)
        {
            hearts.transform.GetChild(Life - 1).gameObject.SetActive(true);
        }
    }

    public Vector3 getAsteroidPosition()
    {
        Vector3 lowerLeft, upperRight;
        Utilities.getBoundaries(out lowerLeft, out upperRight);
        //Vector3 position = new Vector3(Random.Range(lowerLeft.x, upperRight.x), 0, 0);
        Vector3 position = new Vector3(
            Random.Range(lowerLeft.x, upperRight.x),
            0,
            Random.Range(lowerLeft.z, upperRight.z));

        return position;
    }

    public void AddScoreObjects()
    {
        (var question, var answer) = RandomMultiplicationQuestion();
        AnswerToQuesiton = answer;
        m_ScoreBoard.text = question;
        //Vector3 lowerLeft, upperRight;
        //Utilities.getBoundaries(out lowerLeft, out upperRight);
        //Vector3 position = new Vector3(Random.Range(lowerLeft.x, upperRight.x), 0, 0);
        //position = new Vector3(0, 0, 0);
        Vector3 position = getAsteroidPosition();
        var firstAstroid = Instantiate(Prefab_Astroid,
                position,
                Quaternion.identity);

        var firstComponent = firstAstroid.GetComponent<Astroid>();
        firstComponent.TextBox.text = AnswerToQuesiton.ToString();
        firstComponent.IsScoreObject = true;

        instansiatedObjects.Add(firstAstroid);

    }

    public void AddRandomAstroids()
    {
        // Play asteroid sound.
        Object.Instantiate(asteroidSound, new Vector3(), Quaternion.identity);

        //
        for (int i = 0; i < Random.Range(1, difficulty * 2); i++)
        {
            Vector3 position = getAsteroidPosition();
            var gameObject = Instantiate(Prefab_Astroid,
            position,
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
