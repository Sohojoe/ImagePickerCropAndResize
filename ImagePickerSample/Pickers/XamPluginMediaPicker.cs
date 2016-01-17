using System;
using System.Threading.Tasks;
using System.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace ImagePickerSample
{
	public static class XamPluginMediaPicker
	{
		static XamPluginMediaPicker ()
		{
		}

		public static async Task<MediaFile> PickImageAsync()
		{
			if (!CrossMedia.Current.IsPickPhotoSupported)
				return null;
			var mediaFile = await CrossMedia.Current.PickPhotoAsync ();
			return mediaFile;
		}
	}
}

