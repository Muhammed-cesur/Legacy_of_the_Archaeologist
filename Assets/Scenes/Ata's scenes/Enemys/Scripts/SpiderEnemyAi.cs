using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpiderEnemyAI : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public float detectionRange = 10f;  // Range at which the enemy detects the player
    public float attackRange = 2f;  // Range at which the enemy attacks the player
    public float movementSpeed = 2f;  // Speed at which the enemy moves
    public int maxHealth = 20;  // Maximum health of the enemy
    public int damageAmount = 10;  // Amount of damage the enemy inflicts on the player
    public float attackCooldown = 3f;  // Cooldown period between enemy attacks
    public List<GameObject> itemDropPrefabs;

    private bool isWandering = false; // Flag to indicate if the enemy is currently wandering
    private float minWanderDelay = 1f; // Minimum delay before changing wander direction
    private float maxWanderDelay = 5f; // Maximum delay before changing wander direction
    private float wanderTimer = 0f; // Timer to track the delay for changing wander direction

    private Animator animator;  // Animator component for controlling animations
    private bool isAttacking = false;  // Flag to indicate if the enemy is attacking
    private int currentHealth;  // Current health of the enemy
    private float attackTimer = 0f;  // Timer to track the attack cooldown

    private float maxWanderDistance = 2f;  // Maximum distance the enemy can wander in a random direction
    private float waitDuration = 1f;  // Duration to wait after wandering before starting to wander again
    private bool isWaiting = false;  // Flag to indicate if the enemy is currently waiting
    private float waitTimer = 0f;  // Timer to track the wait duration

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within the detection range
        if (distanceToPlayer <= detectionRange)
        {
            // Rotate to face the player
            transform.LookAt(player);

            // Check if the player is within attack range and the attack cooldown has expired
            if (distanceToPlayer <= attackRange && attackTimer <= 0f)
            {
                Attack();
                attackTimer = attackCooldown;
            }
            else
            {
                Move();
            }
        }
        else
        {
            if (isWaiting)
            {
                // Update the wait timer
                waitTimer += Time.deltaTime;

                // Check if the wait duration is over
                if (waitTimer >= waitDuration)
                {
                    isWaiting = false;
                    wanderTimer = 0f;
                    Wander();
                }
            }
            else
            {
                Wander();
            }
        }

        // Update the attack cooldown timer
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private void Wander()
    {
        if (!isWandering && !isWaiting)
        {
            isWandering = true;

            // Generate a random delay for changing wander direction
            float delay = Random.Range(minWanderDelay, maxWanderDelay);
            wanderTimer = 0f;

            // Generate a random direction for wandering
            Vector3 wanderDirection = Random.insideUnitSphere;
            wanderDirection.y = 0f; // Ensure no vertical movement

            // Rotate to face the wander direction
            transform.rotation = Quaternion.LookRotation(wanderDirection);

            StartCoroutine(WanderDelay(delay));
        }

        if (isWandering && !isWaiting)
        {
            // Update the wander timer
            wanderTimer += Time.deltaTime;

            // Check if it's time to change the wander direction
            if (wanderTimer >= maxWanderDistance / movementSpeed)
            {
                isWandering = false;
                waitTimer = 0f;
                Wait();
            }
            else
            {
                // Move forward in the local Z-axis
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

                // Add animation code here for the wandering animation
                animator.SetFloat("speed", 1f);
                // Example: animator.SetBool("IsWandering", true);
            }
        }
    }

    private IEnumerator WanderDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isWandering = false;
        animator.SetFloat("speed", 0f);
    }

    private void Wait()
    {
        isWaiting = true;
        animator.SetFloat("speed", 0f);
        float waitDuration = Random.Range(1f, 5f);
        StartCoroutine(WaitDelay(waitDuration));
    }

    private IEnumerator WaitDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

    private void Move()
    {
        // Move towards the player with increased speed
        transform.Translate(Vector3.forward * (movementSpeed * 2f) * Time.deltaTime);
        animator.SetFloat("speed", 1f);
        // Add animation code here for the movement animation
        // Example: animator.SetBool("IsMoving", true);
    }

    private void Attack()
    {
        if (!isAttacking)
        {
            // Set isAttacking flag to true
            isAttacking = true;

            // Play attack animation
            animator.SetTrigger("attack2");

            // Perform attack logic here
            // ...
            StartCoroutine(DelayedDamage());
            // Print a message to the console



        }
        isAttacking = false;
    }

    private IEnumerator DelayedDamage()
    {
        // Wait for the attack animation to finish
        yield return new WaitForSeconds(1f);
        Debug.Log("Enemy attacking = true");
        // Inflict damage on the player
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
        }

        // Set isAttacking flag to false
        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        // Reduce health by the given damage amount
        currentHealth -= (int)damage;
        animator.SetTrigger("damage");

        // Check if the enemy is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Generate a random number between 0 and 1
        float dropChance = Random.value;

        // Define the drop chance threshold for the item
        float itemDropChance = 0.7f; // Adjust this value as desired

        // Check if the drop chance is within the item drop chance threshold
        if (dropChance <= itemDropChance)
        {
            int randomIndex = Random.Range(0, itemDropPrefabs.Count);
            GameObject itemDropPrefab = itemDropPrefabs[randomIndex];

            // Instantiate the item drop prefab at the enemy's position
            GameObject itemDrop = Instantiate(itemDropPrefab, transform.position, Quaternion.identity);

            // Access the item drop script on the instantiated object
            // ItemDrop itemDropScript = itemDrop.GetComponent<ItemDrop>();

            // Set the item properties or type in the item drop script
            // itemDropScript.SetItemType(ItemType.HealthPotion); // Replace ItemType.HealthPotion with the desired item type

            // Set any other properties or behavior for the item drop
            // itemDropScript.SetProperty(...);

            // Example: Play an item drop sound effect
            // AudioManager.PlaySound("ItemDrop");

            // Example: Display an item drop particle effect
            // ParticleManager.SpawnParticle("ItemDropParticle", transform.position);

            // You can also add a random rotation or apply force to the item drop
            // itemDrop.GetComponent<Rigidbody>().AddForce(Vector3.up * dropForce, ForceMode.Impulse);
            // itemDrop.transform.rotation = Random.rotation;
        }

        // Perform death logic here, such as playing death animation, disabling collider, etc.
        // ...

        // Destroy the enemy game object after some delay
        Destroy(gameObject, 0f);

        // Add animation code here for the death animation
        // Example: animator.SetTrigger("Die");
    }
}