using System.IO;
using System.Xml;
using UnityEngine;

public class TextureManager : MonoBehaviour
{
    private static TextureManager instance = null;
    public static TextureManager Instance { get { return instance; } }

    public Texture2D textureFile;   // the texture atlas file
    public TextAsset xmlAssetFile;  // the XML document containing clip information

    private TextureData[] atlasClips; // clipping information for each AI ship

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            string textData = xmlAssetFile.text;
            ParseXML(textData);
        }
    }

    private void ParseXML(string textData)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(textData));
        string xmlPath = "//atlas/image";
        XmlNodeList nodeList = xmlDoc.SelectNodes(xmlPath);

        atlasClips = new TextureData[nodeList.Count];

        int index = 0;
        foreach(XmlNode n in nodeList)
        {
            atlasClips[index++] = ParseNode(n);
        }
    }

    private TextureData ParseNode(XmlNode node)
    {
        XmlNode nameNode = node.FirstChild;
        XmlNode xNode = nameNode.NextSibling;
        XmlNode yNode = xNode.NextSibling;
        XmlNode wNode = yNode.NextSibling;
        XmlNode hNode = wNode.NextSibling;

        TextureData texData;
        texData.name = nameNode.InnerXml;
        texData.x = int.Parse(xNode.InnerXml);
        texData.y = int.Parse(yNode.InnerXml);
        texData.w = int.Parse(wNode.InnerXml);
        texData.h = int.Parse(hNode.InnerXml);

        // convert to UV coordinate space
        texData.x /= textureFile.width;
        texData.y /= textureFile.height;
        texData.w /= textureFile.width;
        texData.h /= textureFile.height;

        return texData;
    }

    public TextureData GetTexture(string name)
    {
        foreach (TextureData td in atlasClips)
        {
            if (td.name == name)
            {
                return td;
            }
        }
        return new TextureData();
    }
}
