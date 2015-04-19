using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace ComposIT.View.Rendering
{
    /// <summary>
    /// Randomizer for the color palette
    /// </summary>
    public class RandomColor
    {
        /// <summary>
        /// Internal randomizer
        /// </summary>
        private Random _random;


        /// <summary>
        /// Contructor which sets the randomizer
        /// </summary>
        public RandomColor()
        {
             _random = new Random();   
        }

        /// <summary>
        /// Returns a random single color Brush
        /// </summary>
        /// <returns></returns>
        public SolidColorBrush Shuffle()
        {
            return new SolidColorBrush(new Color()
            {
              A  = 255,
              R = Byte.Parse(_random.Next(0, 255).ToString()),
              G = Byte.Parse(_random.Next(0, 255).ToString()),
              B = Byte.Parse(_random.Next(0, 255).ToString())
            });
        }
    }
}
