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
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace jxlNET.Encoder.Parameters
{
    /// <summary>
    /// "frame_indexing, INDICES, // TODO(tfish): Add a more convenient vanilla alternative. INDICES is of the form '^(0*|1[01]*)'. The i-th position indicates whether the i-th frame will be indexed in the frame index box."
    /// 
    ///Gültig Grund
    ///""  leere Zeichenkette ist erlaubt
    ///0   eine Null
    ///00  zwei Nullen
    ///000000  beliebig viele Nullen
    ///🟩 2. 1[01]* — beginnt mit 1, danach beliebig viele 0 oder 1
    ///Das bedeutet:
    ///
    ///Der String muss mit 1 beginnen
    ///
    ///Danach darf eine beliebige Folge aus 0 und 1 kommen(auch leer)
    ///
    ///Beispiele:
    ///
    ///Gültig Bedeutung
    ///1   nur ein gesetztes Bit
    ///10  Frame 0 indexiert
    ///11	Frame 0 und 1 indexiert
    ///10101	Indexierung an Positionen 0,2,4
    ///100000	nur Frame 0 indexiert
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class FrameIndexing : jxlNET.Parameter
    {
        public override bool? Available => true;
        public override string Description => "frame_indexing, INDICES, // TODO(tfish): Add a more convenient vanilla alternative. INDICES is of the form '^(0*|1[01]*)'. The i-th position indicates whether the i-th frame will be indexed in the frame index box.";
        public override string Name => "FrameIndexing";
        public override string Param => ParamLong;
        public override string ParamLong => "--frame_indexing";
        public override OptionType OptionType => OptionType.Value;

        //Constructor
        public FrameIndexing() { }
        public FrameIndexing(string Value)
        {
            this.Value = Value;
        }

        private string _value = "";

        /// <summary>
        /// Valid values are: ^(0*|1[01]*)
        /// </summary>
        public string Value
        {
            get
            {
                Contract.Requires(IsValidIndices(_value));
                return _value;
            }
            set
            {
                Contract.Requires(IsValidIndices(value));
                _value = value;
            }

        }

        public override string ToString()
        {
            return Param + " " + Value.ToString();
        }

        static readonly Regex IndicesRegex = new Regex(@"^(0*|1[01]*)$", RegexOptions.Compiled);

        bool IsValidIndices(string value)
        {
            return IndicesRegex.IsMatch(value);
        }
    }
}
