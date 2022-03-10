using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject
{
    public class Gender
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public Gender(string type)
        {
            this.Type = type;
        }

        public override string ToString() => Type;
    }
}
