using System.Drawing;
using System.Drawing.Imaging;

namespace TelegramBot.Common.Image
{
    public interface IImageHandler
    {
        void Save(Bitmap image, int maxWidth, int maxHeight, int quality, string filePath);
    }
}