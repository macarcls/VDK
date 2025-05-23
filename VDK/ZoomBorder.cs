﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace VDK
{
    class ZoomBorder : Border
    {
        private UIElement? child = null;
        private Point origin;
        private Point start;

        // TT служит для перемещения изобр.
        private TranslateTransform GetTranslateTransform(UIElement element)
        {
            return (TranslateTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is TranslateTransform);
        }

        // ST служит для масштабирования
        private ScaleTransform GetScaleTransform(UIElement element)
        {
            return (ScaleTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is ScaleTransform);
        }

        private MatrixTransform GetMatrixTransform(UIElement element)
        {
            return (MatrixTransform)((TransformGroup)element.RenderTransform).Children.First(tr => tr is MatrixTransform);
        }

        // конструктор для Child из Border
        public override UIElement Child
        {
            get { return base.Child; }
            set
            {
                if (value != null && value != this.Child)
                    this.Initialize(value);
                base.Child = value;
            }
        }

        // Инициализирует все необходимые элементы для изображения
        public void Initialize(UIElement element)
        {
            this.child = element;
            if (child != null)
            {
                TransformGroup group = new TransformGroup();
                ScaleTransform st = new ScaleTransform();
                group.Children.Add(st);
                TranslateTransform tt = new TranslateTransform();
                group.Children.Add(tt);
                MatrixTransform mt = new MatrixTransform();
                group.Children.Add(mt);
                child.RenderTransform = group;
                child.RenderTransformOrigin = new Point(0.0, 0.0);
                this.MouseWheel += child_MouseWheel;
                this.MouseLeftButtonDown += child_MouseLeftButtonDown;
                this.MouseLeftButtonUp += child_MouseLeftButtonUp;
                this.MouseMove += child_MouseMove;
                this.PreviewMouseRightButtonDown += new MouseButtonEventHandler(
                  child_PreviewMouseRightButtonDown);

                /* Попытка сделать сенсорный ввод */

                this.IsManipulationEnabled = true;
                this.ManipulationStarting += child_ManipulationStarted;
                this.ManipulationDelta += child_ManipulationDelta;
            }
        }

        // Возвращает в исходное состояние
        public void Reset()
        {
            if (child != null)
            {
                // reset zoom
                var st = GetScaleTransform(child);
                st.ScaleX = 1.0;
                st.ScaleY = 1.0;

                // reset pan
                var tt = GetTranslateTransform(child);
                tt.X = 0.0;
                tt.Y = 0.0;
            }
        }

        #region Child Events
        /* Обработчики Mouse */
        
        private void child_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (child != null)
            {
                var st = GetScaleTransform(child);
                var tt = GetTranslateTransform(child);

                double zoom = e.Delta > 0 ? .2 : -.2;
                if (!(e.Delta > 0) && (st.ScaleX < .4 || st.ScaleY < .4))
                    return;

                Point relative = e.GetPosition(child);
                double absoluteX;
                double absoluteY;

                absoluteX = relative.X * st.ScaleX + tt.X;
                absoluteY = relative.Y * st.ScaleY + tt.Y;

                st.ScaleX += zoom;
                st.ScaleY += zoom;

                tt.X = absoluteX - relative.X * st.ScaleX;
                tt.Y = absoluteY - relative.Y * st.ScaleY;
            }
        }

        private void child_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                var tt = GetTranslateTransform(child);
                start = e.GetPosition(this);
                origin = new Point(tt.X, tt.Y);
                this.Cursor = Cursors.Hand;
                child.CaptureMouse();
            }
        }

        private void child_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                child.ReleaseMouseCapture();
                this.Cursor = Cursors.Arrow;
            }
        }

        void child_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Reset();
        }

        private void child_MouseMove(object sender, MouseEventArgs e)
        {
            if (child != null)
            {
                if (child.IsMouseCaptured)
                {
                    var tt = GetTranslateTransform(child);
                    Vector v = start - e.GetPosition(this);
                    tt.X = origin.X - v.X;
                    tt.Y = origin.Y - v.Y;
                }
            }
        }

        /* Обработчики Manipulation */

        private void child_ManipulationStarted(object sender, ManipulationStartingEventArgs e)
        {
            if (child != null)
            {
                e.ManipulationContainer = this;
                e.Handled = true;
            }
        }

        private void child_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            if (child != null)
            {
                Matrix mt = GetMatrixTransform(child).Matrix;

                // Перемещение
                mt.Translate(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y);

                // Масштабирование
                mt.ScaleAt(e.DeltaManipulation.Scale.X,
                           e.DeltaManipulation.Scale.Y,
                           e.ManipulationOrigin.X,
                           e.ManipulationOrigin.Y
                    );
            }
        }

        #endregion
    }
}
