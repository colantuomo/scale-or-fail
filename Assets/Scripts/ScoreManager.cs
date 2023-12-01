using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float levelScore;
    public float totalScore;
    private float _maxClientScore = 500f;
    private float _minClientScore = 0f;
    private float _minTimeToReduceScore = 10f;
    private float _maxTimeToReduceScore = 100f;
    private float _rangeToReduceScore;

    [SerializeField]
    private Lifebar _lifebar;

    private void Start()
    {
        SubscribeEvents();
        levelScore = 0f;
        totalScore = 0f;
        _rangeToReduceScore = _maxTimeToReduceScore - _minTimeToReduceScore;
    }

    private void SubscribeEvents()
    {
        GameEvents.Singleton.OnUpdateLevelScore += OnUpdateLevelScore;
        GameEvents.Singleton.OnFailCodeTyping += OnFailCodeTyping;
    }

    private void OnFailCodeTyping()
    {
        print("Fail Code!");
        _lifebar.DecreaseByOne();
    }

    void OnUpdateLevelScore(ClientSO client, string typedCode, float timeSpentOnLine){
        int integerCode = 0;
        int.TryParse(typedCode, out integerCode);
        if (client.Fruit.Code == integerCode)
        {
            float timeSpentLoosingPoints = Mathf.Max(0, timeSpentOnLine - _minTimeToReduceScore);
            levelScore += Mathf.Lerp(_maxClientScore, _minClientScore, timeSpentLoosingPoints / _rangeToReduceScore);
        }
    }

    void OnUpdateTotalScore()
    {
        totalScore += levelScore;
        levelScore = 0f;
    }
}
