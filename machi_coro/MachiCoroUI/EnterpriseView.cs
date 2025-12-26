using Game.Models.Enterprises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachiCoroUI
{
    public class EnterpriseView
    {
        public string Name { get; init; }
        public string ImageName => $"{Name.ToLower()}.png";
    }


}
