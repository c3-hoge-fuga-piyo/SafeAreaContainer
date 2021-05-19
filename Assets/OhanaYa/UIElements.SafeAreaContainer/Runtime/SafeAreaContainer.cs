#if (UNITY_2021_2_OR_NEWER && SAFE_AREA_CONTAINER_UI_ELEMENTS_MODULE_ENABLED) || SAFE_AREA_CONTAINER_UI_TOOLKIT_PACKAGE_INSTALLED
#define SAFE_AREA_CONTAINER_UI_TOOLKIT_SUPPORT
#endif

#if SAFE_AREA_CONTAINER_UI_TOOLKIT_SUPPORT
using UnityEngine;
using UnityEngine.UIElements;

namespace OhanaYa.UIElements
{
    public sealed class SafeAreaContainer : VisualElement
    {
        #region Constructors
        public SafeAreaContainer() : base()
        {
            {
                this.style.position = Position.Absolute;
                this.style.top = 0;
                this.style.left = 0;
                this.style.width = Length.Percent(100);
                this.style.height = Length.Percent(100);
            }

            this.RegisterCallback<GeometryChangedEvent>(this.OnGeometryChanged);
        }
        #endregion

        void OnGeometryChanged(GeometryChangedEvent e)
        {
            var panel = this.panel;

#if UNITY_EDITOR
            if (panel.contextType == ContextType.Editor)
            {
                //TODO: Is it possible to do the same in the editor?
                return;
            }
#endif

            var safeArea = Screen.safeArea;

            var lt = RuntimePanelUtils.ScreenToPanel(panel, new Vector2(safeArea.xMin, Screen.height - safeArea.yMax));
            var rb = RuntimePanelUtils.ScreenToPanel(panel, new Vector2(Screen.width - safeArea.xMax, safeArea.yMin));

            {
                this.style.paddingLeft = lt.x;
                this.style.paddingTop = lt.y;
                this.style.paddingRight = rb.x;
                this.style.paddingBottom = rb.y;
            }
        }

        #region VisualElement
        public new class UxmlFactory : UxmlFactory<SafeAreaContainer, VisualElement.UxmlTraits> {}
        #endregion
    }
}
#endif
