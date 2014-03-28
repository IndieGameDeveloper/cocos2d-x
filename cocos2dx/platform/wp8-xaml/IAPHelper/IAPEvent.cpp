#include "IAPEvent.h"


namespace PhoneDirect3DXamlAppComponent
{


	IAPEvent::IAPEvent( Object^ sender, CompletedEventArgs^ args, Windows::Foundation::EventHandler<CompletedEventArgs^>^ handler ):
		m_sender(sender),
		m_args(args),
		m_handler(handler)
	{

	}

	void IAPEvent::execute( Cocos2dRenderer ^ renderer )
	{
		m_handler->Invoke(m_sender, m_args);
	}

}