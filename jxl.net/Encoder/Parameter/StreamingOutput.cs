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

using System.Xml.Serialization;

namespace jxlNET.Encoder.Parameters
{

    /// <summary>
    /// "streaming_output, Enable incremental writing of the output file.",
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class StreamingOutput : jxlNET.Parameter
    {
        public override bool? Available => true;
        public override string Description => "streaming_output, Enable incremental writing of the output file.";
        public override string Name => "StreamingOutput";
        public override string Param => ParamLong;
        public override string ParamLong => "--streaming_output";
        public override OptionType OptionType => OptionType.Flag;

    }
}
