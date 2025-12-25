using Player;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class BossController : TargetController
    {
        // 플레이어
        PlayerController playerController;
        GameObject player;

        // 체력바
        public int hp1 { get; private set; } // 초록색
        public int hp2 { get; private set; } // 빨간색

        Animator animator;

        bool isMovingToItsPosition;

        // 점수
        int score;

        // 공격 위치
        [SerializeField] private Transform LAttackPos;
        [SerializeField] private Transform RAttackPos;
        // 총알
        [SerializeField] private GameObject bossBullet;

        // 애니메이션 상태 확인용
        // -1 : 대기, 이동 반복
        // 0 : 대기
        // 1 : L공격1
        // 2 : R공격1
        // 3 : L공격2
        // 4 : R공격2
        // 5 : Die

        Transform spawnMovePos;

        float speed;

        float fireDelay;

        new void Awake()
        {
            base.Awake();
            hp1 = 150;
            hp2 = 150;
            animator = GetComponent<Animator>();
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            RefreshPlayerReference();
            spawnMovePos = GameObject.Find("BossSpawn").GetComponent<Transform>();

            isDead = false;
            isMovingToItsPosition = true;

            score = 1000;
            speed = 10;

            UIManager.instance.BossHpBarCheck();

            StartCoroutine(AttackRoutine());
        }

        // Update is called once per frame
        void Update()
        {
            if (isMovingToItsPosition)
            {
                MoveBoss();
            }
            if (player == null || (PlayerController.instance != null && player != PlayerController.instance.gameObject))
            {
                RefreshPlayerReference();
            }
        }

        private void RefreshPlayerReference()
        {
            if (PlayerController.instance != null)
            {
                playerController = PlayerController.instance;
                player = playerController.gameObject;
            }
            else
            {
                playerController = null;
                player = null;
            }
        }

        private void MoveBoss()
        {
            transform.position = Vector3.MoveTowards(transform.position, spawnMovePos.position, speed * Time.deltaTime);
            if (transform.position == spawnMovePos.position)
            {
                isMovingToItsPosition = false;
            }
        }

        public void TakeDamage(int incomingDamage, string damageSource)
        {
            bool isDamaged = false;
            if (hp1 > 0 && damageSource != "shield")
            {
                hp1 -= incomingDamage;
                isDamaged = true;
            }
            else if (hp2 > 0 && damageSource != "shield")
            {
                hp2 -= incomingDamage;
                isDamaged = true;
            }
            if (isDamaged == true)
                StartCoroutine(OnDamageEffect());
            if (hp2 <= 0 && isDead == false)
            {
                OnDeadWithScore();
            }
        }

        public int HandleDamage(int incomingDamage, string damageSource)
        {
            int remainingDamage = incomingDamage;

            // HP1 처리
            if (hp1 > 0)
            {
                int damageToHp1 = Mathf.Min(remainingDamage, hp1);
                TakeDamage(damageToHp1, damageSource);
                remainingDamage -= damageToHp1;
            }

            // HP2 처리 (HP1 깎고 남은 게 있다면)
            if (remainingDamage > 0 && hp2 > 0)
            {
                int damageToHp2 = Mathf.Min(remainingDamage, hp2);
                TakeDamage(damageToHp2, damageSource);
                remainingDamage -= damageToHp2;
            }

            // (처음 받은 데미지 - 남은 데미지) = 실제 보스가 입은 데미지
            return incomingDamage - remainingDamage;
        }

        IEnumerator AttackRoutine()
        {
            yield return new WaitUntil(() => isMovingToItsPosition == false);

            while (!isDead)
            {
                int attackType;
                fireDelay = 1.0f;

                if (hp1 > 0)
                {
                    attackType = Random.Range(1, 3);
                    if (attackType == 1)
                    {
                        animator.SetTrigger("LAttack");
                    }
                    else if (attackType == 2)
                    {
                        animator.SetTrigger("RAttack");
                    }
                }
                else // hp2 상태
                {
                    attackType = Random.Range(3, 5);
                    if (attackType == 3)
                    {
                        animator.SetTrigger("LAttack2");
                    }
                    else if (attackType == 4)
                    {
                        animator.SetTrigger("RAttack2");
                    }
                }

                yield return new WaitForSeconds(0.6f);
                animator.SetTrigger("Idle");
                yield return new WaitForSeconds(fireDelay);
            }
        }

        void LAttack()
        {
            if (player == null)
                return;

            Instantiate(bossBullet, LAttackPos.position, Quaternion.identity);
        }

        void RAttack()
        {
            if (player == null)
                return;
                
            Instantiate(bossBullet, RAttackPos.position, Quaternion.identity);
        }

        private void OnDeadWithScore()
        {
            isDead = true;
            StopAllCoroutines();
            spriteRenderer.color = currentColor;
            animator.SetTrigger("Die");
            UIManager.instance.TurnOffBossInterface();
            if (GameManager.instance.lifeCount < 2)
            {
                GameManager.instance.AddLife(1);
                UIManager.instance.LifeCheck(GameManager.instance.lifeCount);
            }
        }

        public void OnDieAnimationFinished()
        {
            Debug.Log("Boss is dying!");
            UIManager.instance.AddStageNo();
            UIManager.instance.AddScore(score);
            EnemySpawnManager.instance.BossIsKilled();
            Destroy(gameObject);
        }
    }
}