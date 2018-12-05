using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;

    public float width = 10f;
    public float height = 5f;

    private bool _movingRight = true;
    public float Speed = 5f;

    private float _xmin;
    private float _xmax;

	// Use this for initialization
	void Start ()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 meestlinks = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 meestrechts = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        _xmin = meestlinks.x;
        _xmax = meestrechts.x;

        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
	}
	
	// Update is called once per frame
	void Update()
    {
        if (_movingRight)
        {
            transform.position += Vector3.right * Speed * Time.deltaTime;
	    }
        else
        {
            transform.position += Vector3.left * Speed * Time.deltaTime;
        }
        float rightEdgeFormation = transform.position.x + (0.5f * width);
        float leftEdgeFormation = transform.position.x - (0.5f * width);
        if (leftEdgeFormation < _xmin)
        {
            _movingRight = true;
        }
        else if (rightEdgeFormation > _xmax)
        {
            _movingRight = false;
        }
    }
  

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 1f));
    }
}
