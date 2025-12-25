using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController instance;

        float x;
        float y;
        Vector3 speedVector;

        public Vector3 limitMax;
        public Vector3 limitMin;
        Vector3 temp;

        public float speed;
        public float fireDelay;
        Animator animator;

        ShieldController shieldController;
        public int ShieldScorePenalty;

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
                UIManager.instance.BombCheck(value);
            }
        }

        public GameObject Shield;
        public bool isShieldActive { get; private set; }
        private float _shieldAmmo = 0.0f;
        public float ShieldAmmo
        {
            get { return _shieldAmmo; }
            private set
            {
                _shieldAmmo = value;
            }
        }
        private float _maxValue = 1.0f;
        public float maxValue
        {
            get { return _maxValue; }
            private set
            {
                _maxValue = value;
            }
        }

        public bool respawned { get; private set; }
        public bool deadAnimFinished { get; private set; }

        private void Awake()
        {
            instance = this;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            fireDelay = 0;
            speed = 10.0f;

            animator = GetComponent<Animator>();
            isDead = false;

            bulletLevel = 1;

            ShieldScorePenalty = 100;
            isShieldActive = false;

            Bomb = 1;
            UIManager.instance.BombCheck(Bomb);
            IsBulletBombPresent = false;

            respawned = true;
            deadAnimFinished = false;

            if (UIManager.instance != null)
            {
                UIManager.instance.RegisterPlayer(this);
            }
        }

        // Update is called once per frae
        private void Update()
        {
            if (isDead == false)
            {
                Move();
                ShieldAmmoSetter();
                ShieldModule();
                RespawnShield();
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
            if (Input.GetButton("Gun") && isShieldActive == false)
            {
                Debug.Log("Shoot");
                fireDelay += Time.deltaTime;
                if (fireDelay > 0.0f)
                {
                    GameObject go = Instantiate(prefabBullet[bulletLevel - 1], transform.position, Quaternion.identity);
                    ProjectileController projectileController = go.GetComponent <ProjectileController>();
                    projectileController.Initializer(this);
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
            if (Input.GetButtonDown("Bomb") && isShieldActive == false && IsBulletBombPresent == false)
            {
                Debug.Log("Bomb");
                if (Bomb >= 1)
                {
                    IsBulletBombPresent = true;
                    Debug.Log("A bomb is on the way!");
                    GameObject go = Instantiate(BulletBomb, transform.position, Quaternion.identity);
                    ProjectileController projectileController = go.GetComponent<ProjectileController>();
                    projectileController.Initializer(this);
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
                if (isShieldActive == false && ShieldAmmo == 1.0f)
                {
                    Debug.Log("Shield On!");
                    ShieldOn(100, 3.0f);
                    ShieldAmmoForceSetter(0.0f);
                }
                else if (isShieldActive == true)
                {
                    if(shieldController != null)
                    { 
                        shieldController.DestroyShield();
                        SoundManager.instance.ShieldOffSound.Play();
                    }
                }
            }
        }

        public void ShieldAmmoSetter()
        {
            if (ShieldAmmo >= 0.0f && ShieldAmmo < 1.0f && isShieldActive == false)
            {
                ShieldAmmo += Time.deltaTime * 0.1f;
                Debug.Log("Shield Charged " + ShieldAmmo * 100 + "%");
            }
            if (ShieldAmmo >= 1.0f)
            {
                ShieldAmmo = 1.0f;
                Debug.Log("Shield is Fully Charged!");
            }
        }

        public void ShieldAmmoForceSetter(float amount)
        {
            ShieldAmmo = amount;
        }

        public void RespawnShield()
        {
            if (respawned == true)
            {
                Debug.Log("Just Respawned!");
                ShieldOn(100, 1.0f);
                respawned = false;
            }
        }

        public void ShieldOn(int damage, float duration)
        {
            GameObject go = Instantiate(Shield, transform.position, Quaternion.identity);
            shieldController = go.GetComponent<ShieldController>();
            shieldController.Initializer(this);
            shieldController.DamageForceSetter(damage);
            shieldController.DurationForceSetter(duration);
            SetShieldActiveState(true);
        }

        public void SetShieldActiveState(bool state)
        {
            isShieldActive = state;
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
                if (isShieldActive == true)
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
            UIManager.instance.LifeCheck(GameManager.instance.lifeCount);
        }

        private void OnDestroy()
        {
            if (instance == this) instance = null;
        }
    }
}