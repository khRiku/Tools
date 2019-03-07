using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


public class AllFileData
{
    public string mStartPath = "";
    public List<OneFileData> mOneFileDataList = new List<OneFileData>();
}

public class OneFileData
{
    public string mRelativePath = "";
    public string mContent  = "";

}

