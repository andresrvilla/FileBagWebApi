using System;
using System.Collections.Generic;
using System.Text;

namespace FileBagWebApi.ServiceClasses.Interfaces
{
    public interface IDTO<TBe, TDTO>
    {
        TBe GetBE();

        TDTO Build(TBe be);
    }
}
