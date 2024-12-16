using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

public class HeroKnight : MonoBehaviour
{

    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float m_rollForce = 6.0f;
    [SerializeField] bool m_noBlood = false;
    [SerializeField] GameObject m_slideDust;
    [SerializeField] private int attackDamage = 10;
    public float Speed
{
    get { return m_speed; }
    set { m_speed = value; }
}

public float JumpForce{
     get { return m_jumpForce; }
    set { m_jumpForce = value; }
}

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_HeroKnight m_groundSensor;
    private Sensor_HeroKnight m_wallSensorR1;
    private Sensor_HeroKnight m_wallSensorR2;
    private Sensor_HeroKnight m_wallSensorL1;
    private Sensor_HeroKnight m_wallSensorL2;
    private BoxCollider2D SwordCollider;
    private bool m_isWallSliding = false;
    private bool m_grounded = false;
    private bool m_rolling = false;
    private int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;
    private float m_rollDuration = 8.0f / 14.0f;
    private float m_rollCurrentTime;
    private SpriteRenderer m_spriteRenderer;

    //Movement on Moving Platforms
    private Transform currentPlatform;
    private bool isOnPlatform = false; 

    //Player HEALTH
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public bool isDying = false;



    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
        SwordCollider = transform.Find("Sword").GetComponent<BoxCollider2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        


        //Player HEALTH
        currentHealth = 100;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        // Increase timer that checks roll duration
        if (m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if (m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);


        }

       

    

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction and swap sword collider direction
        if (inputX > 0 && m_spriteRenderer.flipX == true)
        {
            m_spriteRenderer.flipX = false;
            m_facingDirection = 1;
            SwordCollider.offset = new Vector2(-SwordCollider.offset.x, SwordCollider.offset.y);
        
        }

        else if (inputX < 0 && m_spriteRenderer.flipX == false)
        {
            m_spriteRenderer.flipX = true;
            m_facingDirection = -1;
            SwordCollider.offset = new Vector2(-SwordCollider.offset.x, SwordCollider.offset.y);
       
        }

        // Move
        if (!m_rolling)
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // -- Handle Animations --

        /* Wall Slide
        m_isWallSliding = (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State());
        m_animator.SetBool("WallSlide", m_isWallSliding); */

        //Death
        if (Input.GetKeyDown("e") && !m_rolling)
        {
            m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
        }

        //Hurt
        else if (Input.GetKeyDown("q") && !m_rolling)
            m_animator.SetTrigger("Hurt");

        //Attack
        else if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
        {
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            // Reset timer
            m_timeSinceAttack = 0.0f;

            //Attack Hit
            StartCoroutine("HitAttack");
        }

        /* Block
        else if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
        }

        else if (Input.GetMouseButtonUp(1))
            m_animator.SetBool("IdleBlock", false);

        // Roll
        else if (Input.GetKeyDown("left shift") && !m_rolling && !m_isWallSliding)
        {
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
        }
        */


        //Jump
        else if (Input.GetKeyDown("space") && m_grounded && !m_rolling)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }

        //Player HEALTH
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(20);



        }
        if (currentHealth <= 0 && !isDying)
        {
            m_animator.SetTrigger("Death");
            isDying = true;
        }


        //Movemtn Platform
        if (Input.GetKeyDown(KeyCode.Space) && isOnPlatform)
    {
        transform.SetParent(null); // Parenting entfernen
        isOnPlatform = false; // Spieler verlässt die Plattform

    }


    }

    //Player HEALTH
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    //SwordCollider
    private IEnumerator HitAttack()
    {
         SwordCollider.enabled = true;

    // Schaden beim Einschalten des Schwertkolliders berechnen
    Collider2D[] hitEnemies = new Collider2D[10];
    ContactFilter2D filter = new ContactFilter2D();
    filter.SetLayerMask(LayerMask.GetMask("Enemies")); // Gegner-Layer
    int hits = SwordCollider.OverlapCollider(filter, hitEnemies);

    for (int i = 0; i < hits; i++)
    {
        EnemyController enemy = hitEnemies[i].GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(attackDamage);
        }
    }

    yield return new WaitForSeconds(0.2f);
    SwordCollider.enabled = false;
    }
    // Animation Events
    // Called in slide animation.
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (m_facingDirection == 1)
            spawnPosition = m_wallSensorR2.transform.position;
        else
            spawnPosition = m_wallSensorL2.transform.position;

        if (m_slideDust != null)
        {
            // Set correct arrow spawn position
            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            // Turn arrow in correct direction
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }

    //Movement on Moving Platforms
  private void OnTriggerEnter2D(Collider2D collision)
{
    // Prüfen, ob der Trigger mit einer Plattform erfolgt
    if (collision.CompareTag("MovingPlatform"))
    {
        // Wenn der Spieler noch nicht auf der Plattform ist, setze Parenting
        if (!isOnPlatform)
        {
            currentPlatform = collision.transform;
            isOnPlatform = true;
            transform.SetParent(currentPlatform); // Parenting setzen
        }
    }

    if (collision.CompareTag("Enemy"))
    {
        EnemyController enemy = collision.GetComponent<EnemyController>();
        if (enemy != null && SwordCollider.enabled) // Stelle sicher, dass der Schwertkollider aktiv ist
        {
            enemy.TakeDamage(attackDamage); // Schaden zufügen
        }
    }
}

private void OnTriggerExit2D(Collider2D collision)
{
    // Prüfen, ob der Trigger mit der Plattform endet
    if (collision.CompareTag("MovingPlatform") && collision.transform == currentPlatform)
    {
        currentPlatform = null;
        isOnPlatform = false;
        transform.SetParent(null); // Parenting entfernen
    }
}






}