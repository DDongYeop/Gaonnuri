using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenTransition : MonoSingleton<ScreenTransition>
{
    [SerializeField] private float _transitionTime;
    private Material _screenTransitionMat;
    private readonly int _materialValue = Shader.PropertyToID("_value");

    private void Start()
    {
        _screenTransitionMat = transform.GetChild(0).GetComponent<MeshRenderer>().material;
    }

    public void SceneChange(int num, GameState nextState)
    {
        StartCoroutine(SceneChangeCo(() => SceneManager.LoadScene(num), nextState));
    }
    
    public void SceneChange(string str, GameState nextState)
    {
        StartCoroutine(SceneChangeCo(() => SceneManager.LoadScene(str), nextState));
    }

    private IEnumerator SceneChangeCo(Action sceneChange, GameState nextState)
    {
        GameManager.Instance.StateChange(nextState, _transitionTime * 2 + .5f);
        StartCoroutine(ScreenTransitionCo(1.5f, 0, 1, sceneChange));
        yield return new WaitForSeconds(_transitionTime + .5f);
        StartCoroutine(ScreenTransitionCo(0, 1.5f, 1, null));
    }

    private IEnumerator ScreenTransitionCo(float start, float end, int transitionTime, Action act = null)
    {
        float currentTime = 0;
        while (currentTime <= _transitionTime)
        {
            yield return null;
            currentTime += Time.deltaTime;
            float time = currentTime / _transitionTime;
            _screenTransitionMat.SetFloat(_materialValue, Mathf.Lerp(start, end, time));
        }
        
        act?.Invoke();
    }
}
