// Copyright (c) 2026 github.com/cocoon
// 
// The copyright notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfExampleCore
{
    public class ImageComparisonSlider : Control
    {
        static ImageComparisonSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ImageComparisonSlider),
                new FrameworkPropertyMetadata(typeof(ImageComparisonSlider)));
        }

        public ImageSource BeforeImage
        {
            get => (ImageSource)GetValue(BeforeImageProperty);
            set => SetValue(BeforeImageProperty, value);
        }

        public static readonly DependencyProperty BeforeImageProperty =
            DependencyProperty.Register(nameof(BeforeImage), typeof(ImageSource), typeof(ImageComparisonSlider));

        public ImageSource AfterImage
        {
            get => (ImageSource)GetValue(AfterImageProperty);
            set => SetValue(AfterImageProperty, value);
        }

        public static readonly DependencyProperty AfterImageProperty =
            DependencyProperty.Register(nameof(AfterImage), typeof(ImageSource), typeof(ImageComparisonSlider));

        public double DividerPosition
        {
            get => (double)GetValue(DividerPositionProperty);
            set => SetValue(DividerPositionProperty, value);
        }

        public static readonly DependencyProperty DividerPositionProperty =
            DependencyProperty.Register(nameof(DividerPosition), typeof(double), typeof(ImageComparisonSlider),
                new FrameworkPropertyMetadata(0.5, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        // Template parts
        private FrameworkElement _afterImage;
        private RectangleGeometry _clip;
        private FrameworkElement _dividerLine;
        private Slider _slider;

        // Drag
        private bool _isDraggingDivider = false;
        private Point _dragStartPoint;
        private double _dragStartDividerPosition;


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _afterImage = GetTemplateChild("PART_AfterImage") as FrameworkElement;
            _clip = GetTemplateChild("PART_ClipGeometry") as RectangleGeometry;
            _dividerLine = GetTemplateChild("PART_DividerLine") as FrameworkElement;
            _slider = GetTemplateChild("PART_Slider") as Slider;

            if (_slider != null)
                _slider.ValueChanged += (s, e) => UpdateDivider();

            if (_afterImage != null)
                _afterImage.SizeChanged += (s, e) => UpdateDivider();

            if (_dividerLine != null)
            {
                _dividerLine.MouseLeftButtonDown += Divider_MouseLeftButtonDown;
                _dividerLine.MouseLeftButtonUp += Divider_MouseLeftButtonUp;
                _dividerLine.MouseMove += Divider_MouseMove;
                _dividerLine.LostMouseCapture += Divider_LostMouseCapture;
            }

            // Extra safety
            this.MouseLeftButtonUp += Control_MouseLeftButtonUp;
            this.MouseLeave += Control_MouseLeave;

            // Tunneling version (strongest)
            this.AddHandler(MouseLeftButtonUpEvent,
                new MouseButtonEventHandler(Control_MouseLeftButtonUp), true);


            var zoomBorder = GetTemplateChild("PART_ZoomBorder") as ZoomBorder;

            if (zoomBorder != null)
                zoomBorder.TransformChanged += (s, e) => UpdateDivider();

            // ⭐ Set initial divider position here
            DividerPosition = 0.05; // 5% visible

            UpdateDivider();
        }

        private void Divider_LostMouseCapture(object sender, MouseEventArgs e)
        {
            if (_isDraggingDivider)
            {
                _isDraggingDivider = false;
            }
        }

        private void Control_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isDraggingDivider)
            {
                _isDraggingDivider = false;
                _dividerLine?.ReleaseMouseCapture();
            }
        }

        private void Control_MouseLeave(object sender, MouseEventArgs e)
        {
            if (_isDraggingDivider && Mouse.LeftButton == MouseButtonState.Released)
            {
                _isDraggingDivider = false;
                _dividerLine?.ReleaseMouseCapture();
            }
        }


        private void Divider_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_afterImage == null)
                return;

            e.Handled = true; // 🔥 Prevent ZoomBorder from receiving the event

            _isDraggingDivider = true;
            _dragStartPoint = e.GetPosition(_afterImage);
            _dragStartDividerPosition = DividerPosition;

            _dividerLine.CaptureMouse();
        }

        private void Divider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true; // 🔥 Prevent ZoomBorder from receiving mouse up

            _isDraggingDivider = false;
            _dividerLine.ReleaseMouseCapture();
        }


        private void Divider_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDraggingDivider || _afterImage == null)
                return;

            e.Handled = true;

            // Mouse position in control coordinates
            Point pos = e.GetPosition(this);

            // Get image bounds
            Rect bounds = GetImageBoundsOnScreen();

            // Clamp X to image area
            double clampedX = Math.Max(bounds.Left, Math.Min(bounds.Right, pos.X));

            // Convert clamped X back to image-relative coordinate
            Point imagePos = this.TranslatePoint(new Point(clampedX, 0), _afterImage);

            double newPos = imagePos.X / _afterImage.ActualWidth;
            newPos = Math.Max(0, Math.Min(1, newPos));

            DividerPosition = newPos;
            UpdateDivider();
        }



        private void UpdateDivider()
        {
            if (_afterImage == null || _clip == null || _dividerLine == null)
                return;

            Rect imageBounds = GetImageBoundsOnScreen();
            Rect controlBounds = GetControlBounds();

            double imageWidth = _afterImage.ActualWidth;
            double imageHeight = _afterImage.ActualHeight;

            // Divider position in image coordinates
            double xImage = DividerPosition * imageWidth;

            // Convert to control coordinates
            Point screenPoint = _afterImage.TranslatePoint(new Point(xImage, 0), this);
            double dividerX = screenPoint.X;

            // Check if divider is inside BOTH image and control bounds
            bool insideImage = dividerX >= imageBounds.Left && dividerX <= imageBounds.Right;
            bool insideControl = dividerX >= controlBounds.Left && dividerX <= controlBounds.Right;

            bool visible = insideImage && insideControl;

            _dividerLine.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;

            if (visible)
            {
                _dividerLine.Margin = new Thickness(dividerX, 0, 0, 0);
            }

            // Update clip (always in image coordinates)
            _clip.Rect = new Rect(0, 0, xImage, imageHeight);
        }



        private Rect GetImageBoundsOnScreen()
        {
            if (_afterImage == null)
                return Rect.Empty;

            // Top-left corner of the image in control coordinates
            Point topLeft = _afterImage.TranslatePoint(new Point(0, 0), this);

            // Bottom-right corner
            Point bottomRight = _afterImage.TranslatePoint(
                new Point(_afterImage.ActualWidth, _afterImage.ActualHeight), this);

            return new Rect(topLeft, bottomRight);
        }

        private Rect GetControlBounds()
        {
            return new Rect(0, 0, ActualWidth, ActualHeight);
        }







    }
}
