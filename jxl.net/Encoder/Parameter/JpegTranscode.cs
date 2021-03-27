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
    /// 'j', "jpeg_transcode","Do lossy transcode of input JPEG file (decode to pixels instead of doing lossless transcode)."
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class JpegTranscode : jxlNET.Parameter
    {
        public override string Description => "j, jpeg_transcode, Do lossy transcode of input JPEG file(decode to pixels instead of doing lossless transcode).";
        public override string Name => "JpegTranscode";
        public override string Param => "-j";
        public override string ParamLong => "--jpeg_transcode";
        public override OptionType OptionType => OptionType.Flag;

    }
}
