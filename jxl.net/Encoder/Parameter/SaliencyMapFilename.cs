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


namespace jxlNET.Encoder.Parameters
{
    /// <summary>
    /// "saliency_map_filename", "STRING"
    /// </summary>
    public class SaliencyMapFilename : jxlNET.Parameter
    {
        public override string Description => "saliency_map_filename, STRING";
        public override string Name => "SaliencyMapFilename";
        public override string Param => ParamLong;
        public override string ParamLong => "--saliency_map_filename";
        public override OptionType OptionType => OptionType.Value;

        //Constructor
        public SaliencyMapFilename() { }
        public SaliencyMapFilename(string Vaule) { this.Value = Value; }

        private string _value = String.Empty;

        /// <summary>
        /// Valid values are: [string]
        /// </summary>
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }

        }

        public override string ToString()
        {
            return Param + " " + Value;
        }


    }
}
