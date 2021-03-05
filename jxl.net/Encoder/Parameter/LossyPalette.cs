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


namespace jxlNET.Encoder.Parameters
{

    /// <summary>
    /// lossy-palette", [modular encoding] quantize to a palette that has fewer entries than 
    /// would be necessary for perfect preservation; for the time being, it is 
    /// recommended to set --palette=0 with this option to use the default palette only
    /// </summary>
    public class LossyPalette : jxlNET.Parameter
    {
        public override string Description => "lossy-palette, [modular encoding] quantize to a palette that has fewer entries than would be necessary for perfect preservation; for the time being, it is recommended to set --palette=0 with this option to use the default palette only";
        public override string Name => "LossyPalette";
        public override string Param => ParamLong;
        public override string ParamLong => "--lossy-palette";
        public override OptionType OptionType => OptionType.Flag;

    }
}
