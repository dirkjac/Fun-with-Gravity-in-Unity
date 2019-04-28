using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class spawns the orbiting objects.
/// </summary>
public class SpawnScript : MonoBehaviour {

	[SerializeField] private GameObject planetPrefab = null;
	[SerializeField] private GameObject sun = null;
	[SerializeField] private GameObject sunBlue = null;
	[SerializeField] private GameObject sunGreen = null;
	[SerializeField] private GameObject centerObject = null;

	private Vector3 sunVelocity = Vector3.zero;
	private Vector3 sunBlueVelocity = Vector3.zero;
	private Vector3 sunGreenVelocity = Vector3.zero;

    [Tooltip("Defines the number of orbiting objects to spawn. If you have performance problems, decrease this number.")]
    [Range(1, 500)]
	[SerializeField] private int numberOfPlanets = 1;

    [Tooltip("The radius around the center where orbiting objects are spawned.")]
    [Range(10, 10000)]
	[SerializeField] private float radius = 20f;

    [Tooltip("The minimum mass of orbiting objects. Play around with it to see how the mass influence the orbit behaviour of objects.")]
    [Range(0, 10000)]
	[SerializeField] private float minPlanetMass = 500f;

    [Tooltip("The maximum mass of orbiting objects. Play around with it to see how the mass influence the orbit behaviour of objects.")]
    [Range(10000, 100000)]
	[SerializeField] private float maxPlanetMass = 50000f;

    [Tooltip("The delay between spawning new orbiting objects at game start.")]
    [Range(0, 10)]
	[SerializeField] private float spawnDelay = 1f;

    [Tooltip("The maximum size of the orbiting objects. This value does not influence the mass of an orbiting object.")]
    [Range(1f, 20f)]
	[SerializeField] private float maxPlanetSize = 1f;

    private List<GameObject> planets = new List<GameObject>();
	private Transform cameraTransform;

    /// <summary>
    /// Spawn orbiting objects.
    /// </summary>
    void Start () {
		cameraTransform = Camera.main.transform;

		StartCoroutine(SpawnPlanets());

        sun.GetComponent<Rigidbody>().velocity = sunVelocity;
        sunBlue.GetComponent<Rigidbody>().velocity = sunBlueVelocity;
        sunGreen.GetComponent<Rigidbody>().velocity = sunGreenVelocity;
    }

	/// <summary>
	/// Spawn new objects with a delay.
	/// </summary>
	/// <returns></returns>
    private IEnumerator SpawnPlanets() {
        for (int i = 0; i < numberOfPlanets; i++) {
            GameObject planet = Instantiate(planetPrefab);
            float x = -radius / 2f + Random.value * radius;
            float y = -radius / 2f + Random.value * radius;
            float z = -radius / 2f + Random.value * radius;
            Vector3 position = new Vector3(x, y, z);
            planet.transform.position = position;
            float scale = 1f + Random.value * maxPlanetSize;
            planet.transform.localScale = new Vector3(scale, scale, scale);

            float m = minPlanetMass + Random.value * (maxPlanetMass - minPlanetMass);
            Rigidbody prb = planet.GetComponent<Rigidbody>();
            prb.mass = m;
            prb.velocity = new Vector3(Random.value * 15f, Random.value * 30f, Random.value * 5f);
            planet.GetComponent<Renderer>().material.color = new Color(0.5f + Random.value / 2, 0.5f + Random.value / 2, 0.5f + Random.value / 2);

            planets.Add(planet);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    /// <summary>
    /// Spawn new planet at a random position with a random velocity.
    /// </summary>
    public void SpawnNewPanet() {
        GameObject planet = Instantiate(planetPrefab);
        float x = -radius / 2f + Random.value * radius;
        float y = -radius / 2f + Random.value * radius;
        float z = -radius / 2f + Random.value * radius;
        Vector3 position = new Vector3(x, y, z);
        planet.transform.position = position;
        float scale = 1f + Random.value * maxPlanetSize;
        planet.transform.localScale = new Vector3(scale, scale, scale);

        float m = minPlanetMass + Random.value * (maxPlanetMass - minPlanetMass);
        Rigidbody prb = planet.GetComponent<Rigidbody>();
        if (prb) {
            prb.mass = m;
            prb.velocity = new Vector3(Random.value * 15f, Random.value * 30f, Random.value * 5f);
        }

        planet.GetComponent<Renderer>().material.color = new Color(0.5f + Random.value/2, 0.5f + Random.value / 2, 0.5f + Random.value / 2);

        planets.Add(planet);
    }

    /// <summary>
    /// Let camera always look at the center object.
    /// </summary>
    private void Update() {
        if (centerObject != null) {
            cameraTransform.LookAt(centerObject.transform);
        }
    }
}
