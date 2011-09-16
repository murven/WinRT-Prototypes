using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ASD.Infrastructure
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			var threadSafePropertyChanged = PropertyChanged;
			if (threadSafePropertyChanged != null)
			{
				threadSafePropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		public ViewModelBase()
		{
			BeforeResolveDependencies();
			ResolveDependencies();
			AfterResolveDependencies();
			Initialize();
		}

		protected virtual void OnBeforeResolveDependencies() { }
		protected virtual void OnResolveDependencies() { }
		protected virtual void OnAfterResolveDependencies() { }
		protected virtual void OnInitialize() { }

		void BeforeResolveDependencies()
		{
			OnBeforeResolveDependencies();
		}

		void ResolveDependencies()
		{
			OnResolveDependencies();
		}

		void AfterResolveDependencies()
		{
			OnAfterResolveDependencies();
		}

		void Initialize() 
		{
			OnInitialize();
		}

	}
}
