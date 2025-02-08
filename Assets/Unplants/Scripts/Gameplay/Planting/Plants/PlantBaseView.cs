using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Unplants.General.Systems.DragDropSystem;
using Unplants.Scripts.General.Extensions;

namespace Unplants.Scripts.Gameplay.Planting.Plants
{
    public class PlantBaseView : MonoBehaviour, IPlantView, 
        IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private SpriteRenderer mainSprite;
        [SerializeField] private new Animation animation;

        public event Action<IDragDropItem, IPointerData> PointerDown;
        public event Action<IDragDropItem, IPointerData> PointerUp;
        public event Action<IDragDropItem, IPointerData> DragBegin;
        public event Action<IDragDropItem, IPointerData> Drag;
        public event Action<IDragDropItem, IPointerData> DragEnd;

        public bool IsVisible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public Transform TransformAbstraction => transform;

        private PointerData _pointerData;

        private void Awake()
        {
            mainSprite.ThrowIfNull();
            animation.ThrowIfNull();
        }

        public void SetView(AnimationClip animationClip)
        {
            animation.ThrowIfNull();

            animation.clip = animationClip;
        }

        public void SetView(Sprite sprite)
        {
            sprite.ThrowIfNull();

            mainSprite.sprite = sprite;
        }

        public void SetView(Color color)
        {
            mainSprite.color = color;
        }

        public void OnDrag(PointerEventData eventData)
        {
            SetPointerDataValues(eventData);
            Drag?.Invoke(this, _pointerData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SetPointerDataValues(eventData);
            PointerDown?.Invoke(this, _pointerData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            SetPointerDataValues(eventData);
            PointerUp?.Invoke(this, _pointerData);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            SetPointerDataValues(eventData);
            DragBegin?.Invoke(this, _pointerData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            SetPointerDataValues(eventData);
            DragEnd?.Invoke(this, _pointerData);
        }

        private void SetPointerDataValues(PointerEventData eventData)
        {
            _pointerData.PointerPos = eventData.position;
            _pointerData.DisplayIndex = eventData.displayIndex;
        }
    }
}