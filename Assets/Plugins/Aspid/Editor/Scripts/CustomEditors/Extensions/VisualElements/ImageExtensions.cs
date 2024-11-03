using UnityEngine;
using UnityEngine.UIElements;

namespace Aspid.CustomEditors.Extensions.VisualElements
{
    public static class ImageExtensions
    {
        public static T SetImage<T>(this T image, Texture2D texture)
            where T : Image
        {
            image.image = texture;
            return image;
        }

        public static T SetImageFromResource<T>(this T image, string path)
            where T : Image
        {
            return image.SetImage(Resources.Load<Texture2D>(path));
        }
    }
}