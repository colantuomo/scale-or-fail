using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float levelScore;
    public float totalScore;
    public float totalClientsServed;
    public float clientsCorrectlyServed;
    public float precision;
    public float avgClientServeTime;
    public float totalClientServeTime;
    private float _maxClientScore = 500f;
    private float _minClientScore = 0f;
    private float _minTimeToReduceScore = 2f;
    private float _maxTimeToReduceScore = 20f;
    private float _maxPrecisionScore = 10000f;
    private float _maxAvgTimeScore = 10000f;
    private float _rangeToReduceScore;

    [SerializeField]
    private Lifebar _lifebar;

    private void Start()
    {
        SubscribeEvents();
        levelScore = 0f;
        precision = 0f;
        totalClientsServed = 0;
        clientsCorrectlyServed = 0;
        avgClientServeTime = 0f;
        totalClientServeTime = 0f;
        PlayerPrefs.SetFloat("score", levelScore);
        PlayerPrefs.SetFloat("precision", precision);
        PlayerPrefs.SetFloat("avg", avgClientServeTime);
        PlayerPrefs.SetFloat("totalScore", totalScore);
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

    void OnUpdateLevelStatistics(float timeSpentOnLine, bool isCodeCorrect)
    {
        totalClientsServed++;
        if (isCodeCorrect)
        {
            clientsCorrectlyServed++;
        }
        totalClientServeTime += timeSpentOnLine;
        avgClientServeTime = totalClientServeTime / totalClientsServed;
        precision = (clientsCorrectlyServed / totalClientsServed) * 100;

        // Calculate score based on precision and avg serve time
        float precisionScore = Mathf.Lerp(_maxPrecisionScore, 0, precision / 100);
        float avgTimeScore = Mathf.Lerp(_maxAvgTimeScore, 0, avgClientServeTime / _maxTimeToReduceScore);

        totalScore = levelScore + precisionScore + avgTimeScore;

        PlayerPrefs.SetFloat("precision", precision);
        PlayerPrefs.SetFloat("avg", avgClientServeTime);
        PlayerPrefs.SetFloat("totalScore", totalScore);
    }

    void OnUpdateLevelScore(float timeSpentOnLine)
    {
        float timeSpentLoosingPoints = Mathf.Max(0, timeSpentOnLine - _minTimeToReduceScore);
        levelScore += Mathf.Lerp(_maxClientScore, _minClientScore, timeSpentLoosingPoints / _rangeToReduceScore);
        PlayerPrefs.SetFloat("score", levelScore);
    }
}
