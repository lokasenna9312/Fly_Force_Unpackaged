using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        float x;
        float y;
        Vector3 speedVector;

        public Vector3 limitMax;
        public Vector3 limitMin;
        Vector3 temp;

        public float speed;
        public float fireDelay;
        Animator animator;

        ShieldAmmoGaugeController shieldAmmoGaugeController;

        public bool isDead { get; private set; }

        [SerializeField] private GameObject[] prefabBullet;
        private int _bulletLevel;
        public int bulletLevel
        {
            get => _bulletLevel;
            private set => _bulletLevel = value;
        }

        [SerializeField] private GameObject BulletBomb;
        public bool IsBulletBombPresent { get; private set; }
        private int _bomb;
        public int Bomb
        {
            get => _bomb;
            private set
            {
                _bomb = value;
                UIManager.instance.BombCheck(_bomb);
            }
        }

        public GameObject Shield;
        public GameObject currentShieldInstance { get; private set; }
        public int ShieldDamage;
        public float ShieldDuration;
        public int ShieldScorePenalty;

        public bool respawned { get; private set; }
        public bool deadAnimFinished { get; private set; }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            fireDelay = 0;
            speed = 10.0f;

            animator = GetComponent<Animator>();
            isDead = false;

            bulletLevel = 1;

            Bomb = 1;
            currentShieldInstance = null;
            shieldAmmoGaugeController = UIManager.instance.shieldGaugeController;
            ShieldScorePenalty = 100;
            ShieldDamage = 100;

            UIManager.instance.BombCheck(Bomb);
            IsBulletBombPresent = false;

            respawned = true;
            deadAnimFinished = false;

        }

        // Update is called once per frae
        private void Update()
        {
            RespawnShield();
            if (isDead == false)
            {
                Move();
                ShieldModule();
                FireBullet();
                FireBomb();
            }
            if (isDead == true) MoveByInertia();
        }
        public void Move()
        {
            x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            speedVector = new(x, y, 0);

            transform.Translate(speedVector);

            if (transform.position.x > limitMax.x)
            {
                temp.x = limitMax.x;
                temp.y = transform.position.y;
                transform.position = temp;
            }
            if (transform.position.y > limitMax.y)
            {
                temp.y = limitMax.y;
                temp.x = transform.position.x;
                transform.position = temp;
            }
            if (transform.position.x < limitMin.x)
            {
                temp.x = limitMin.x;
                temp.y = transform.position.y;
                transform.position = temp;
            }
            if (transform.position.y < limitMin.y)
            {
                temp.y = limitMin.y;
                temp.x = transform.position.x;
                transform.position = temp;
            }
        }

        public void MoveByInertia()
        {
            transform.Translate(speedVector);
        }

        public void FireBullet()
        {
            if (isDead == true) return;
            if (Input.GetButton("Gun") && currentShieldInstance == null)
            {
                Debug.Log("Shoot");
                fireDelay += Time.deltaTime;
                if (fireDelay > 0.0f)
                {
                    Instantiate(prefabBullet[bulletLevel - 1], transform.position, Quaternion.identity);
                    fireDelay -= 0.3f;
                }
            }
            else
                fireDelay = 0.0f;
        }

        public void IncreaseBulletLevel(int delta)
        {
            bulletLevel += delta;
            Debug.Log("Main gun has upgraded by " + bulletLevel);
        }

        public void FireBomb()
        {
            if (isDead == true) return;
            if (Input.GetButtonDown("Bomb") && currentShieldInstance == null && IsBulletBombPresent == false)
            {
                Debug.Log("Bomb");
                if (Bomb >= 1)
                {
                    IsBulletBombPresent = true;
                    Debug.Log("A bomb is on the way!");
                    GameObject go = Instantiate(BulletBomb, transform.position, Quaternion.identity);
                    Bomb--;
                }
            }
        }

        public void GetBomb(int delta)
        {
            Bomb += delta;
        }


        public void BulletBombDowned()
        {
            IsBulletBombPresent = false;
            Debug.Log("Bomb cleared!");
        }

        public void ShieldModule()
        {
            if (Input.GetButtonDown("Shield"))
            {
                if (currentShieldInstance == null && shieldAmmoGaugeController.ShieldAmmo == 1.0f)
                {
                    Debug.Log("Shield On!");
                    ShieldDuration = 3.0f;
                    ShieldOn(-ShieldScorePenalty, ShieldDuration);
                }
                else if (currentShieldInstance != null)
                {
                    DestroyShield();
                    SoundManager.instance.ShieldOffSound.Play();
                }
            }
            if (currentShieldInstance != null)
            {
                if (ShieldDuration > 0.0f)
                {
                    ShieldDuration -= Time.deltaTime * 1.0f;
                    Debug.Log("Shield Duration: " + ShieldDuration + " seconds");
                    if (ShieldDuration <= 0.0f)
                    {
                        DestroyShield();
                        SoundManager.instance.ShieldOffSound.Play();
                    }
                }
            }
        }

        public void ShieldOn(int shieldScorePenalty, float duration)
        {
            UIManager.instance.AddScore(-shieldScorePenalty);
            currentShieldInstance = Instantiate(Shield, transform.position, Quaternion.identity);
            currentShieldInstance.transform.parent = transform;
            shieldAmmoGaugeController.ShieldAmmo = 0.0f;
            ShieldDuration = duration;
        }
        void DestroyShield()
        {
            Destroy(currentShieldInstance);
            currentShieldInstance = null;
            Debug.Log("Shield Off!");
        }

        public void RemoveShield()
        {
            if (currentShieldInstance != null)
            {
                Destroy(currentShieldInstance);
                currentShieldInstance = null;
                SoundManager.instance.ShieldOffSound.Play();
                Debug.Log("Shield was nullified!");
            }
        }

        public void RespawnShield()
        {
            if (respawned == true)
            {
                Debug.Log("Just Respawned!");
                ShieldDuration = 1.0f;
                ShieldOn(0, ShieldDuration);
                respawned = false;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(limitMax, new Vector2(limitMax.x, limitMin.y));
            Gizmos.DrawLine(limitMax, new Vector2(limitMin.x, limitMax.y));
            Gizmos.DrawLine(limitMin, new Vector2(limitMax.x, limitMin.y));
            Gizmos.DrawLine(limitMin, new Vector2(limitMin.x, limitMax.y));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("enemyBullet") || collision.CompareTag("enemy") || collision.CompareTag("ItemDropper") || collision.CompareTag("Boss"))
            {
                if (currentShieldInstance != null)
                {
                    Debug.Log("Bullet hit, but Shield is Active. Ignoring damage.");
                    return;
                }
                if (isDead == true)
                {
                    Debug.Log("Player is already dead.");
                    return;
                }
                Debug.Log("Bullet hit, Shield is Off. Player Dead.");
                isDead = true;
                animator.SetInteger("State", 1);
                GetComponent<Collider2D>().enabled = false;
                if (SoundManager.instance.playerDeadSound.isPlaying == false)
                    SoundManager.instance.playerDeadSound.Play();
                GameManager.instance.PlayerDeath();
            }
        }

        public void OnDieAnimationFinished()
        {
            deadAnimFinished = true;
            Destroy(gameObject);
            if (GameManager.instance.lifeCount >= 0) GameManager.instance.CreatePlayer();
            GetComponent<Collider2D>().enabled = true;
            UIManager.instance.LifeCheck(GameManager.instance.lifeCount);
        }
    }
}