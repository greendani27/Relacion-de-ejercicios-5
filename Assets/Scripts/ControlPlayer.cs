using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    public float speed = 1f;

    public float maxLeft;
    public float maxRight;

    public GameObject shootPrefab;
    public SpriteRenderer laserSpriteRenderer;

    private bool _canFire = true;

    public GameObject enemyPrefab;
    // Start is called before the first frame update

    [SerializeField] private Animator _animator;

    private bool choque = false;


    void Update()
    {
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.position = new Vector3( transform.position.x - (speed * Time.deltaTime), transform.position.y);
            transform.position = new Vector3(Mathf.Max(maxLeft, transform.position.x - (speed * Time.deltaTime)), transform.position.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y);
            transform.position = new Vector3(Mathf.Min(maxRight, transform.position.x + (speed * Time.deltaTime)), transform.position.y);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_canFire)
            {
                _canFire = false;
                GameObject go = Instantiate(shootPrefab);
                //go.transform.position = this.transform.position;
                go.transform.position = laserSpriteRenderer.gameObject.transform.position;
                go.GetComponent<ControlShoot>().controlPlayer = this;
                laserSpriteRenderer.enabled = false;
            }
        }
    }

    public void CanFire()
    {
        _canFire = true;
        laserSpriteRenderer.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<ControlEnemyA>(out var component) && !choque)
        {
            if (GameManager.Instance.vidas >= 0)
            {
                _animator.Play("AnimationExplotion");
                GameManager.Instance.explosion.Play();
                GameManager.Instance.vidas -= 1;
                GameManager.Instance.contadorVidas.sprite = GameManager.Instance.spriteVidas[GameManager.Instance.vidas];
                choque = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        choque = false;
        _animator.Play("Idle");
        GameManager.Instance.explosion.Stop();
    }
}
