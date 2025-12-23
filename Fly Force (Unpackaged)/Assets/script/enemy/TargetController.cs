using UnityEngine;
using System.Collections;

namespace Enemy
{
    public class TargetController : MonoBehaviour
    {
        // 피격 관련
        public SpriteRenderer spriteRenderer;
        public Color currentColor;
        protected bool isDead = false;

        protected void Awake()
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                currentColor = spriteRenderer.color;
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public IEnumerator OnDamageEffect()
        {
            if (isDead)
            {
                spriteRenderer.color = currentColor;
                yield break;
            }
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = currentColor;
            if (isDead)
            {
                StopCoroutine(nameof(OnDamageEffect));
                spriteRenderer.color = currentColor;
                yield break;
            }
        }
    }
}