using UnityEngine;
using System.Collections;
using TowerScripts;

public class NozzleBase : MonoBehaviour {


	public TowerBase tower;
	public ProjectileBase projectilePrefab;

	public ParticleSystem particle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		particle.Play();
	}
}
