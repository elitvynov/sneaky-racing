namespace sneakyRacing
{
	using UnityEngine;
	using UnityEngine.UI;

	public class ConfirmationPanel : MonoBehaviour
	{
		public enum ActionType
		{
			Yes,
			No,
			Cancel,
		};

		public System.Action<ActionType> onCloseEvent;

		public void yesButtonClick()
		{
			close();

			if (onCloseEvent != null)
				onCloseEvent(ActionType.Yes);

			//SoundPlayer.instance.play("ButtonClick");
		}

		public void noButtonClick()
		{
			close();

			if (onCloseEvent != null)
				onCloseEvent(ActionType.No);

			//SoundPlayer.instance.play("ButtonClick");
		}

		public void cancelButtonClick()
		{
			close();

			if (onCloseEvent != null)
				onCloseEvent(ActionType.Cancel);

			//SoundPlayer.instance.play("ButtonClick");
		}

		public bool isOpened
		{
			get
			{
				return gameObject.activeSelf;
			}
		}

		virtual public void close()
		{
			gameObject.SetActive(false);
		}

		virtual public void open()
		{
			gameObject.SetActive(true);
		}
	}
}