// Copyright (c) 2021 github.com/cocoon
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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Xml.Serialization;

namespace jxlNET.Encoder.Parameters
{
    /// <summary>
    /// "progressive_dc", "num_dc_frames" (number-of-recursive-DC-frames, and maybe add that most sane usage will be =1), "Use progressive mode for DC. Sticks a progressive+lossless Modular-encoded initial DC frame inside of the VarDCT-encoded image. Will encode DC as a responsive modular frame.\nHigher values use VarDCT recursively, e.g. =3 does DC (1:8) as VarDCT, with _its_ DC (1:64) also as VarDCT, with _its_ DC (1:512) as responsive modular.\nThat only makes sense for huuuge images."
    /// </summary>
    public class ProgressiveDC : jxlNET.Parameter
    {
        public override string Description => "progressive_dc, num_dc_frames (number-of-recursive-DC-frames, and maybe add that most sane usage will be =1), Use progressive mode for DC.\nSticks a progressive+lossless Modular-encoded initial DC frame inside of the VarDCT-encoded image.\nWill encode DC as a responsive modular frame.\nHigher values use VarDCT recursively, e.g. =3 does DC (1:8) as VarDCT, with _its_ DC (1:64) also as VarDCT, with _its_ DC (1:512) as responsive modular.\nThat only makes sense for huuuge images.";
        public override string Name => "ProgressiveDC";
        public override string Param => ParamLong;
        public override string ParamLong => "--progressive_dc";
        public override OptionType OptionType => OptionType.Value;

        //https://twitter.com/jonsneyers/status/1217192274490208256
        //E.g. if you have a 16 gigapixel image, say 128k by 128k, the DC is 16k by 16k, the DC's DC is 2k by 2k, and the DC's DC's DC is 256x256, which then gets encoded with progressive modular (so you can get a 16x16 mini-icon from the first few bytes of the bitstream)
        //The current encoder takes the whole image in memory so you'll need a huge amount of memory to be able to encode such a thing. Setting --progressive-dc=4 adds one more recursive step to that; it probably only makes sense for terapixel images.

        //Constructor
        public ProgressiveDC() { }
        public ProgressiveDC(int Vaule) { this.Value = Value; }

        [XmlIgnoreAttribute]
        public int MinValue = 1;
        [XmlIgnoreAttribute]
        public int MaxValue = int.MaxValue;

        private int _value = 1;

        /// <summary>
        /// Valid values are: [1:int.MaxValue]
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
