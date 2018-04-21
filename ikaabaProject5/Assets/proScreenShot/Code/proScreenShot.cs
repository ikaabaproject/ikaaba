using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class proScreenShot : MonoBehaviour {

	[SerializeField]
	private string directory = "";
	[SerializeField]
	public string filePrimaryName = "ScreenShot";
	[SerializeField]
	public string fileExtension = ".jpg";
	[SerializeField]
	public bool addDateToFileName = true;
	[SerializeField]
	public bool addRandomNrToFileName = true;
	[SerializeField]
	public bool useScreenResolution = true;
	[SerializeField]
	public bool RenderUI = true;
	[SerializeField]
	public AudioClip screenshotSound;
	[SerializeField]
	public Vector2 shotResolution = new Vector2(512,512);
	[SerializeField]
	public Vector2 screenOffset = new Vector2(0,0);
	[SerializeField]
	public List<Texture2D> blendTextures = new List<Texture2D>();
	[SerializeField]
	public List<Vector2> blendTexturesPosition = new List<Vector2>();
	[SerializeField]
	public List<GameObject> ignoreObjectsList = new List<GameObject>();
	[HideInInspector]
	[SerializeField]
	public string[] textureFormats;
	[HideInInspector]
	[SerializeField]
	public int textureFormatsIndex = 0;
	[HideInInspector]
	[SerializeField]
	public string[] encodeFormats;
	[HideInInspector]
	[SerializeField]
	public int encodeFormatsIndex = 0;
	[HideInInspector]
	[SerializeField]
	public string[] blendFormats;
	[HideInInspector]
	[SerializeField]
	public int blendFormatsIndex = 0;
	private string forceCustomPath = "";

	public Vector2 _screenOffset
	{
		get { return screenOffset; }
		set {

			if (screenOffset==value) return;

			screenOffset = value;
		}
	}

	public bool _IncludeUIInScreenShot
	{
		get { return RenderUI; }
		set {

			if (RenderUI==value) return;

			RenderUI = value;
		}
	}

	public AudioClip _screenshotSound
	{
		get { return screenshotSound; }
		set
		{
			if (screenshotSound==value) return;

			screenshotSound = value;
		}
	}

	public string[] _blendFormats
	{
		get { return blendFormats; }
		set
		{
			if (blendFormats==value) return;

			blendFormats = value;
		}
	}

	public int _blendFormatsIndex
	{
		get { return blendFormatsIndex; }
		set
		{
			if (blendFormatsIndex==value) return;

			blendFormatsIndex = value;
		}
	}

	public string[] _encodeFormats
	{
		get { return encodeFormats; }
		set
		{
			if (encodeFormats==value) return;

			encodeFormats = value;
		}
	}

	public int _encodeFormatsIndex
	{
		get { return encodeFormatsIndex; }
		set
		{
			if (encodeFormatsIndex==value) return;

			encodeFormatsIndex = value;
		}
	}

	public string[] _textureFormats
	{
		get { return textureFormats; }
		set
		{
			if (textureFormats==value) return;

			textureFormats = value;
		}
	}

	public int _textureFormatsIndex
	{
		get { return textureFormatsIndex; }
		set
		{
			if (textureFormatsIndex==value) return;

			textureFormatsIndex = value;
		}
	}

	public bool _useScreenResolution
	{
		get { return useScreenResolution; }
		set
		{
			if (useScreenResolution==value) return;

			useScreenResolution = value;
		}
	}

	public Vector2 _shotResolution
	{
		get { return shotResolution; }
		set
		{
			if (shotResolution==value) return;

			shotResolution = value;
		}
	}

	public List<Texture2D> _blendTextures
	{
		get { return blendTextures; }
		set
		{
			if (blendTextures==value) return;

			blendTextures = value;
		}
	}

	public List<GameObject> _ignoreObjectsList
	{
		get { return ignoreObjectsList; }
		set
		{
			if (ignoreObjectsList==value) return;

			ignoreObjectsList = value;
		}
	}

	public List<Vector2> _blendTexturesPosition
	{
		get { return blendTexturesPosition; }
		set
		{
			if (blendTexturesPosition==value) return;

			blendTexturesPosition = value;
		}
	}

	public string _directory
	{
		get { return directory; }
		set
		{
			if (directory==value) return;

			directory = value;
		}
	}

	public string _filePrimaryName
	{
		get { return filePrimaryName; }
		set
		{
			if (filePrimaryName==value) return;

			filePrimaryName = value;
		}
	}

	public string _fileExtension
	{
		get { return fileExtension; }
        set
        {
            if (fileExtension == value) return;
 
            fileExtension = value;
        }
	}

	public bool _addDateFileName
    {
        get { return addDateToFileName; }
        set
        {
            if (addDateToFileName == value) return;
 
            addDateToFileName = value;
        }
    }

	public bool _addRandomNumberToName
    {
        get { return addRandomNrToFileName; }
        set
        {
            if (addRandomNrToFileName == value) return;
 
            addRandomNrToFileName = value;
        }
    }

    private void Awake()
    {
    	string[] currentSaved = GetPathListOfSavedScreenShots();
    	string newList = "";

    	for(int i=0;i<currentSaved.Length;i++)
    	{
    		if(System.IO.File.Exists(currentSaved[i]))
    		{
    			newList += currentSaved[i]+"&";
    		}
    	}

    	PlayerPrefs.SetString("proScreenShotPictures",newList);

    	#if UNITY_EDITOR
    	if(blendTextures.Count!=blendTexturesPosition.Count&&blendTextures.Count>blendTexturesPosition.Count)
    	{
    		Debug.Log("[Warning] Blend Textures does not have position set. BlendTexturesPosition List needs to have same amount of elements as BlendTextures. All (non runtime) Elements will have Vector2.zero position.");
    		
    		blendTexturesPosition.Clear();

    		for(int i=0;i<blendTextures.Count;i++)
    		{
    			blendTexturesPosition.Add(new Vector2(0,0));
    		}
    	}
    	#endif
    }

	public void MakeScreenShot()
	{
		proCore _proCore = Camera.main.gameObject.AddComponent<proCore>();

		if(useScreenResolution)
		{
			shotResolution = new Vector2(Screen.width,Screen.height);
		}
		
		_proCore.MakeScreenShot(this,filePrimaryName,directory,fileExtension,blendTextures,blendTexturesPosition,addDateToFileName,addRandomNrToFileName,ignoreObjectsList,shotResolution,textureFormats[textureFormatsIndex],encodeFormats[encodeFormatsIndex],blendFormats[blendFormatsIndex],screenshotSound,forceCustomPath,RenderUI,screenOffset);	
	}

	public void SetDirectory(string path)
	{
		directory = path;
	}

	public void SetName(string name)
	{
		if(name.Equals(""))
		{
			name = "ScreenShot";
		}

		filePrimaryName = name;
	}

	public void SetExtension(string ex)
	{
		if(ex.Equals(""))
		{
			ex = ".jpg";
		}

		fileExtension = ex;
	}

	public void AddBlendTexture(Vector2 position, Texture2D texture)
	{
		blendTextures.Add(texture);
		blendTexturesPosition.Add(position);
	}

	public void AddIgnoreObject(GameObject obj)
	{
		ignoreObjectsList.Add(obj);
	}

	public void CleanBlendTextures()
	{
		blendTextures.Clear();
	}

	public void CleanIgnoreObjectList()
	{
		ignoreObjectsList.Clear();
	}

	public void ClearSavedScreenShotList()
	{
		PlayerPrefs.DeleteKey("proScreenShotPictures");
	}

	public void DeleteScreenShot(string path)
	{
		if(System.IO.File.Exists(path))
		{
			System.IO.File.Delete(path);
		}
	}

	public void DeleteAllScreenShots()
	{
		string[] list = GetPathListOfSavedScreenShots();

		for(int i=0;i<list.Length;i++)
		{
			if(!list[i].Equals(""))
			{
				if(System.IO.File.Exists(list[i]))
				{
					System.IO.File.Delete(list[i]);
				}
			}
		}


		ClearSavedScreenShotList();
		
		#if UNITY_EDITOR
		System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(Application.dataPath+"/MyScreenShots");

		foreach (FileInfo file in downloadedMessageInfo.GetFiles())
		{
		    file.Delete(); 
		}
		
		foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
		{
		    dir.Delete(true); 
		}

		UnityEditor.AssetDatabase.SaveAssets();
		UnityEditor.AssetDatabase.Refresh();
		#endif
	}

	public string GetPathOfLastScreenShot()
	{
		string[] all = GetPathListOfSavedScreenShots();

		if(all.Length>=1)
		{
			return all[all.Length-1];
		}
		else
		{
			return "";
		}
	}

	public string[] GetPathListOfSavedScreenShots()
	{
		string all = PlayerPrefs.GetString("proScreenShotPictures");
		string[] parts = all.Split("&"[0]);

		List<string> parts2 = new List<string>();

		for(int i=0;i<parts.Length;i++)
		{
			if(parts[i]!="")
			{
				if(System.IO.File.Exists(parts[i]))
				{
					parts2.Add(parts[i]);
				}
			}
		}

		string[] finalparts = new string[parts2.Count];

		for(int i=0;i<parts2.Count;i++)
		{
			finalparts[i] = parts2[i];
		}

		return finalparts;
	}

	public void SetShotResolution(int x, int y)
	{
		shotResolution = new Vector2(x,y);
	}

	public void SetBlendFormat(string format)
	{
		bool found = false;

		for(int i=0;i<blendFormats.Length;i++)
		{
			if(blendFormats[i].Equals(format))
			{
				found = true;
				blendFormatsIndex = i;
			}
		}

		if(!found)
		{
			string availableTextureFormats = "";

			for(int i=0;i<blendFormats.Length;i++)
			{
				availableTextureFormats += blendFormats[i]+" ";
			}

			Debug.Log("Error: Blend format is not found. Available blend formats: "+availableTextureFormats);
		}
	}

	public void SetEncodeFormat(string format)
	{
		bool found = false;

		for(int i=0;i<encodeFormats.Length;i++)
		{
			if(encodeFormats[i].Equals(format))
			{
				found = true;
				encodeFormatsIndex = i;
			}
		}

		if(!found)
		{
			string availableTextureFormats = "";

			for(int i=0;i<encodeFormats.Length;i++)
			{
				availableTextureFormats += encodeFormats[i]+" ";
			}

			Debug.Log("Error: Encode format is not found. Available Encode formats: "+availableTextureFormats);
		}
	}

	public void SetTextureFormat(string format)
	{
		bool found = false;

		for(int i=0;i<textureFormats.Length;i++)
		{
			if(textureFormats[i].Equals(format))
			{
				found = true;
				textureFormatsIndex = i;
			}
		}

		if(!found)
		{
			string availableTextureFormats = "";

			for(int i=0;i<textureFormats.Length;i++)
			{
				availableTextureFormats += textureFormats[i]+" ";
			}

			Debug.Log("Error: Texture format is not found. Available texture formats: "+availableTextureFormats);
		}
	}

	public Texture2D GetTextureFromPath(string path)
	{
		if(System.IO.File.Exists(path))
		{
			byte[] bytes = System.IO.File.ReadAllBytes(path);
			Texture2D tex = new Texture2D(2,2);
			tex.LoadImage(bytes);

			if(tex)
			{
				return tex;
			}
			else
			{
				return null;
			}
		}
		else
		{
			return null;
		}
	}

	public void PlaySound()
	{
		StartCoroutine(DestroyIfNotPlaying());
	}

	public IEnumerator DestroyIfNotPlaying()
	{
		AudioSource sc = this.gameObject.AddComponent<AudioSource>();
		sc.clip = screenshotSound;
		sc.loop = false;
		sc.Play();

		while(sc.isPlaying)
		{
			yield return new WaitForSeconds(0.1f);
		}

		#if !UNITY_EDITOR
		Destroy(sc);
		#endif

		#if UNITY_EDITOR
		DestroyImmediate(sc);
		#endif

		yield return 0;
	}

	public void IsRenderUI(bool isRender)
	{
		RenderUI = isRender;
	}

	public void DeleteLastScreenShot()
	{
		string last = GetPathOfLastScreenShot();

		if(System.IO.File.Exists(last))
		{
			System.IO.File.Delete(last);

			string all = PlayerPrefs.GetString("proScreenShotPictures");
			all = all.Replace(last+":","");
			PlayerPrefs.SetString("proScreenShotPictures",all);
		}

		#if UNITY_EDITOR
		string fullpath = Application.dataPath+"/MyScreenShots/"+proCore.latestScreenshotPath;

		if(forceCustomPath!="")
		{
			fullpath = forceCustomPath+"/"+proCore.latestScreenshotPath;
		}

		if(System.IO.File.Exists(fullpath))
		{
			System.IO.File.Delete(fullpath);
		}

		UnityEditor.AssetDatabase.SaveAssets();
		UnityEditor.AssetDatabase.Refresh();
		#endif
	}

	public void ForceFullCustomPath(string path)
	{
		forceCustomPath = path;
	}
}