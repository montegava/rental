using System;
using System.Collections.Generic;

using System.Text;

namespace ImageProcessing
{
    public enum eOcrEngineMode : int
    {
        TESSERACT_ONLY = 0,
        CUBE_ONLY = 1,
        TESSERACT_CUBE_COMBINED = 2,
        DEFAULT = 3
    }
}
