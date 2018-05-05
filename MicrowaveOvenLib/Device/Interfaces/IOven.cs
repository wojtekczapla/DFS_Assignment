namespace MicrowaveOvenLib.Device.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IOven
    {
        ILight Light { get; }
    }
}
