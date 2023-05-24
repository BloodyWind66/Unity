using UnityEngine;

public class Mouvement : MonoBehaviour
{
    public float speed = 5f; // Vitesse de déplacement du joueur
    public float jumpForce = 10f; // Force de saut du joueur
    public LayerMask groundLayer; // Layer où se trouvent les plateformes

    private Rigidbody2D rb;
    private bool isGrounded = true;
    public SpriteRenderer spriteRenderer;
    public Transform groundCheck;
    public LayerMask collisionLayers;
    public float groundCheckRadius;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // On récupère le composant Rigidbody2D attaché au GameObject
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius);

        float moveHorizontal = Input.GetAxis("Horizontal"); // On récupère la valeur de l'axe horizontal (-1, 0, ou 1)

        Vector2 movement = new Vector2(moveHorizontal, 0f); // On crée un vecteur de mouvement avec la valeur de l'axe horizontal

        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y); // On applique la vitesse horizontale au Rigidbody2D pour déplacer le joueur

       // isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer); // On vérifie si le joueur touche une plateforme

        if (Input.GetButtonDown("Jump") && isGrounded) // Si le joueur appuie sur la touche de saut et qu'il touche une plateforme
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // On ajoute une impulsion vers le haut au Rigidbody2D pour faire sauter le joueur
        }

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
    }

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }else if(_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }
    private void OnDrawnGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
