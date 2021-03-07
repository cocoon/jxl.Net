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
    /// '\0', "allow_more_progressive_steps", allow decoding more progressive steps in truncated files. No effect without --allow_partial_files
    /// </summary>
    public class AllowMoreProgressiveSteps : jxlNET.Parameter
    {
        public override string Description => "\0, allow_more_progressive_steps, allow decoding more progressive steps in truncated files. No effect without --allow_partial_files";
        public override string Name => "AllowMoreProgressiveSteps";
        public override string Param => ParamLong;
        public override string ParamLong => "--allow_more_progressive_steps";
        public override OptionType OptionType => OptionType.Flag;

        

    }
}
