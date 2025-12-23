using UnityEngine;

namespace Enemy
{
    public class MinionController : TargetController
    {
        public GameObject ememyBullet;
        GameObject player;
        Rigidbody2D rg2D;
        float fireDelay;

        Animator animator;
        float moveSpeed;

        public GameObject[] item;
        // HP
        public int hp { get; private set; }
        // 태그 임시 저장
        public string tagName;
        // 점수
        int score;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public void Start()
        {
            animator = GetComponent<Animator>();
            isDead = false;
            player = GameObject.FindGameObjectWithTag("Player");
            tagName = gameObject.tag;
            score = 0;
            // 이동 관련 변수
            rg2D = GetComponent<Rigidbody2D>();
            moveSpeed = Random.Range(5.0f, 7.0f);
            fireDelay = 2.5f;
            if (gameObject.CompareTag("ItemDropper"))
                hp = 3;
            else
                hp = 1;
            Move();
        }

        // Update is called once per frame
        public void Update()
        {
            FireBullet();
        }

        private void FireBullet()
        {
            if (player == null)
                return;

            fireDelay += Time.deltaTime;
            if (fireDelay > 3f)
            {
                Instantiate(ememyBullet, transform.position, Quaternion.identity);
                fireDelay -= 3f;
            }
        }

        private void Move()
        {
            if (player == null)
                return;
            Vector3 distance = player.transform.position - transform.position;
            Vector3 dir = distance.normalized;
            rg2D.linearVelocity = dir * moveSpeed;
        }

        public void TakeDamage(int incomingDamage, string damageSource)
        {
            hp -= incomingDamage;
            if (hp > 0)
                StartCoroutine(OnDamageEffect());
            if (hp <= 0 && !isDead)
            {
                if (damageSource == "Shield")
                {
                    OnDead();
                }
                else if (damageSource != "Shield")
                {
                    OnDeadWithScore();
                }
            }
        }

        public int HandleDamage(int incomingDamage, string damageSource)
        {
            int remainingDamage = incomingDamage;

            // HP1 처리
            if (hp > 0)
            {
                int damageToHp = Mathf.Min(remainingDamage, hp);
                TakeDamage(damageToHp, damageSource);
                remainingDamage -= damageToHp;
            }

            // (처음 받은 데미지 - 남은 데미지) = 실제 보스가 입은 데미지
            return incomingDamage - remainingDamage;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("BlockCollider"))
            {
                OnDisappear();
            }
        }

        private void OnDead()
        {
            isDead = true;
            StopAllCoroutines();
            spriteRenderer.color = currentColor;
            if (tagName == "ItemDropper")
            {
                int temp = Random.Range(0, item.Length);
                Instantiate(item[temp], transform.position, Quaternion.identity);
            }
            animator.SetInteger("State", 1);
            if (gameObject.tag != "Untagged")
            {
                SoundManager.instance.enemyDeadSound.Play();
            }
            gameObject.tag = "Untagged";
        }
        private void OnDeadWithScore()
        {
            UIManager.instance.AddScore(score);
            OnDead();
        }

        private void OnDisappear()
        {
            Destroy(gameObject);
        }

        public void OnDieAnimationFinished()
        {
            Destroy(gameObject);
        }
    }
}