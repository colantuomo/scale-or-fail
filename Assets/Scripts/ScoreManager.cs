using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int levelScore;
    public int totalScore;

    private void Start()
    {
        SubscribeEvents();
        levelScore = 0;
        totalScore = 0;
    }

    private void SubscribeEvents()
    {
        GameEvents.Singleton.OnUpdateLevelScore += OnUpdateLevelScore;
    }

    void OnUpdateLevelScore(ClientSO client, string typedCode){
        int integerScore = 0;
        int.TryParse(typedCode, out integerScore);
        if (client.Fruit.Code == integerScore)
        {
            levelScore += 1;
        }
    }

    void OnUpdateTotalScore()
    {
        totalScore += levelScore;
        levelScore = 0;
    }
}
