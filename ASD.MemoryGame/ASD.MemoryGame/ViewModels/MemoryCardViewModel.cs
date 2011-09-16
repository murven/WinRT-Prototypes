namespace ASD.MemoryGame.ViewModels
{
	using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASD.Infrastructure;
using ASD.MemoryGame.Lib;
using Microsoft.Practices.Prism.Commands;
using Windows.UI.Xaml.Input;
	public class MemoryCardViewModel : ViewModelBase
	{

		#region MemoryCard (INotifyPropertyChanged Property)
		private MemoryCard memoryCard;

		public MemoryCard MemoryCard
		{
			get { return memoryCard; }
			set
			{
				if (memoryCard != value)
				{
					memoryCard = value;
					ImageUriString = memoryCard.ImageUri.ToString();
				}
			}
		}
		#endregion

		#region CurrentState (INotifyPropertyChanged Property)
		private MemoryCardState currentState;

		public MemoryCardState CurrentState
		{
			get { return currentState; }
			set
			{
				if (currentState != value)
				{
					currentState = value;
					OnPropertyChanged("CurrentState");
				}
			}
		}
		#endregion

		#region ToggleCommand (INotifyPropertyChanged Property)
		private ICommand toggleCommand;

		public ICommand ToggleCommand
		{
			get { return toggleCommand; }
			set
			{
				if (toggleCommand != value)
				{
					toggleCommand = value;
					OnPropertyChanged("ToggleCommand");
				}
			}
		}
		#endregion

		#region ImageUriString (INotifyPropertyChanged Property)
		private string imageUriString;

		public string ImageUriString
		{
			get { return imageUriString; }
			set
			{
				if (imageUriString != value)
				{
					imageUriString = value;
					OnPropertyChanged("ImageUriString");
				}
			}
		}
		#endregion

		#region CardBackgroundImageUriString (INotifyPropertyChanged Property)
		private string cardBackgroundImageUriString;

		public string CardBackgroundImageUriString
		{
			get { return cardBackgroundImageUriString; }
			set
			{
				if (cardBackgroundImageUriString != value)
				{
					cardBackgroundImageUriString = value;
					OnPropertyChanged("CardBackgroundImageUriString");
				}
			}
		}
		#endregion



		protected override void OnInitialize()
		{
			ToggleCommand = new DelegateCommand(OnToggle);
		}

		public void Initialize(Uri imageUri, string backgroundUriString)
		{
			if (MemoryCard == null)
			{
				MemoryCard = new MemoryCard();
			}
			MemoryCard.ImageUri = imageUri;
			ImageUriString = imageUri.ToString();
			CardBackgroundImageUriString = backgroundUriString;
		}

		public void Initialize(MemoryCard memoryCard, string backgroundUriString)
		{
			MemoryCard = memoryCard;
			CardBackgroundImageUriString = backgroundUriString;
		}

		void OnToggle()
		{
			switch (CurrentState)
			{
				case MemoryCardState.BackState:
					CurrentState = MemoryCardState.FrontState;
					break;
				case MemoryCardState.FrontState:
					CurrentState = MemoryCardState.BackState;
					break;
				default:
					break;
			}
		}

	}
}
