using System;
using BoardElements;
using CodeAnimation;
using UniRx;
using UnityEngine;

namespace Merge.MergeItems
{
    [RequireComponent(typeof(AlphaSpriteRendererHandler), typeof(Collider2D)
                        ,typeof(BoardDynamicElementAnimation))]
    public abstract class MergeItem : DraggableItem
    {
        [field:SerializeField] public string Id { get; private set; }
    
        private AlphaSpriteRendererHandler _alphaHandler;
        private Collider2D _collider2D;
        private BoardDynamicElementAnimation _boardDynamicElementAnimation;

        private void Awake()
        {
            _alphaHandler = GetComponent<AlphaSpriteRendererHandler>();
            _collider2D = GetComponent<Collider2D>();
            _boardDynamicElementAnimation = GetComponent<BoardDynamicElementAnimation>();
        
            OnBeginDragCommand
                .Subscribe(_ =>
                {
                    _alphaHandler.GlowDim();
                    _collider2D.enabled = false;//Такова уж условность, что OnDrop перекрывается коллайдером объекта(
                })
                .AddTo(this)
                ;

            OnEndDragCommand
                .Subscribe(_ =>
                {
                    _alphaHandler.ShineUp();
                    _collider2D.enabled = true;
                })
                .AddTo(this)
                ;
        }

        private void Start()
        {
            transform.localScale = Vector3.zero;
            Appear();
        }

        public void Appear(Action OnAppear = null)
        {
            _boardDynamicElementAnimation.Appear(OnAppear);
        }
        
        public void Disappear(Action OnDisapper = null)
        {
            _boardDynamicElementAnimation.Disappear(OnDisapper);
        }
    }
}