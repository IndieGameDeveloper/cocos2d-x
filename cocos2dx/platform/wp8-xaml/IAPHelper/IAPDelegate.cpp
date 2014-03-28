#include "IAPDelegate.h"

namespace PhoneDirect3DXamlAppComponent
{
	namespace IAPHelper
	{


		IAPDelegate::IAPDelegate( void )
		{

		}

		void IAPDelegate::SetCallback( ICallback^ callback )
		{
			GlobalCallback = callback;
		}

	}
}
