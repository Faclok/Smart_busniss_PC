using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Model
{
    public interface ILinkToObject
    {
        public string ColumnLink { get; }

        public int Link { get;}
    }
}
