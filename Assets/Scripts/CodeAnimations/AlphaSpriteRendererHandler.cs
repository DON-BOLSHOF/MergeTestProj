using UnityEngine;

namespace CodeAnimation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class AlphaSpriteRendererHandler : MonoBehaviour
    {
        [SerializeField, Range(0, 1f)] private float _alphaInterval;

        private float _baseAlpha;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _baseAlpha = _spriteRenderer.color.a;
        }

        public void GlowDim()
        {
            ChangeAlpha(_alphaInterval);
        }

        public void ShineUp()
        {
            ChangeAlpha(_baseAlpha);
        }

        private void ChangeAlpha(float alphaInterval)
        {
            var temp = _spriteRenderer.color;
            temp.a = alphaInterval;
            _spriteRenderer.color = temp;
        }
    }
}