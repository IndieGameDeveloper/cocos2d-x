#include "InMobiDelegate.h"

namespace PhoneDirect3DXamlAppComponent
{
	namespace InMobiHelper
	{


		InMobiDelegate::InMobiDelegate( void )
		{

		}

		void InMobiDelegate::SetCallback( ICallback^ callback )
		{
			GlobalCallback = callback;
		}

	}
}
