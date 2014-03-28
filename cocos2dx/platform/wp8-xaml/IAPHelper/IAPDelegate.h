#pragma once

#include "ICallback.h"

namespace PhoneDirect3DXamlAppComponent
{
	namespace IAPHelper
	{
		public ref class IAPDelegate sealed
		{
		public:
			IAPDelegate(void);

			void SetCallback(ICallback^ callback);

			property static ICallback^ GlobalCallback;
		};
	}

}