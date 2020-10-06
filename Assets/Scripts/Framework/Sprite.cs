using UnityEngine;

//Declared in the default namespace outside of the class scope so that other //files can access this structure too.
public struct TextureData
{
    public string name;
    public float x;
    public float y;
    public float w;
    public float h;
}

//Now our actual sprite class.
public class Sprite : MonoBehaviour
{
    public string imageName;
    private TextureData data;

    void Start()
    {
        //get the data for this image from the texture atlas.
        data = TextureManager.Instance.GetTexture(imageName);

        //create our new uv coordinates from the texture atlas data
        Vector2[] uvs = new Vector2[4];
        uvs[0].x = data.x;
        uvs[0].y = 1 - (data.y + data.h);
        uvs[1].x = (data.x + data.w);
        uvs[1].y = 1 - (data.y + data.h);
        uvs[2].x = data.x;
        uvs[2].y = 1 - data.y;
        uvs[3].x = data.x + data.w;
        uvs[3].y = 1 - data.y;

        //get the mesh and change its uv coordinates.
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.uv = uvs;
    }
}