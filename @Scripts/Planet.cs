using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;

public class Planet : MonoBehaviour
{

    public PlanetGameManager manager;
    public Rigidbody2D _rigidbody;
    Animator _animator;
    SpriteRenderer _spriteRenderer;

    public bool isDrag;
    public int level;
    float deadTime;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (isDrag)
        {

            Vector3 targetPos = Input.mousePosition;
            targetPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
            targetPos = Camera.main.ScreenToWorldPoint(targetPos);

            targetPos.z = 0;
            targetPos.y = 8;

            float leftBorder = -4.2f + (1 + transform.localScale.x) / 2f;
            float rightBorder = 4.2f - (1 + transform.localScale.x) / 2f;

            transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f);

            Vector3 currentPos = transform.position;
            currentPos.x = Mathf.Clamp(currentPos.x, leftBorder, rightBorder);
            transform.position = currentPos;
        }
    }

    public void Drag()
    {
        isDrag = true;
    }

    public void Drop()
    {
        isDrag = false;
        _rigidbody.simulated = true;
        _rigidbody.gravityScale = 1;
    }

    public void SetLevel(int newLevel)
    {
        level = newLevel;
        if (_animator == null)
            _animator = GetComponent<Animator>();

        _animator.SetInteger("Level", level);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Planet"))
        {
            Planet other = collision.collider.GetComponent<Planet>();
            if (level == other.level)
            {
                _animator.SetInteger("Level", level + 1);
                level++;
                CreateMergeEffect(other.transform.position);
                Destroy(other.gameObject);
                manager.maxLevel = Mathf.Max(level, manager.maxLevel);
                manager.score += 2 * level;
                manager.scoreText.text = "Score : " + manager.score;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            deadTime += Time.deltaTime;

            if (deadTime > 2)
            {
                _spriteRenderer.color = new Color(0.9f, 0.2f, 0.2f);
            }
            if (deadTime > 5)
            {
                Time.timeScale = 0;
                manager.GameOverUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            deadTime = 0;
            _spriteRenderer.color = Color.white;
        }
    }

    void CreateMergeEffect(Vector3 position)
    {
        GameObject effect = Instantiate(manager.mergeEffect, position, Quaternion.Euler(90, 0, 0)); // Z축 회전으로 화면에 보이게함

        float sizeFactor = transform.localScale.x * 3f; // 파티클 크기 키우기
        effect.transform.localScale = Vector3.one * sizeFactor;

        Destroy(effect, 2f);
    }


}
