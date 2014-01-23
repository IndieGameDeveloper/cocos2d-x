#pragma once

#include "ICallback.h"
#include "InputEvent.h"
using namespace Platform;
using namespace PhoneDirect3DXamlAppComponent::InMobiHelper;

namespace PhoneDirect3DXamlAppComponent
{
	class InMobiEvent : public InputEvent
	{
	public:
		InMobiEvent(Object^ sender, CompletedEventArgs^ args, Windows::Foundation::EventHandler<CompletedEventArgs^>^ handler);
		virtual void execute(Cocos2dRenderer ^ renderer);

	private:
		Object^ m_sender;
		CompletedEventArgs^ m_args;
		Windows::Foundation::EventHandler<CompletedEventArgs^>^ m_handler;
	};
}

