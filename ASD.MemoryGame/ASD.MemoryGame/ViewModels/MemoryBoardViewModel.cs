using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASD.Infrastructure;
using ASD.MemoryGame.Lib;
using Microsoft.Practices.Prism.Commands;
using Windows.UI.Xaml.Input;

namespace ASD.MemoryGame.ViewModels
{
	public class MemoryBoardViewModel : ViewModelBase
	{


		#region FoundCardCount (INotifyPropertyChanged Property)
		private int foundCardCount;

		public int FoundCardCount
		{
			get { return foundCardCount; }
			set
			{
				if (foundCardCount != value)
				{
					foundCardCount = value;
					OnPropertyChanged("FoundCardCount");
				}
			}
		}
		#endregion

		#region MoveCount (INotifyPropertyChanged Property)
		private int moveCount;

		public int MoveCount
		{
			get { return moveCount; }
			set
			{
				if (moveCount != value)
				{
					moveCount = value;
					OnPropertyChanged("MoveCount");
				}
			}
		}
		#endregion


		MemoryCardViewModel firstSelectedCard;
		MemoryCardViewModel secondSelectedCard;

		public void SelectCard(MemoryCardViewModel cardViewModel)
		{
			if (firstSelectedCard == null)
			{
				firstSelectedCard = cardViewModel;
			}
			else if (secondSelectedCard == null)
			{
				secondSelectedCard = cardViewModel;
				MoveCount++;
			}
			else if (secondSelectedCard.MemoryCard.Id == firstSelectedCard.MemoryCard.Id)
			{
				FoundCardCount++;
				firstSelectedCard = cardViewModel;
				secondSelectedCard = null;
			}
			else
			{
				var firstCard = firstSelectedCard;
				var secondCard = secondSelectedCard;
				firstCard.IsSelected = false;
				secondCard.IsSelected = false;
				firstSelectedCard = cardViewModel;
				secondSelectedCard = null;
			}
		}

		Uri[] defaultCardUriArray = new Uri[] 
		{
			new Uri("http://thewholegardenwillbow.files.wordpress.com/2010/12/mantis.jpg"),
			new Uri("http://www.indianaturewatch.net/images/album/photo/16068245264ba88e8cf16eb.jpg"),
			new Uri("http://www.dharmaflix.com/w/images/9/98/Blue_morpho_butterfly1.jpg"),
			new Uri("http://wallpapersinfinity.files.wordpress.com/2010/08/butterfly.jpg"),
			new Uri("http://suziesden.com/wp-content/uploads/2010/07/butterfly.jpg"),
			new Uri("http://upload.wikimedia.org/wikipedia/commons/2/24/Red_butterfly.jpg"),
			new Uri("http://www.ceffect.com/wp-content/uploads/2009/01/butterfly-nolegs-2.jpg"),
			new Uri("http://www.freeimageslive.co.uk/files/images005/blue_butterfly.jpg"),
		};

		static readonly Random randomGenerator = new Random();

		bool isCreatingNewGame = false;

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


		#region CardCount (INotifyPropertyChanged Property)
		private MemoryGameCardCount cardCount;

		public MemoryGameCardCount CardCount
		{
			get { return cardCount; }
			set
			{
				if (cardCount != value)
				{
					cardCount = value;
					OnPropertyChanged("CardCount");
				}
			}
		}
		#endregion
		#region NewGameCommand (INotifyPropertyChanged Property)
		private ICommand newGameCommand;

		public ICommand NewGameCommand
		{
			get { return newGameCommand; }
			set
			{
				if (newGameCommand != value)
				{
					newGameCommand = value;
					OnPropertyChanged("NewGameCommand");
				}
			}
		}
		#endregion


		#region CurrentGameCards (INotifyPropertyChanged Property)
		private List<MemoryCardViewModel> currentGameCards;

		public List<MemoryCardViewModel> CurrentGameCards
		{
			get { return currentGameCards; }
			set
			{
				if (currentGameCards != value)
				{
					currentGameCards = value;
					OnPropertyChanged("CurrentGameCards");
				}
			}
		}
		#endregion


		protected override void OnInitialize()
		{
			NewGameCommand = new DelegateCommand(OnNewGame, CanCreateNewGame);
			CardCount = MemoryGameCardCount.Sixteen;
			CardBackgroundImageUriString = "http://www.hdwallpapers.in/wallpapers/red_in_abstract-1280x800.jpg";
			OnNewGame();
		}

		void OnNewGame()
		{
			isCreatingNewGame = true;
			FoundCardCount = 0;
			MoveCount = 0;
			firstSelectedCard = null;
			secondSelectedCard = null;
			var cardCountValue = GetCardCountValue();
			var halfCardCountValue = cardCountValue / 2;

			var cards = new MemoryCardViewModel[cardCountValue];

			var cardIndexes = (from index in GetRange(cardCountValue)
							   orderby randomGenerator.NextDouble()
							   select index).ToArray();

			var currentCardIndex = 0;

			var memoryCards = (from Uri cardImageUri in defaultCardUriArray
							   select new MemoryCard()
							   {
								   ImageUri = cardImageUri,
							   }).ToArray();


			for (int i = 0; i < halfCardCountValue; i++)
			{
				var firstCardViewModel = new MemoryCardViewModel(this);
				var secondCardViewModel = new MemoryCardViewModel(this);
				firstCardViewModel.Initialize(memoryCards[i], CardBackgroundImageUriString);
				secondCardViewModel.Initialize(memoryCards[i], CardBackgroundImageUriString);

				var firstCardIndex = cardIndexes[currentCardIndex];
				currentCardIndex++;
				var secondCardIndex = cardIndexes[currentCardIndex];
				currentCardIndex++;

				cards[firstCardIndex] = firstCardViewModel;
				cards[secondCardIndex] = secondCardViewModel;
			}

			CurrentGameCards = cards.ToList();

			isCreatingNewGame = false;
		}

		bool CanCreateNewGame()
		{
			return !isCreatingNewGame;
		}

		private IEnumerable<int> GetRange(int exclusiveUpperBound)
		{
			for (int i = 0; i < exclusiveUpperBound; i++)
			{
				yield return i;
			}
		}

		private int GetCardCountValue()
		{
			return (int)CardCount;
		}

	}
}
