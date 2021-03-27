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

namespace jxlNET.Decoder.Parameters
{

    /// <summary>
    /// 'j', "pixels_to_jpeg",
    ///  By default, if the input JPEG XL contains a recompressed JPEG file, 
    ///  djxl reconstructs the exact original JPEG file. This flag causes the decoder 
    ///  to instead decode the image to pixels and encode a new (lossy) JPEG. 
    ///  The output file if provided must be a .jpg or .jpeg file.,
    ///  &amp;decode_to_pixels, &amp;SetBooleanTrue);
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Decoder.Parameters")]
    public class JPEG : jxlNET.Parameter
    {
        public override string Description => "j, pixels_to_jpeg, By default, if the input JPEG XL contains a recompressed JPEG file, djxl reconstructs the exact original JPEG file. This flag causes the decoder to instead decode the image to pixels and encode a new (lossy) JPEG. The output file if provided must be a .jpg or .jpeg file.";
        public override string Name => "JPEG";
        public override string Param => "-j";
        public override string ParamLong => "--pixels_to_jpeg";
        public override OptionType OptionType => OptionType.Flag;

        string MinVersion = "0.3.3"; //changed in 0.3.3: https://gitlab.com/wg1/jpeg-xl/-/commit/b1c6fdcd3f6f2720daf3eb953761f1087206f981?page=2

    }
}
