#include "InMobiEvent.h"


namespace PhoneDirect3DXamlAppComponent
{


	InMobiEvent::InMobiEvent( Object^ sender, CompletedEventArgs^ args, Windows::Foundation::EventHandler<CompletedEventArgs^>^ handler ):
		m_sender(sender),
		m_args(args),
		m_handler(handler)
	{

	}

	void InMobiEvent::execute( Cocos2dRenderer ^ renderer )
	{
		m_handler->Invoke(m_sender, m_args);
	}

}