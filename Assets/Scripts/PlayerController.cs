using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private int health=250;
    [SerializeField] private float speed;
    [SerializeField] private FixedJoystick joystick;
    private Vector2 input;
    private Rigidbody2D rb;
    public Animator animator;
    public GameObject gameovermenu;
    private int currentHealth;
    private Vector3 startPosition; 
    public AudioManager audioManager;
    Image fillImage;
    float zamanlayici = 0f;
    float tekrarSuresi = 0.5f;
    public Slider healthBar;
    private void Start(){
        Time.timeScale = 1f;
        currentHealth = health;
        startPosition = gameObject.transform.position; 
        healthBar.maxValue=health;
        healthBar.value=health;
        fillImage= healthBar.fillRect.GetComponent<Image>();
    }
    private void Awake() {
        rb=GetComponent<Rigidbody2D>();
    }

    private void Update() {
       // input.x=joystick.Horizontal;
       // input.y=joystick.Vertical;
        float joystickX = joystick.Horizontal;
    float joystickY = joystick.Vertical;
        float keyboardX = Input.GetKey(KeyCode.D) ? 1f : (Input.GetKey(KeyCode.A) ? -1f : 0f);
    float keyboardY = Input.GetKey(KeyCode.W) ? 1f : (Input.GetKey(KeyCode.S) ? -1f : 0f);

    // Joystick ve klavye girdilerini birleştir
    input.x = joystickX != 0f ? joystickX : keyboardX;
    input.y = joystickY != 0f ? joystickY : keyboardY;
        Vector2 direction=new Vector2(input.x,input.y);
        animator.SetBool("CharMove",direction.magnitude>0f);

        if (direction.magnitude > 0.1f)
    {

    zamanlayici += Time.deltaTime;
    if (zamanlayici >= tekrarSuresi)
    {
        audioManager.PlaySFX(audioManager.playerWalkSound);

        zamanlayici = 0f; // zamanlayıcıyı sıfırla
    }
    float angle = (direction.x > 0) ? 0f : -180f;  
    Quaternion toRotation = Quaternion.Euler(0, angle, 0);
    //transform.rotation = toRotation;
    transform.eulerAngles = new Vector3(0, angle, 0);
    if (angle==0)
    {
        healthBar.transform.localScale=new Vector3(1, 1, 1);
    }
    else
    {
        healthBar.transform.localScale=new Vector3(-1, 1, 1);
    }
    }
    }
    
    private void FixedUpdate() {
    Vector2 adjustedInput = (transform.rotation.eulerAngles.y == 180) ? new Vector2(-input.x, input.y) : input;
    rb.MovePosition(transform.position + transform.TransformDirection(adjustedInput * speed * Time.fixedDeltaTime));
}

    public void ResetPlayer()
    {
        health=currentHealth;
        healthBar.value=health;
        fillImage.color=Color.green;
        transform.position=startPosition;
        Debug.Log("Karakter sıfırlandı: Can = " + currentHealth);
    }


    public void TakeDamage(int damage)
    {
        audioManager.PlaySFX(audioManager.playerHitSound);
        health -= damage;
        Debug.Log("Player Health: " + health);
        healthBar.value=health;
        if (health<=healthBar.maxValue/2)
        {
            fillImage.color=Color.red;
        }
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Time.timeScale = 0f;
        gameovermenu.SetActive(true);
        Debug.Log("Player has died.");
    }

}
