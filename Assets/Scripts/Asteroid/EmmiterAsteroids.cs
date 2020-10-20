using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class EmmiterAsteroids : SingletonMonoBehaviour<EmmiterAsteroids>
{
    [Serializable]
    public class AsteroidData
    {
        public Asteroid prefab;
        public MinMax   scale    = new MinMax(1, 1);
        public MinMax   velocity = new MinMax(0.1f, 5f);
        public MinMax   angularVelocity = new MinMax(-5f, 5f);
    }

    [SerializeField] private AsteroidData[] asteroidPrefabs;
    [Space]
    [SerializeField] private int            maxAsteroidsNum = 30;
    [SerializeField] private MinMax         delayData = new MinMax() { min = 2f, max = 5f };
    [SerializeField] private Collider2D     liveArea;

    private List<Asteroid> asteroids = new List<Asteroid>();
    private float timeout;

    private void Awake()
    {
        Asteroid.OnDestroyEvent += OnDestroyAsteroid;
        Asteroid.OnKillEvent += OnKillAsteroid;
    }

    private void OnDestroy()
    {
        Asteroid.OnDestroyEvent -= OnDestroyAsteroid;
        Asteroid.OnKillEvent -= OnKillAsteroid;
    }

    private void OnDestroyAsteroid(Asteroid asteroid)
    {
        asteroids.Remove(asteroid);
    }

    private void Update()
    {
        timeout -= Time.deltaTime;
        if (timeout <= 0f)
        {
            MakeRandomAsteroid();
            timeout = delayData.Random();
        }
    }

    private void MakeRandomAsteroid()
    {
        var asteroidData = asteroidPrefabs.Random();

        var scale = Vector3.one * asteroidData.scale.Random();

        var bounds = liveArea.bounds;
        var pos = new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
        if (Random.value < 0.5f)
        {
            pos.x = Random.value < 0.5f ? bounds.min.x : bounds.max.x;
        }
        else
        {
            pos.y = Random.value < 0.5f ? bounds.min.y : bounds.max.y;
        }

        Vector2 target;
        target.x = Random.Range(bounds.min.x / 2, bounds.max.x / 2);
        target.y = Random.Range(bounds.min.y / 2, bounds.max.y / 2);

        var dir = target - pos;
        var velocity = dir * asteroidData.velocity.Random();

        var angularVelocity = asteroidData.angularVelocity.Random();

        MakeAsteroid(
            asteroidData.prefab,
            pos,
            scale,
            velocity,
            angularVelocity
        );
    }

    private void MakeAsteroid(Asteroid prefab, Vector2 position, Vector3 scale, Vector2 velocity, float angularVelocity)
    {
        if (asteroids.Count >= maxAsteroidsNum)
        {
            return;
        }

        var asteroid = GameObject.Instantiate(prefab);
        asteroids.Add(asteroid);

        asteroid.transform.position = position;
        asteroid.transform.localScale = scale;

        var rigidbody = asteroid.GetComponent<Rigidbody2D>();
        rigidbody.mass *= scale.magnitude;
        rigidbody.velocity = velocity;
        rigidbody.angularVelocity = angularVelocity;
    }
    
    private void OnKillAsteroid(Asteroid asteroid, Damage lastDamage)
    {
        var scale = asteroid.transform.localScale.x;
        var halfScale = scale / 2f;

        var prefabs = Array.FindAll(asteroidPrefabs, _ => halfScale > _.scale.min);
        if (prefabs.Length > 0)
        {
            var p = (Vector2)asteroid.transform.position - lastDamage.ContactPoint.point;

            foreach (var angle in new[] { 90f, -90f })
            {
                var data = prefabs.Random();
                var newScale = Random.Range(data.scale.min, halfScale) * Vector3.one;
                var pos = Quaternion.Euler(0f, 0f, angle) * p.normalized * halfScale + asteroid.transform.position;
                var velocity = Quaternion.Euler(0f, 0f, Random.Range(-45f, 45f)) * pos.normalized * asteroid.Velocity.magnitude;// * Random.value;

                MakeAsteroid(data.prefab, pos, newScale, velocity, data.angularVelocity.Random());

            }
        }
    }

    public void Clear()
    {
        asteroids.ForEach(_ => Destroy(_.gameObject));
        asteroids.Clear();
        timeout = 0f;
    }

}
