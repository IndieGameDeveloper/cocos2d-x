#include "AdControlEvent.h"


namespace PhoneDirect3DXamlAppComponent
{


	AdControlEvent::AdControlEvent( Object^ sender, CompletedEventArgs^ args, Windows::Foundation::EventHandler<CompletedEventArgs^>^ handler ):
		m_sender(sender),
		m_args(args),
		m_handler(handler)
	{

	}

	void AdControlEvent::execute( Cocos2dRenderer ^ renderer )
	{
		m_handler->Invoke(m_sender, m_args);
	}

}