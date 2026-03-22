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
using System.Globalization;
using System.Xml.Serialization;

namespace jxlNET.Encoder.Parameters
{

    /// <summary>
    /// "allow_expert_options,\r\n        Allow setting effort to 11 for somewhat denser lossless \r\n        compression at an extreme compute cost."
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class AllowExpertOptions : jxlNET.Parameter
    {
        public override bool? Available => true;
        public override string Description => "allow_expert_options,\r\n        Allow setting effort to 11 for somewhat denser lossless \r\n        compression at an extreme compute cost.";
        public override string Name => "AllowExpertOptions";
        public override string Param => ParamLong;
        public override string ParamLong => "--allow_expert_options";
        public override OptionType OptionType => OptionType.Flag;
    }
}
