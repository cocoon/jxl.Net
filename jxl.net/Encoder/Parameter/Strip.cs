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

using System.Xml.Serialization;

namespace jxlNET.Encoder.Parameters
{

    /// <summary>
    /// "strip","Do not encode using container format (strips Exif/XMP/JPEG bitstream reconstruction data)"
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class Strip : jxlNET.Parameter
    {
        public override string Description => "strip, Do not encode using container format (strips Exif/XMP/JPEG bitstream reconstruction data)";
        public override string Name => "Strip";
        public override string Param => ParamLong;
        public override string ParamLong => "--strip";
        public override OptionType OptionType => OptionType.Flag;

        string MinVersion = "0.3.4"; //added in 0.3.4: https://gitlab.com/wg1/jpeg-xl/-/commit/f2aeba7e3d091314eaccb64748791e1f4e2cf0a6?expanded=1&page=2#cfde100f1256fa3ef950648f50b0968da2c55a00_302_308
    }
}
