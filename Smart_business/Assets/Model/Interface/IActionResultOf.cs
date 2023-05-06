using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.MultiSetting;

namespace Assets.Model
{
    public interface IActionResultOf<T>
        where T: class, IItemDatabase, new()
    {
        public string Name { get; } 
        public Type Type { get; }
    }
}
