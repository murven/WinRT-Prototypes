using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASD.MemoryGame.Lib
{
    public class MemoryCard
    {
        public Guid Id
        {
            get
            {
                return id;
            }
        }
		private Guid id = Guid.NewGuid();

        public Uri ImageUri { get; set; }
    }
}
