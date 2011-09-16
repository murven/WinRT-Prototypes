using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASD.MemoryGame.ViewModels
{
	public class ViewModelLocator
	{

		public MemoryBoardViewModel MemoryBoardViewModel
		{
			get
			{
				return new MemoryBoardViewModel();
			}
		}


	}
}
