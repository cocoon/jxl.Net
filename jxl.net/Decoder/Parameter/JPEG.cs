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
    public class JPEG : jxlNET.Parameter
    {
        public override string Description => "j, jpeg, decode directly to JPEG when possible.\nDepending on the JPEG XL mode used when encoding this will produce an exact original JPEG file, a lossless pixel image data in a JPEG file or just a similar JPEG than the original image.\nThe output file if provided must be a .jpg or .jpeg file.";
        public override string Name => "JPEG";
        public override string Param => "-j";
        public override string ParamLong => "--jpeg";
        public override OptionType OptionType => OptionType.Flag;

    }
}
