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
    /// "disable_perceptual_optimizations, Disable perceptual optimizations.",
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class DisablePerceptualOptimizations : jxlNET.Parameter
    {
        public override string Description => "disable_perceptual_optimizations, Disable perceptual optimizations.";
        public override string Name => "DisablePerceptualOptimizations";
        public override string Param => ParamLong;
        public override string ParamLong => "--disable_perceptual_optimizations";
        public override OptionType OptionType => OptionType.Flag;

    }
}
