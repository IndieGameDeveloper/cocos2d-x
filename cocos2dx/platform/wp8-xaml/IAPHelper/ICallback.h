namespace PhoneDirect3DXamlAppComponent
{
	namespace IAPHelper
	{
		// An asynchronous completed event argument
		public ref class CompletedEventArgs sealed
		{
		public:
			CompletedEventArgs(bool ReturnValue, int ErrorCode, Platform::String^ ErrorMessage, Platform::String^ strItems)
			{
				this->ReturnValue = ReturnValue;
				this->ErrorCode = ErrorCode;
				this->ErrorMessage = ErrorMessage;
                this->purchasedItems = strItems;
			}

			property bool ReturnValue;
			property int ErrorCode;
			property Platform::String^ ErrorMessage;
            property Platform::String^ purchasedItems;
		};
		// A callback interface for C# code to implement.
		public interface class ICallback
		{
			// completed event.
			event Windows::Foundation::EventHandler<CompletedEventArgs^>^ OnItemsRefreshed;

			// switch bottombar in mainpage to add or remove panel
			void GetIAP();

			//... other event and other function
		};
	}
}