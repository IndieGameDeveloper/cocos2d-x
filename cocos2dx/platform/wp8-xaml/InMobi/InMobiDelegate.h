#pragma once

#include "ICallback.h"

namespace PhoneDirect3DXamlAppComponent
{
	namespace InMobiHelper
	{
		public ref class InMobiDelegate sealed
		{
		public:
			InMobiDelegate(void);

			void SetCallback(ICallback^ callback);

			property static ICallback^ GlobalCallback;
		};
	}

}