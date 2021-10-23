using Assets;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro TextBox;

    void Start()
    {
        TextBox.text = "Your final \n score: \n" + SceneTransitionStorage.TotalScore;
    }
}
