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
    public class ColorSpaceBase
    {
        //Constructor
        public ColorSpaceBase() { }
        private ColorSpaceBase(int value) { Value = value; }

        public int Value { get; set; }

        public static ColorSpaceBase RGB { get { return new ColorSpaceBase(0); } }
        public static ColorSpaceBase YCoCg { get { return new ColorSpaceBase(1); } }
        /// <summary>
        /// Reversible Color Transform (2-37)
        /// </summary>
        public static ColorSpaceBase RCT1 { get { return new ColorSpaceBase(2); } }
        public static ColorSpaceBase RCT2 { get { return new ColorSpaceBase(3); } }
        public static ColorSpaceBase RCT3 { get { return new ColorSpaceBase(4); } }
        public static ColorSpaceBase RCT4 { get { return new ColorSpaceBase(5); } }
        public static ColorSpaceBase RCT5 { get { return new ColorSpaceBase(6); } }
        public static ColorSpaceBase RCT6 { get { return new ColorSpaceBase(7); } }
        public static ColorSpaceBase RCT7 { get { return new ColorSpaceBase(8); } }
        public static ColorSpaceBase RCT8 { get { return new ColorSpaceBase(9); } }
        public static ColorSpaceBase RCT9 { get { return new ColorSpaceBase(10); } }
        public static ColorSpaceBase RCT10 { get { return new ColorSpaceBase(11); } }
        public static ColorSpaceBase RCT11 { get { return new ColorSpaceBase(12); } }
        public static ColorSpaceBase RCT12 { get { return new ColorSpaceBase(13); } }
        public static ColorSpaceBase RCT13 { get { return new ColorSpaceBase(14); } }
        public static ColorSpaceBase RCT14 { get { return new ColorSpaceBase(15); } }
        public static ColorSpaceBase RCT15 { get { return new ColorSpaceBase(16); } }
        public static ColorSpaceBase RCT16 { get { return new ColorSpaceBase(17); } }
        public static ColorSpaceBase RCT17 { get { return new ColorSpaceBase(18); } }
        public static ColorSpaceBase RCT18 { get { return new ColorSpaceBase(19); } }
        public static ColorSpaceBase RCT19 { get { return new ColorSpaceBase(20); } }
        public static ColorSpaceBase RCT20 { get { return new ColorSpaceBase(21); } }
        public static ColorSpaceBase RCT21 { get { return new ColorSpaceBase(22); } }
        public static ColorSpaceBase RCT22 { get { return new ColorSpaceBase(23); } }
        public static ColorSpaceBase RCT23 { get { return new ColorSpaceBase(24); } }
        public static ColorSpaceBase RCT24 { get { return new ColorSpaceBase(25); } }
        public static ColorSpaceBase RCT25 { get { return new ColorSpaceBase(26); } }
        public static ColorSpaceBase RCT26 { get { return new ColorSpaceBase(27); } }
        public static ColorSpaceBase RCT27 { get { return new ColorSpaceBase(28); } }
        public static ColorSpaceBase RCT28 { get { return new ColorSpaceBase(29); } }
        public static ColorSpaceBase RCT29 { get { return new ColorSpaceBase(30); } }
        public static ColorSpaceBase RCT30 { get { return new ColorSpaceBase(31); } }
        public static ColorSpaceBase RCT31 { get { return new ColorSpaceBase(32); } }
        public static ColorSpaceBase RCT32 { get { return new ColorSpaceBase(33); } }
        public static ColorSpaceBase RCT33 { get { return new ColorSpaceBase(34); } }
        public static ColorSpaceBase RCT34 { get { return new ColorSpaceBase(35); } }
        public static ColorSpaceBase RCT35 { get { return new ColorSpaceBase(36); } }
        public static ColorSpaceBase RCT36 { get { return new ColorSpaceBase(37); } }
    }
}
