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
    ///"modular_lossy_palette,\r\n        Use delta palette in a lossy way; it is recommended to also set \r\n        --modular_palette_colors=0 \r\n            with this option to use the default palette only.",
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class ModularLossyPalette : jxlNET.Parameter
    {
        public override bool? Available => true;
        public override string Description => "modular_lossy_palette,\r\n        Use delta palette in a lossy way; it is recommended to also set \r\n        --modular_palette_colors=0 \r\n            with this option to use the default palette only.";
        public override string Name => "ModularLossyPalette";
        public override string Param => ParamLong;
        public override string ParamLong => "--modular_lossy_palette";
        public override OptionType OptionType => OptionType.Flag;

    }
}
