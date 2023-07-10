using DG.Tweening;
using UnityEngine;

namespace CodeAnimations
{
    public class GlowingHint : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteToGlow;

        private Sequence _glowingSequence;
        private Sequence _movingSequence;

        public void StartGlow()
        {
            _glowingSequence.Kill();
            
            _glowingSequence = DOTween.Sequence()
                    .Append(_spriteToGlow.DOFade(0.8f, 1f))
                    .AppendCallback(Glowing)
                ;
        
        }

        public void Glowing()
        {
            _glowingSequence.Kill();

            _glowingSequence = DOTween.Sequence()
                    .Append(_spriteToGlow.DOFade(0.4f, 0.5f))
                    .SetLoops(-1, LoopType.Yoyo)
                ;
        }

        public void Fade()
        {
            _glowingSequence.Kill();

            _glowingSequence = DOTween.Sequence()
                .Append(_spriteToGlow.DOFade(0f, 1f));
        }

        public void MoveTo(Vector3 position)
        {
            _movingSequence.Kill();

            _movingSequence = DOTween.Sequence()
                .Append(transform.DOMove(position, 0.25f));
        }

        private void OnDestroy()
        {
            _glowingSequence.Kill();
            _movingSequence.Kill();
        }
    }
}