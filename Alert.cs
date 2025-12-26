using System;
using System.Collections.Generic;
using System.Text;

namespace ALERT
{
    public class Alert
    {
        public int cd { get; set; }

        public int type { get; set; }
        public int buttonFlg { get; set; }
        public string? message { get; set; }
        public DateTime recordDate { get; set; }
        public int flg { get; set; }
        public int markasRead { get; set; }
    }
}
