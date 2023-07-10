using System;
using DG.Tweening;
using UnityEngine;

namespace BoardElements
{
    public class BoardDynamicElementAnimation : MonoBehaviour
    {
        private Sequence _appearSequence;

        public void Appear(Action OnAppear = null)
        {
            _appearSequence.Kill();

            _appearSequence = DOTween.Sequence()
                    .Append(this.transform.DOScale(new Vector3(1, 1, 1), 1f))
                    .AppendCallback(() =>
                    {
                        OnAppear?.Invoke();
                    })
                    ;
        }
        
        public void Disappear(Action OnDisappered = null)
        {
            _appearSequence.Kill();

            _appearSequence = DOTween.Sequence()
                    .Append(transform.DOScale(Vector3.zero, 1f))
                    .AppendCallback(() =>
                    {
                        OnDisappered?.Invoke();
                    })
                    ;
        }

        private void OnDestroy()
        {
            _appearSequence.Kill();
        }
    }
}