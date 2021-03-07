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
    /// '\0', "use_sjpeg", use sjpeg instead of libjpeg for JPEG output
    /// </summary>
    public class UseSJpeg : jxlNET.Parameter
    {
        public override string Description => "\0, use_sjpeg, use sjpeg instead of libjpeg for JPEG output";
        public override string Name => "UseSJpeg";
        public override string Param => ParamLong;
        public override string ParamLong => "--use_sjpeg";
        public override OptionType OptionType => OptionType.Flag;


    }
}
