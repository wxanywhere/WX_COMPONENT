﻿using System;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WX.Windows.Diagram.Designer
{
    //These attributes identify the types of the named parts that are used for templating
    [TemplatePart(Name = "PART_DragThumb", Type = typeof(DragThumb))]
    [TemplatePart(Name = "PART_ResizeDecorator", Type = typeof(Control))]
    [TemplatePart(Name = "PART_ConnectorDecorator", Type = typeof(Control))]
    [TemplatePart(Name = "PART_ContentPresenter", Type = typeof(ContentPresenter))]
    [TemplatePart(Name = "PART_LinkButton", Type = typeof(Button))]
    public class DesignItem : ContentControl, ISelectable, IGroupable
    {
        private DesignCanvas canvas = null;
        #region ID
        private Guid id;
        public Guid ID
        {
            get { return id; }
        }
        #endregion

        #region ParentID
        public Guid ParentID
        {
            get { return (Guid)GetValue(ParentIDProperty); }
            set { SetValue(ParentIDProperty, value); }
        }
        public static readonly DependencyProperty ParentIDProperty = DependencyProperty.Register("ParentID", typeof(Guid), typeof(DesignItem));
        #endregion

        #region IsGroup
        public bool IsGroup
        {
            get { return (bool)GetValue(IsGroupProperty); }
            set { SetValue(IsGroupProperty, value); }
        }
        public static readonly DependencyProperty IsGroupProperty =
            DependencyProperty.Register("IsGroup", typeof(bool), typeof(DesignItem));
        #endregion

        #region IsSelected Property

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        public static readonly DependencyProperty IsSelectedProperty =
          DependencyProperty.Register("IsSelected",
                                       typeof(bool),
                                       typeof(DesignItem),
                                       new FrameworkPropertyMetadata(false));

        #endregion

        #region DragThumbTemplate Property

        // can be used to replace the default template for the DragThumb
        public static readonly DependencyProperty DragThumbTemplateProperty =
            DependencyProperty.RegisterAttached("DragThumbTemplate", typeof(ControlTemplate), typeof(DesignItem));

        public static ControlTemplate GetDragThumbTemplate(UIElement element)
        {
            return (ControlTemplate)element.GetValue(DragThumbTemplateProperty);
        }

        public static void SetDragThumbTemplate(UIElement element, ControlTemplate value)
        {
            element.SetValue(DragThumbTemplateProperty, value);
        }

        #endregion

        #region ConnectorDecoratorTemplate Property

        // can be used to replace the default template for the ConnectorDecorator
        public static readonly DependencyProperty ConnectorDecoratorTemplateProperty =
            DependencyProperty.RegisterAttached("ConnectorDecoratorTemplate", typeof(ControlTemplate), typeof(DesignItem));

        public static ControlTemplate GetConnectorDecoratorTemplate(UIElement element)
        {
            return (ControlTemplate)element.GetValue(ConnectorDecoratorTemplateProperty);
        }

        public static void SetConnectorDecoratorTemplate(UIElement element, ControlTemplate value)
        {
            element.SetValue(ConnectorDecoratorTemplateProperty, value);
        }

        #endregion

        #region IsDragConnectionOver

        // while drag connection procedure is ongoing and the mouse moves over 
        // this item this value is true; if true the ConnectorDecorator is triggered
        // to be visible, see template
        public bool IsDragConnectionOver
        {
            get { return (bool)GetValue(IsDragConnectionOverProperty); }
            set { SetValue(IsDragConnectionOverProperty, value); }
        }
        public static readonly DependencyProperty IsDragConnectionOverProperty =
            DependencyProperty.Register("IsDragConnectionOver",
                                         typeof(bool),
                                         typeof(DesignItem),
                                         new FrameworkPropertyMetadata(false));

        public Visibility LinkButtonVisibility
        {
            get { return (Visibility)GetValue(LinkButtonVisibilityProperty); }
            set { SetValue(LinkButtonVisibilityProperty, value); }
        }
        public static readonly DependencyProperty LinkButtonVisibilityProperty =
            DependencyProperty.Register("LinkButtonVisibility", typeof(Visibility), typeof(DesignItem), new PropertyMetadata(Visibility.Hidden));


        public Style LinkButtonStyle
        {
            get { return (Style)GetValue(LinkButtonStyleProperty); }
            set { SetValue(LinkButtonStyleProperty, value); }
        }
        public static readonly DependencyProperty LinkButtonStyleProperty =
            DependencyProperty.Register("LinkButtonStyle", typeof(Style), typeof(DesignItem));

        #endregion

        static DesignItem()
        {
            // set the key to reference the style for this control
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
                typeof(DesignItem), new FrameworkPropertyMetadata(typeof(DesignItem)));
        }

        public DesignItem(Guid id)
        {
            this.id = id;
            this.Loaded += new RoutedEventHandler(DesignItem_Loaded);
        }

        public DesignItem()
            : this(Guid.NewGuid())
        {
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);
            if (canvas == null)
            {
                return;
            }
            if (canvas.SelectionService.IsSelectionChanged)
            {
                canvas.SelectionService.RaiseDesignShapeSelectAction();
            }
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            canvas = VisualTreeHelper.GetParent(this) as DesignCanvas;

            // update selection
            if (canvas != null)
            {
                if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != ModifierKeys.None)
                {
                    if (this.IsSelected)
                    {
                        canvas.SelectionService.RemoveFromSelection(this);
                    }
                    else
                    {
                        canvas.SelectionService.AddToSelection(this);
                    }
                }
                else if (!this.IsSelected)
                {
                    canvas.SelectionService.SelectItem(this);
                }
                var designItemInfo = canvas.CurrentDesignItemInfos.First(a=>a.ID==this.ID);
                if (VisualTreeHelperX.FindParentControl<Button>(e.OriginalSource as DependencyObject) == null)
                {
                    if (e.ChangedButton == MouseButton.Left && e.LeftButton == MouseButtonState.Pressed)
                    {
                        if (e.ClickCount == 2)
                        {
                            canvas.RaiseDesignItemMouseDoubleClick(designItemInfo);
                        }
                    }
                    else if (e.ChangedButton == MouseButton.Right && e.RightButton == MouseButtonState.Pressed)
                    {
                        if (e.ClickCount == 1)
                        {
                            canvas.RaiseDesignItemMouseRightClick(designItemInfo);
                        }
                    }
                }
                Focus();
            }
            e.Handled = false;
        }



        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var linkButton = this.Template.FindName("PART_LinkButton", (FrameworkElement)this) as Button;
            if (linkButton == null) return;
            var canvas = VisualTreeHelper.GetParent(this) as DesignCanvas;
            if (this.Content is Shape)
            {
                var designItemInfo = (this.Content as Shape).ShapeInfoUnit.ShapeInfo as DesignItemInfo;
                linkButton.Click += (obj, e) =>
                {
                    canvas.RaiseDesignItemLinkButtonClick(designItemInfo);
                };

                linkButton.MouseDoubleClick += (obj, e) =>
                {
                    canvas.RaiseDesignItemLinkButtonDoubleClick(designItemInfo);
                };
            }
        }

        void DesignItem_Loaded(object sender, RoutedEventArgs e)
        {
            if (base.Template != null)
            {
                ContentPresenter contentPresenter =
                    this.Template.FindName("PART_ContentPresenter", this) as ContentPresenter;
                if (contentPresenter != null)
                {
                    UIElement contentVisual = VisualTreeHelper.GetChild(contentPresenter, 0) as UIElement;
                    if (contentVisual != null)
                    {
                        DragThumb thumb = this.Template.FindName("PART_DragThumb", this) as DragThumb;
                        if (thumb != null)
                        {
                            ControlTemplate template =
                                DesignItem.GetDragThumbTemplate(contentVisual) as ControlTemplate;
                            if (template != null)
                                thumb.Template = template;
                        }
                    }
                }
            }
        }
    }
}