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
    /// '\0', "quiet", "silence output (except for errors)"
    /// </summary>
    public class Quiet : jxlNET.Parameter
    {
        public override string Description => "\0, quiet, silence output(except for errors)";
        public override string Name => "Quiet";
        public override string Param => ParamLong;
        public override string ParamLong => "--quiet";
        public override OptionType OptionType => OptionType.Flag;

        string MinVersion = "0.3.3"; //changed in 0.3.3: https://gitlab.com/wg1/jpeg-xl/-/commit/b1c6fdcd3f6f2720daf3eb953761f1087206f981?page=2

    }
}
