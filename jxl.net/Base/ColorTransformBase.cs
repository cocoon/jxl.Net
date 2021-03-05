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
    public class ColorTransformBase
    {
        //Constructor
        public ColorTransformBase() { }
        private ColorTransformBase(int value) { Value = value; }

        public int Value { get; set; }

        public static ColorTransformBase XYB { get { return new ColorTransformBase(0); } }
        public static ColorTransformBase None { get { return new ColorTransformBase(1); } }
        public static ColorTransformBase YCbCr { get { return new ColorTransformBase(2); } }

    }
}
