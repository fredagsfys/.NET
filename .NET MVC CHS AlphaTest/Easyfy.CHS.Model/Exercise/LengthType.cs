using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easyfy.CHS.Model.Exercise
{
    public enum LengthType
    {
        [Description("m")]
        Meter = 1,

        [Description("mile")]
        Mile = 2,

        [Description("micrometer")]
        Millimeter = 3,

        [Description("cm")]
        Centimeter = 4,

        [Description("km")]
        KiloMeter = 5,

        [Description("feet")]
        Foot = 6,
    }
}
