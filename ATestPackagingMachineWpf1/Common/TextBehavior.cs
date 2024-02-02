using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using System.Windows.Media;

namespace ATestPackagingMachineWpf1.Common
{
    public class TextBehavior : Behavior<TextBlock>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseEnter += AssociatedObject_MouseEnter;
            AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseEnter -= AssociatedObject_MouseEnter;
            AssociatedObject.MouseLeave -= AssociatedObject_MouseLeave;
        }

        private void AssociatedObject_MouseEnter(object sender,
            System.Windows.Input.MouseEventArgs e)
        {
            var element = sender as TextBlock;
            DropShadowEffect effect = new DropShadowEffect();
            effect.Color = Colors.Gold;
            effect.ShadowDepth = 0;
            effect.BlurRadius = 15;
            element.Effect = effect;
        }

        private void AssociatedObject_MouseLeave(object sender,
            System.Windows.Input.MouseEventArgs e)
        {
            var element = sender as TextBlock;
            DropShadowEffect effect = new DropShadowEffect();
            effect.Color = Colors.Gold;
            effect.ShadowDepth = 0;
            effect.BlurRadius = 15;
            element.Effect = null;
        }
    }
}
