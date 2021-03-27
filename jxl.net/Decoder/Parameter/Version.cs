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

namespace jxlNET.Decoder.Parameters
{

    /// <summary>
    /// 'V', "version", "print version number and exit
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Decoder.Parameters")]
    public class Version : jxlNET.Parameter
    {
        public override string Description => "V, version, print version number and exit";
        public override string Name => "Version";
        public override string Param => "-V";
        public override string ParamLong => "--version";
        public override OptionType OptionType => OptionType.Flag;

    }
}
