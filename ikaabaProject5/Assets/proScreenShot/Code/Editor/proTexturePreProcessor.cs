using UnityEngine;
using UnityEditor;

public class proTexturePreProcessor : AssetPostprocessor
{
	void OnPreprocessTexture()
	{
		if(assetPath.Contains(proCore.latestScreenshotPath)&&proCore.latestScreenshotPath!="")
		{
			TextureImporter importer = assetImporter as TextureImporter;
			importer.textureType  = TextureImporterType.Default;
			importer.textureFormat = TextureImporterFormat.AutomaticTruecolor;
			importer.isReadable = true;
			importer.anisoLevel = 16;
			importer.maxTextureSize = 4096;
			importer.mipmapEnabled = false;
			importer.filterMode = FilterMode.Bilinear;
			importer.npotScale = TextureImporterNPOTScale.None;

			Object asset = AssetDatabase.LoadAssetAtPath(importer.assetPath, typeof(Texture2D));
			
			if(asset)
			{
				EditorUtility.SetDirty(asset);
			}
			else
			{
				importer.textureType  = TextureImporterType.Default ;   
			}
		}
	}
}