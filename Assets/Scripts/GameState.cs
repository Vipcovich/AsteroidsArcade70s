using UnityEngine;
using System.Collections;
using System;

public class GameState : SingletonMonoBehaviour<GameState>
{
    public enum State
    {
        OnStart,
        Gameplay,
        Result,
    }

    [SerializeField] private State      starState = State.OnStart;
    [SerializeField] private Fighter    fighterPrefab;
    [SerializeField] private Transform  fighterPlace;
    [SerializeField] private int        lifes = 3;
    [SerializeField] private float      delayDeathKill = 3f;

    private State state;

    public int Score { get; private set; }
    public int Lifes { get; private set; }

    private void Start()
    {
        SetState(starState);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Fighter.OnKillEvent += OnKillPlayer;
        Asteroid.OnKillEvent += OnKillAsteroid;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        Fighter.OnKillEvent -= OnKillPlayer;
        Asteroid.OnKillEvent -= OnKillAsteroid;
    }

    public void SetState(State newState)
    {
        StopAllCoroutines();

        state = newState;
        switch (state)
        {
            case State.OnStart:
                OnStart();
                break;

            case State.Gameplay:
                OnGameplay();
                break;

            case State.Result:
                OnResult();
                break;

            default:
                Debug.LogErrorFormat("Don't support state = {0}", state);
                break;
        }
    }

    private void OnStart()
    {
        Score = 0;
        Lifes = lifes;

        Time.timeScale = 0f;

        Menu.instance.ShowScreen<HudScreen>();
        Menu.instance.ShowScreen<StartScreen>(true);

        EmmiterAsteroids.instance.Clear();
        PlaceFighter();
    }

    private void OnGameplay()
    {
        Time.timeScale = 1f;
        Menu.instance.ShowScreen<HudScreen>();
    }

    private void OnResult()
    {
        Time.timeScale = 0f;
        Menu.instance.ShowScreen<ResultScreen>();
    }

    private void OnKillPlayer(Fighter fighter, Damage lastDamage)
    {
        Lifes -= 1;
        if (Lifes > 0)
        {
            InvokeAfterDelay(() => PlaceFighter(), delayDeathKill);
        }
        else
        {
            InvokeAfterDelay(() => SetState(State.Result), delayDeathKill);
        }
    }

    private void PlaceFighter()
    {
        EmmiterAsteroids.instance.Clear();

        GameObject.Instantiate(fighterPrefab, fighterPlace.position, fighterPlace.rotation);
    }

    private void OnKillAsteroid(Asteroid asteroid, Damage damage)
    {
        if (damage.OwnerIsPlayer)
        {
            Score += asteroid.Score;
        }
    }

    private Coroutine InvokeAfterDelay(Action action, float delay)
    {
        return StartCoroutine(_InvokeAfterDelay(action, delay));
    }

    private IEnumerator _InvokeAfterDelay(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }
}
