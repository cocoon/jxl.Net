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
    /// "Disable/enable storing JPEG reconstruction metadata with lossless JPEG transcoding, default = 1. 0 = disable. 1 = enable. --allow_jpeg_reconstruction=0"
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class AllowJpegReconstruction : jxlNET.Parameter
    {
        public override string Description => "Disable/enable storing JPEG reconstruction metadata with lossless JPEG transcoding, default = 1. 0 = disable. 1 = enable. --allow_jpeg_reconstruction=0";
        public override string Name => "Gaborish";
        public override string Param => ParamLong;
        public override string ParamLong => "--allow_jpeg_reconstruction";
        public override OptionType OptionType => OptionType.Value;

        //Constructor
        public AllowJpegReconstruction() { }
        public AllowJpegReconstruction(int Value)
        {
            this.Value = Value;
        }

        [XmlIgnoreAttribute]
        public int MinValue = 0;
        [XmlIgnoreAttribute]
        public int MaxValue = 1;

        private int _value = 0;

        /// <summary>
        /// Valid values are: [0|1]
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
                    throw new ArgumentOutOfRangeException("Valid values are: [" + MinValue + ":" + MaxValue + "]");
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
