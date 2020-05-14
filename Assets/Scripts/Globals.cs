using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Globals : MonoBehaviour {
    public static Game game;
    public static System.Random random;
    public static void LoadData()
    {
        random = new System.Random();
        game = new Game();
        game.Load();
    }
}

[System.Serializable]
public class Game
{
    public uint score { get; set; }
    public string path { get; set; }
    private bool isSaving = false;
    public Game()
    {
        score = 0;
        path = Application.persistentDataPath + "/game.data";
        Debug.Log(path);
    }

    public void Save()
    {
        if (!isSaving)
        {
            isSaving = true;
            using (StreamWriter sw = new StreamWriter(File.Open(path, FileMode.OpenOrCreate)))
                sw.WriteLine(Utilities.FromByteArrayToString(Utilities.ObjectToByteArray(this)));
            isSaving = false;
        }
        Debug.Log("Game Saved");
    }
    public void Load()
    {
        if (!File.Exists(path))
            Save();
        else
            using (StreamReader sr = new StreamReader(File.Open(path, FileMode.OpenOrCreate)))
            {
                var pom = (Game)Utilities.ByteArrayToObject(Utilities.FromStringBytearray(sr.ReadLine()));
                score = pom.score;
            }
    }
}
public class Utilities
{
    public static byte[] ObjectToByteArray(object obj)
    {
        if (obj == null)
            return null;

        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream ms = new MemoryStream();
        bf.Serialize(ms, obj);

        return ms.ToArray();
    }
    public static object ByteArrayToObject(byte[] arrBytes)
    {
        MemoryStream memStream = new MemoryStream();
        BinaryFormatter binForm = new BinaryFormatter();
        memStream.Write(arrBytes, 0, arrBytes.Length);
        memStream.Seek(0, SeekOrigin.Begin);
        object obj = (object)binForm.Deserialize(memStream);

        return obj;
    }
    public static string FromByteArrayToString(byte[] obj)
    {
        string result = "";
        foreach (byte b in obj)
        {
            result += b.ToString("X");
            result += " ";
        }
        return result;
    }
    public static byte[] FromStringBytearray(string value)
    {
        List<byte> result = new List<byte>();
        string word = "";
        foreach (char c in value)
        {
            if (c.Equals(' '))
            {
                result.Add(byte.Parse(word, System.Globalization.NumberStyles.HexNumber));
                word = "";
                continue;
            }
            else
            {
                word += c;
            }
        }
        return result.ToArray();
    }
}
