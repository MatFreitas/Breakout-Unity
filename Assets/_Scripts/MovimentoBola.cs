using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoBola : MonoBehaviour
{
    GameManager gm;
    public float velocidade = 7.14f;
    public bool spacePressed;
    private Vector3 direcao;
    private AudioSource soundEffect;
    private Animator anim;
    private SpriteRenderer arrow;
    private GameObject arrowChild;
    public float rotation;
    public bool clockwise;
    public Vector3 direction;


    
    // Start is called before the first frame update
    void Start()
    {   
        arrow = GetComponent<SpriteRenderer>();
        arrowChild = GameObject.Find("Arrow");

        transform.Rotate(0.0f, 0.0f, 0.15f);    

        rotation = transform.eulerAngles.z;
        clockwise = false;

        float dirX = Random.Range(-5.0f, 5.0f);
        float dirY = Random.Range(1.0f, 5.0f);

        soundEffect = GetComponent<AudioSource>();
        // anim = GetComponent<Animator>();

        spacePressed = false;

        direcao = new Vector3(dirX, dirY).normalized;

        gm = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        rotation = transform.eulerAngles.z;

        if(rotation > 175) {
            clockwise = true;
        } else if (rotation  < 5) {
            clockwise = false;
        }

        if(rotation < 175 && !clockwise) {
            transform.Rotate(0.0f, 0.0f, 0.15f); 
        } else if (rotation > 0 && rotation < 179 && clockwise) {
            transform.Rotate(0.0f, 0.0f, -0.15f);
        }
        

        

        Debug.Log($"Rotação: {direction.x}");

        if (gm.gameState != GameManager.GameState.GAME)
        {
            spacePressed = false;
            return;
        } 
        // leave spin or jump to complete before changing

        if(!spacePressed) 
        {
            float inputX = Input.GetAxis("Horizontal");
            transform.position += new Vector3(inputX, 0, 0)*Time.deltaTime*velocidade;

            direction =  arrowChild.transform.position - transform.position;
            direcao = new Vector3(direction.x, direction.y).normalized;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            spacePressed = true;
            arrowChild.SetActive(false);
        } 

        if (!spacePressed) return;

        transform.position += direcao * Time.deltaTime * velocidade;
    
        Vector2 posicaoViewport = Camera.main.WorldToViewportPoint(transform.position);

        if (posicaoViewport.x < 0 || posicaoViewport.x > 1) {
            direcao = new Vector3(-direcao.x, direcao.y);
        }

        if(posicaoViewport.y > 1) {
            direcao = new Vector3(direcao.x, -direcao.y);
        }

        if(posicaoViewport.y < 0) {
            Reset();
        }

        // Debug.Log($"Vidas: {gm.vidas} \t | \t Pontos: {gm.pontos}");
    }

    private void Reset() 
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.position = playerPosition + new Vector3(0, 0.5f, 0);

        // float dirX = Random.Range(-5.0f, 5.0f);
        // float dirY = Random.Range(1.0f, 5.0f);

        arrowChild.SetActive(true);
        spacePressed = false;
        gm.vidas--;

        if(gm.vidas <= 0 && gm.gameState == GameManager.GameState.GAME)
        {
            gm.ChangeState(GameManager.GameState.ENDGAME);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {

        if (col.gameObject.CompareTag("Player")) {
            float dirX = Random.Range(-5.0f, 5.0f);
            float dirY = Random.Range(1.0f, 5.0f);

            direcao = new Vector3(dirX, dirY).normalized;
        } else if (col.gameObject.CompareTag("Tijolo")) {
            direcao = new Vector3(direcao.x, -direcao.y);
            gm.pontos++;
            //anim.Play("Explosion");
            soundEffect.Play();
        }
    }
}
