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


namespace jxlNET.Deccoder.Parameters
{

    /// <summary>
    /// j', "jpeg",
    ///  "decode directly to JPEG when possible. Depending on the JPEG XL mode 
    ///  used when encoding this will produce an exact original JPEG file, a 
    ///  lossless pixel image data in a JPEG file or just a similar JPEG than 
    ///  the original image. The output file if provided must be a .jpg or .jpeg 
    ///  file.
    /// </summary>
    public class JPEG_up_to_0_3_2 : jxlNET.Parameter
    {
        public override string Description => "j, jpeg, decode directly to JPEG when possible.\nDepending on the JPEG XL mode used when encoding this will produce an exact original JPEG file, a lossless pixel image data in a JPEG file or just a similar JPEG than the original image.\nThe output file if provided must be a .jpg or .jpeg file.";
        public override string Name => "JPEG";
        public override string Param => "-j";
        public override string ParamLong => "--jpeg";
        public override OptionType OptionType => OptionType.Flag;

        string MaxVersion = "0.3.2"; //changed in 0.3.3: https://gitlab.com/wg1/jpeg-xl/-/commit/b1c6fdcd3f6f2720daf3eb953761f1087206f981?page=2

    }
}
