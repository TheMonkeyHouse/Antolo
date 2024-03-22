using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float lifetime;
    [SerializeField] private TextMesh textDisplay;
    private Rigidbody2D rb;
    private float timeAlive;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= lifetime)
        {
            Destroy(this.gameObject);
        }
    }

    private void SetRandomVelocity()
    {
        rb.velocity = new Vector2(Random.Range(-0.5f, 0.5f), 2.5f);
    }

    public void Initialize(float lifetime, string textValue, Color color)
    {
        this.lifetime = lifetime;
        this.textDisplay.text = textValue;
        this.textDisplay.color = color;
        SetRandomVelocity();
    }
}