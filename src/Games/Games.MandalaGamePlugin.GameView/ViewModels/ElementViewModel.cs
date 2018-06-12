using Games.MandalaGamePlugin.Data;

namespace Games.MandalaGamePlugin.GameView.ViewModels
{
    public class ElementViewModel
    {
        protected ElementViewModel(IMandalaElement element)
        {
            this.MandalaElement = element;
        }

        public IMandalaElement MandalaElement { get; }
    }
}