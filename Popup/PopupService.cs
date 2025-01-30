using System.Text.Json;

namespace QueueProgram.Popup
{
    public interface IPopupService
    {
        PopupOptions GetOptions();
    }
    
    public class PopupService: IPopupService
    {
                
        private QueueProgram.Popup.PopupOptions _options;

        public PopupService()
        {
            LoadOptions();
        }

        private void LoadOptions()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "popup.json");

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var popupData = JsonSerializer.Deserialize<QueueProgram.Popup.PopupOptions>(json);
                if (popupData == null) throw new Exception("Popup data is null");
                _options = popupData;
            }
            else
            {
                throw new FileNotFoundException("File popup.json not found.");
            }
        }

        public virtual QueueProgram.Popup.PopupOptions GetOptions()
        {
            return _options;
        }
    }
}
