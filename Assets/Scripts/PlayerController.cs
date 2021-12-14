using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public GameObject pickUps;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject loseTextObject;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }

    void FixedUpdate() 
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        // End Game
        if(count >= pickUps.transform.childCount)
        {
            EndGame(true);
        }
    }

    // Si es positivo, entonces mostrar texto de ganar
    void EndGame(bool win)
    {
        gameObject.SetActive(false);

        if(win)
        {
            winTextObject.SetActive(true);
        }
        else
        {
            loseTextObject.SetActive(true);
        }
            
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;

            SetCountText();
        }
    }

    // Nueva funcion
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Spike"))
        {
            EndGame(false);
        }
    }
}
