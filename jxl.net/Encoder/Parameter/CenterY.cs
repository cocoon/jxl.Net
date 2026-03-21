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
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Xml.Serialization;

namespace jxlNET.Encoder.Parameters
{
    /// <summary>
    /// "center_y, -1..YSIZE, Set the vertical position of center for center-first group ordering, range: [-1 .. xsize). -1 = middle of the image. Default = -1."
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class CenterY : jxlNET.Parameter
    {
        public override string Description => "center_y, -1..YSIZE, Set the vertical position of center for center-first group ordering, range: [-1 .. xsize). -1 = middle of the image. Default = -1.";
        public override string Name => "CenterY";
        public override string Param => ParamLong;
        public override string ParamLong => "--center_y";
        public override OptionType OptionType => OptionType.Value;

        //Constructor
        public CenterY() { }
        public CenterY(int Value)
        {
            this.Value = Value;
        }

        [XmlIgnoreAttribute]
        public int MinValue = 0;
        [XmlIgnoreAttribute]
        public int MaxValue = int.MaxValue;

        private int _value = 0;

        /// <summary>
        /// Valid values are: [0:2 147 483 647]
        /// </summary>
        public int Value
        {
            get
            {
                Contract.Requires(_value >= MinValue);
                Contract.Requires(_value <= MaxValue);
                return _value;
            }
            set
            {
                Contract.Requires(value >= MinValue);
                Contract.Requires(value <= MaxValue);

                // If image size is known, validate upper bound
                if (_ysize.HasValue)
                {
                    Contract.Requires(value < _ysize.Value, $"center_x must be < image width ({_ysize.Value})");
                }

                if (value < MinValue || value > MaxValue)
                {
                    throw new ArgumentOutOfRangeException("Valid values are: [" + MinValue + ":"+ MaxValue + "]");
                }

                _value = value;
            }

        }

        public override string ToString()
        {
            return Param + " " + Value.ToString();
        }

        private int? _ysize;

        public void ApplyImageSize(int height)
        {
            Contract.Requires(height > 0);

            _ysize = height;

            // Re‑validate current value
            if (Value >= height)
                Value = -1; // fallback to default
        }

    }
}
