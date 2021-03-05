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
    public class ColorSpaceName
    {
        //Constructor
        public ColorSpaceName() {}
        private ColorSpaceName(string value) { Value = value; }

        public string Value { get; set; }

        public static ColorSpaceName JXL_COLOR_SPACE_UNKNOWN { get { return new ColorSpaceName("JXL_COLOR_SPACE_UNKNOWN"); } }
        public static ColorSpaceName JXL_COLOR_SPACE_RGB { get { return new ColorSpaceName("JXL_COLOR_SPACE_RGB"); } }
        public static ColorSpaceName JXL_COLOR_SPACE_GRAY { get { return new ColorSpaceName("JXL_COLOR_SPACE_GRAY"); } }
        public static ColorSpaceName JXL_COLOR_SPACE_XYB { get { return new ColorSpaceName("JXL_COLOR_SPACE_XYB"); } }
    }
}
