namespace sneakyRacing
{
	using UnityEngine;

	public class AboutPanel : Panel 
	{

		public void closeButtonClick()
		{
			if (_inputInvalidator.invalidateEvent() == false)
				return;

			(Level.instance.menu as MainMenu).mainMenuPanel.show();
			hide();
		}
	}
}