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
    /// '\0', "tone_map",
    ///  tone map the image to the luminance range indicated by --display_nits 
    ///  instead of performing a naive 0-1 -&gt; 0-1 conversion,
    ///  &amp;tone_map, &amp;SetBooleanTrue)
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Decoder.Parameters")]
    public class ToneMap : jxlNET.Parameter
    {
        public override string Description => "\0, tone_map, tone map the image to the luminance range indicated by --display_nits instead of performing a naive 0-1 -> 0-1 conversion, &tone_map, &SetBooleanTrue)";
        public override string Name => "ToneMap";
        public override string Param => ParamLong;
        public override string ParamLong => "--tone_map";
        public override OptionType OptionType => OptionType.Flag;

        public string MinVersion = "0.3.3"; // added here: https://gitlab.com/wg1/jpeg-xl/-/commit/b1c6fdcd3f6f2720daf3eb953761f1087206f981?page=2
    }
}
