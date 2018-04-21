using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif

public class proCore : MonoBehaviour {

	public static string latestScreenshotPath = "";
	public static Texture2D lastScreenShotTexture = null;
	public RenderTexture RT;
	public Texture2D capturedTexture;
	private bool makeScreenShot = false;
	private string scrName;
	private string scrExtension;
	private string scrPath;
	private List<Texture2D> scrBlendTextures = new List<Texture2D>();
	private List<Vector2> scrBlendTexturesPosition = new List<Vector2>();
	private List<GameObject> ignoreObjectList = new List<GameObject>();
	private string blendFormat;
	private bool isAddDate = true;
	private bool isAddRandom = true;
	private proScreenShot _reference;
	private Vector2 shotResolution;
	private string textureFormat;
	private string encodeFormat;
	private AudioClip screenshotSound;
	private string forceCustomPath;
	private string _givenDirectory;
	private bool isPrintUI;
	private Vector2 offsetRect = Vector2.zero;

	//ignoreobjs
	private List<MeshRenderer> ignoreMRS = new List<MeshRenderer>();
	private List<SkinnedMeshRenderer> ignoreSMRS = new List<SkinnedMeshRenderer>();
	private List<SpriteRenderer> ignoreSRS = new List<SpriteRenderer>();

	private IEnumerator OnPostRender()
	{
		if(isPrintUI)
		{
			yield return new WaitForEndOfFrame();
		}

		if(makeScreenShot)
		{
			if(screenshotSound&&Application.isPlaying)
			{
				_reference.PlaySound();
			}

			RT = new RenderTexture((int)shotResolution.x,(int)shotResolution.y,32);
			Camera.main.targetTexture = RT;

			switch(textureFormat)
			{
				case "ARGB32":
					capturedTexture = new Texture2D(Screen.width-(int)offsetRect.x,Screen.height-(int)offsetRect.y,TextureFormat.ARGB32,false);
					RT.format = RenderTextureFormat.ARGB32;
					break;
				case "RGB24":
					capturedTexture = new Texture2D(Screen.width-(int)offsetRect.x,Screen.height-(int)offsetRect.y,TextureFormat.RGB24,false);
					RT.format = RenderTextureFormat.ARGB32;
					break;
				case "RGB16":
					capturedTexture = new Texture2D(Screen.width-(int)offsetRect.x,Screen.height-(int)offsetRect.y,TextureFormat.RGB565,false);
					RT.format = RenderTextureFormat.RGB565;
					break;
				default: 
					capturedTexture = new Texture2D(Screen.width-(int)offsetRect.x,Screen.height-(int)offsetRect.y,TextureFormat.ARGB32,false);
					RT.format = RenderTextureFormat.ARGB32;
					break;
			}
			
			capturedTexture.ReadPixels(new Rect((int)offsetRect.x,(int)offsetRect.x,Screen.width,Screen.height),0,0);

			if(!_reference.useScreenResolution)
			{			
				ResizeTexture(capturedTexture,(int)shotResolution.x,(int)shotResolution.y);
			}

			Camera.main.targetTexture = null;

			for(int i=0;i<scrBlendTextures.Count;i++)
			{
				Texture2D tex = scrBlendTextures[i];
				int pX = (int) scrBlendTexturesPosition[i].x;
				int pY = (int) scrBlendTexturesPosition[i].y;

				if(blendFormat.Equals("Simple"))
				{
					Color[] cls = tex.GetPixels(0,0,tex.width,tex.height);
					capturedTexture.SetPixels(pX,pY,tex.width,tex.height,cls);
				}
				else
				{
					for(int x=0;x<tex.width;x++)
					{
						for(int y=0;y<tex.height;y++)
						{
							Color c = tex.GetPixel(x,y);

							if(c.a==0) continue;

							Color c2 = capturedTexture.GetPixel(pX+x,pY+y);
							Color c3 = Color.Lerp(c, c2, c2.a);
							capturedTexture.SetPixel(pX+x,pY+y,c3);
						}
					}
				}
			}

			capturedTexture.Apply();

			//#if UNITY_EDITOR
			proCore.latestScreenshotPath = scrName+scrExtension;
			//#endif

			byte[] bytes;

			if(encodeFormat.Equals("JPG"))
			{
				bytes = capturedTexture.EncodeToJPG();
			}
			else if(encodeFormat.Equals("PNG"))
			{
				bytes = capturedTexture.EncodeToPNG();
			}
			else
			{
				bytes = capturedTexture.EncodeToJPG();
			}
			#if !UNITY_WEBPLAYER
			File.WriteAllBytes(scrPath+scrName+scrExtension, bytes);
			#endif
			makeScreenShot = false;

			#if UNITY_EDITOR && !UNITY_WEBPLAYER

			string lPathDir = Application.dataPath+"/MyScreenShots";

			if(!Directory.Exists(lPathDir))
			{
				Directory.CreateDirectory(lPathDir);
			}

			string lPath = "Assets/MyScreenShots/"+_givenDirectory+scrName+scrExtension;
			System.IO.File.WriteAllBytes(lPath, bytes);
			
			UnityEditor.AssetDatabase.SaveAssets();
			UnityEditor.AssetDatabase.Refresh();

			if(_givenDirectory!="")
			{
				_givenDirectory+="/";
			}

			lastScreenShotTexture = (Texture2D) UnityEditor.AssetDatabase.LoadAssetAtPath(lPath,typeof(Texture2D));
			Debug.Log("ScreenShot saved to: "+scrPath+scrName+scrExtension+" (Editor Only Message)");
			#endif

			string allScreenShots = PlayerPrefs.GetString("proScreenShotPictures");

			if(allScreenShots.Equals(""))
			{
				allScreenShots = scrPath+scrName+scrExtension+"&";
			}
			else
			{
				allScreenShots += scrPath+scrName+scrExtension+"&";
			}

			PlayerPrefs.SetString("proScreenShotPictures",allScreenShots);

			MakeIgnoreObjectsVisible();
			#if UNITY_EDITOR
			DestroyImmediate(this);
			#endif

			#if !UNITY_EDITOR
			Destroy(this);
			#endif
		}

		yield return 0;
	}

	public void MakeScreenShot(proScreenShot reference, string name, string path, string extension, List<Texture2D> blendTextures, List<Vector2> blendTexturesPosition, bool addDateToScreenShotName, bool addRandomNumber, List<GameObject> _ignoreObjectList, Vector2 _shotResolution, string _textureFormat, string _encodeFormat, string _blendFormat, AudioClip _screenshotSound, string _forceCustomPath, bool renderUI, Vector2 _screenOffset)
	{
		isPrintUI = renderUI;
		_reference = reference;
		_givenDirectory = path;
		forceCustomPath = _forceCustomPath;
		screenshotSound = _screenshotSound;
		blendFormat = _blendFormat;
		encodeFormat = _encodeFormat;
		textureFormat = _textureFormat;
		shotResolution = _shotResolution;
		ignoreObjectList = _ignoreObjectList;
		isAddRandom = addRandomNumber;
		isAddDate = addDateToScreenShotName;
		scrBlendTextures = blendTextures;
		scrBlendTexturesPosition = blendTexturesPosition;
		offsetRect = _screenOffset;

		if(extension.Equals(""))
		{
			scrExtension = ".jpg";
		}
		else
		{
			scrExtension = extension;
		}

		if(name.Equals(""))
		{
			name = "ScreenShot_01";
		}

		if(isAddDate)
		{
			string date = System.DateTime.Now.ToString();
			date = "_"+date.Replace("/","-");
			date = date.Replace(" ","_");
			date = date.Replace("\\","_");
			date = date.Replace(":","-");
			name += date;
		}

		if(isAddRandom)
		{
			name += "_["+Random.Range(0,1000).ToString()+"]";
		}

		name = name.Replace("&","_");

		if(forceCustomPath.Equals(""))
		{
			scrPath =  Application.persistentDataPath+"/"+path+"/";
#if UNITY_EDITOR
			scrPath = Application.streamingAssetsPath+"/";
#endif
		}
		else
		{
			scrPath = forceCustomPath;
		}

		if(!System.IO.Directory.Exists(scrPath))
		{
			System.IO.Directory.CreateDirectory(scrPath);
		}
			
		scrName = name;

		if(scrName.Equals(""))
		{
			scrName = "ScreenShot";
		}

		MakeIgnoreObjectsInvisible();

		makeScreenShot = true;
	}

	private void MakeIgnoreObjectsInvisible()
	{
		ignoreSMRS.Clear();
		ignoreMRS.Clear();
		ignoreSRS.Clear();

		for(int i=0;i<ignoreObjectList.Count;i++)
		{
			GameObject o = ignoreObjectList[i];

			if(o)
			{
				MeshRenderer[] mrs = o.GetComponentsInChildren<MeshRenderer>();

				for(int mrsC=0;mrsC<mrs.Length;mrsC++)
				{
					ignoreMRS.Add(mrs[mrsC]);
					mrs[mrsC].enabled = false;
				}

				SkinnedMeshRenderer[] smrs = o.GetComponentsInChildren<SkinnedMeshRenderer>();

				for(int smrsC=0;smrsC<smrs.Length;smrsC++)
				{
					ignoreSMRS.Add(smrs[smrsC]);
					smrs[smrsC].enabled = false;
				}

				SpriteRenderer[] srs = o.GetComponentsInChildren<SpriteRenderer>();

				for(int srsC=0;srsC<srs.Length;srsC++)
				{
					ignoreSRS.Add(srs[srsC]);
					srs[srsC].enabled = false;
				}
			}
		}
	}

	private void MakeIgnoreObjectsVisible()
	{
		for(int i=0;i<ignoreMRS.Count;i++)
		{
			ignoreMRS[i].enabled = true;
		}

		for(int i=0;i<ignoreSMRS.Count;i++)
		{
			ignoreSMRS[i].enabled = true;
		}

		for(int i=0;i<ignoreSRS.Count;i++)
		{
			ignoreSRS[i].enabled = true;
		}
	}

	public Color32[] ResizeTexture(Texture2D texture, int width, int height)
	{
		var resizedPX = ResizeTexture(texture.GetPixels32(), texture.width, texture.height, width, height);
		texture.Resize(width, height);
		texture.SetPixels32(resizedPX);
		texture.Apply();

		return resizedPX;
	}
 
	private Color32[] ResizeTexture(IList<Color32> pixels, int oldWidth, int oldHeight, int width, int height)
	{
		Color32[] resizedPX = new Color32[(width * height)];
		int wB = (width - oldWidth) / 2;
		int hB = (height - oldHeight) / 2;

		for (int r = 0; r < height; r++)
		{
			int oldR = r - hB;

			if (oldR < 0) {
				continue;
			}

			if (oldR >= oldHeight) {
				break;
			}

			for (int c = 0; c < width; c++)
			{
				var oldC = c - wB;
				
				if (oldC < 0) {
					continue;
				}

				if (oldC >= oldWidth) {
					break;
				}

				int oldI = oldR*oldWidth + oldC;
				int i = r*width + c;
				resizedPX[i] = pixels[oldI];
			}
		}

		return resizedPX;
	}
}
