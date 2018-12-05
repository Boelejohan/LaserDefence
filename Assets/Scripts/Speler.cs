using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speler : MonoBehaviour
{
    private float _stuur;
    private float _xmin;
    private float _xmax;

    public float Padding = 0.7f;
    public float Snelheid;

    public GameObject laserPreFab;

    private float straalsnelheid = 4f;
    float fireRate = 0.5f; 

	// Use this for initialization
	void Start ()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 meestlinks = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 meestrechts = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        _xmin = meestlinks.x + Padding;
        _xmax = meestrechts.x - Padding;
    }
	
	// Update is called once per frame
	void Update ()
    {
        _stuur = Mathf.Clamp((_stuur += (Input.GetAxis("Horizontal") * Time.deltaTime * Snelheid)), _xmin, _xmax);
        transform.position = new Vector2(_stuur, transform.position.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, fireRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
    }
    void Fire()
    {
        Vector3 startPositie = transform.position + new Vector3(0, 1.2f, 0);
        GameObject straal = Instantiate(laserPreFab, startPositie, Quaternion.identity) as GameObject;
        straal.GetComponent<Rigidbody2D>().velocity = new Vector2(0, straalsnelheid);
    }
}
