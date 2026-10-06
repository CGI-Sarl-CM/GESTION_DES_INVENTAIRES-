using System;
using System.Collections.Generic;
using System.Text;

namespace Mystore.Interfaces;

public interface IAppUtil
{
    public Shell GetShell();
    public Window LoadWindow();
}