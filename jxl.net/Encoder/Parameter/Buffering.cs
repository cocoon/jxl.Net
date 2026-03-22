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
using System.Xml.Serialization;

namespace jxlNET.Encoder.Parameters
{
    /// <summary>
    /// "buffering, -1..3,\r\n        How frames are buffered when encoding, which affects memory usage and \r\n        compression.    \r\n        -1 = encoder chooses (default). \r\n        0 = buffer everything (most memory, best compression).    \r\n        1 = stream input for large images, buffer output. \r\n        2 = stream input, buffer output.    \r\n        3 = stream both input and output (least memory, worst compression)"
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class Buffering : jxlNET.Parameter
    {
        public override bool? Available => true;
        public override string Description => "buffering, -1..3,\r\n        How frames are buffered when encoding, which affects memory usage and \r\n        compression.    \r\n        -1 = encoder chooses (default). \r\n        0 = buffer everything (most memory, best compression).    \r\n        1 = stream input for large images, buffer output. \r\n        2 = stream input, buffer output.    \r\n        3 = stream both input and output (least memory, worst compression)";
        public override string Name => "Buffering";
        public override string Param => ParamLong;
        public override string ParamLong => "--photon_noise_iso";
        public override OptionType OptionType => OptionType.Value;

        //Constructor
        public Buffering() { }
        public Buffering(int Value)
        {
            this.Value = Value;
        }

        [XmlIgnoreAttribute]
        public int MinValue = -1;
        [XmlIgnoreAttribute]
        public int MaxValue = 3;

        private int _value = -1;

        /// <summary>
        /// Valid values are: [-1:3]
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


    }
}
