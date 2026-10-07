using System;
using System.Collections.Generic;
using System.Text;

namespace CGIERP.Interfaces;

public interface IAppUtil
{
    public Shell GetShell();
    public Window LoadWindow();
}