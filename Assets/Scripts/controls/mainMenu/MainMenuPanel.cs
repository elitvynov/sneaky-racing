namespace sneakyRacing
{
	using UnityEngine;

	public class MainMenuPanel : Panel 
	{

		public void aboutButtonClick()
		{
			if (_inputInvalidator.invalidateEvent() == false)
				return;

			(Level.instance.menu as MainMenu).mainMenuPanel.hide();
			(Level.instance.menu as MainMenu).aboutPanel.show();
		}

		public void startButtonClick()
		{
			if (_inputInvalidator.invalidateEvent() == false)
				return;

			(Level.instance.menu as MainMenu).mainMenuPanel.hide();
			(Level.instance.menu as MainMenu).levelPanel.show();
		}
	}
}