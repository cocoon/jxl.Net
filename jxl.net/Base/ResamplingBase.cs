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
using System.Text;

namespace jxlNET
{
    public class ResamplingBase
    {
        //Constructor
        public ResamplingBase() { }
        private ResamplingBase(int value) { Value = value; }

        public int Value { get; set; }

        public static ResamplingBase Sampling_1 { get { return new ResamplingBase(1); } }
        public static ResamplingBase Sampling_2 { get { return new ResamplingBase(2); } }
        public static ResamplingBase Sampling_4 { get { return new ResamplingBase(4); } }
        public static ResamplingBase Sampling_8 { get { return new ResamplingBase(8); } }
    }
}
