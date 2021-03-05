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
    public class EdgePreservingFilterLevelBase
    {
        //Constructor
        public EdgePreservingFilterLevelBase() { }
        private EdgePreservingFilterLevelBase(int value) { Value = value; }

        public int Value { get; set; }

        public static EdgePreservingFilterLevelBase Level_minus_1 { get { return new EdgePreservingFilterLevelBase(-1); } }
        public static EdgePreservingFilterLevelBase Level_0 { get { return new EdgePreservingFilterLevelBase(0); } }
        public static EdgePreservingFilterLevelBase Level_1 { get { return new EdgePreservingFilterLevelBase(1); } }
        public static EdgePreservingFilterLevelBase Level_2 { get { return new EdgePreservingFilterLevelBase(2); } }
        public static EdgePreservingFilterLevelBase Level_3 { get { return new EdgePreservingFilterLevelBase(3); } }
    }
}
