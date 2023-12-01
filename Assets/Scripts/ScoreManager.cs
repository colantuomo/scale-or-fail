using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float levelScore;
    private float _maxClientScore = 500f;
    private float _minClientScore = 0f;
    private float _minTimeToReduceScore = 10f;
    private float _maxTimeToReduceScore = 100f;
    private float _rangeToReduceScore;

    private void Start()
    {
        SubscribeEvents();
        levelScore = 0f;
        PlayerPrefs.SetFloat("score", levelScore);
        _rangeToReduceScore = _maxTimeToReduceScore - _minTimeToReduceScore;
    }

    private void SubscribeEvents()
    {
        GameEvents.Singleton.OnUpdateLevelScore += OnUpdateLevelScore;
    }

    void OnUpdateLevelScore(float timeSpentOnLine)
    {
        float timeSpentLoosingPoints = Mathf.Max(0, timeSpentOnLine - _minTimeToReduceScore);
        levelScore += Mathf.Lerp(_maxClientScore, _minClientScore, timeSpentLoosingPoints / _rangeToReduceScore);
        PlayerPrefs.SetFloat("score", levelScore);
    }
}
