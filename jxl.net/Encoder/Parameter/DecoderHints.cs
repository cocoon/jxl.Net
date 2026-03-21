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
using System.Xml.Serialization;

namespace jxlNET.Encoder.Parameters
{
    /// <summary>
    /// "x, dec-hints, key=value,\r        Use with 'raw' formats like PPM which do not store colorspace \r\n        information\r\n            and metadata, or to strip or modify metadata from formats that \r\n        do.\r\n            The key 'color_space' indicates an enumerated ColorEncoding, for \r\n        example:\r\n              -x color_space=RGB_D65_SRG_Per_SRG is sRGB with perceptual \r\n        rendering intent\r\n              -x color_space=RGB_D65_202_Rel_PeQ is Rec.2100 PQ with relative \r\n        \"rendering intent\r\n            Shorthands: sRGB, DisplayP3, Adobe98, Rec2100PQ, Rec2100HLG\r\n            The key 'icc_pathname' refers to a binary file containing an ICC \r\n        profile.\r\n            The keys 'exif', 'xmp', and 'jumbf' refer to a binary file \r\n        containing metadata;\r\n            existing metadata of the same type will be overwritten.\r\n            Specific metadata can be stripped using e.g. -x strip=exif.\r\n            Stripping metadata with lossless JPEG recompression won't \r\n        allow reconstruction,\r\n            hence `--allow_jpeg_reconstruction=0` must be passed in this \r\n        case."
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class DecoderHints : jxlNET.Parameter
    {
        public override string Description => "x, dec-hints, key=value,\r        Use with 'raw' formats like PPM which do not store colorspace \r\n        information\r\n            and metadata, or to strip or modify metadata from formats that \r\n        do.\r\n            The key 'color_space' indicates an enumerated ColorEncoding, for \r\n        example:\r\n              -x color_space=RGB_D65_SRG_Per_SRG is sRGB with perceptual \r\n        rendering intent\r\n              -x color_space=RGB_D65_202_Rel_PeQ is Rec.2100 PQ with relative \r\n        \"rendering intent\r\n            Shorthands: sRGB, DisplayP3, Adobe98, Rec2100PQ, Rec2100HLG\r\n            The key 'icc_pathname' refers to a binary file containing an ICC \r\n        profile.\r\n            The keys 'exif', 'xmp', and 'jumbf' refer to a binary file \r\n        containing metadata;\r\n            existing metadata of the same type will be overwritten.\r\n            Specific metadata can be stripped using e.g. -x strip=exif.\r\n            Stripping metadata with lossless JPEG recompression won't \r\n        allow reconstruction,\r\n            hence `--allow_jpeg_reconstruction=0` must be passed in this \r\n        case.";
        public override string Name => "DecoderHints";
        public override string Param => "-x";
        public override string ParamLong => "--dec-hints";
        public override OptionType OptionType => OptionType.Value;

        //Constructor
        public DecoderHints() { }
        public DecoderHints(string Vaule) { this.Value = Value; }

        private string _value = String.Empty;

        /// <summary>
        /// Valid values are: [string]
        /// </summary>
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }

        }

        public override string ToString()
        {
            return Param + " " + Value;
        }


    }
}
