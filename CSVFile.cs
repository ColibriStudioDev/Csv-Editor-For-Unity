using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CSVFile
{
    public List<string[]> lines = new List<string[]>();
    public List<string> cells;

    public string savePath;
    public CSVFile(string path)
    {
        savePath = path;
        Parsing(File.ReadAllLines(path));
    }
    public CSVFile(TextAsset asset)
    {
        Parsing(asset.text.Split('\n'));
    }



    //INTERNAL
    private void Parsing(string[] allLines)
    {
        foreach (string line in allLines)
        {
            string[] cellsOnLine = line.Split(',');
            cells = new List<string>();
            foreach (string tempCell in cellsOnLine)
            {
                cells.Add(tempCell);

            }

            lines.Add(cells.ToArray());
        }



    }
    //PUBLIC
    public string GetRawText()
    {
        string rawtext="";

        for(int i = 0; i < lines.Count; i++)
        {
            for(int z = 0; z < lines[i].Length; z++)
            {
                rawtext += lines[i][z];
                if (z < lines[i].Length - 1) rawtext += ',';
            }

            if (i < lines.Count - 1) rawtext += '\n';
        }

        return rawtext;
    }
    public string[] GetAllCells()
    {
        List<string> temp = new List<string>();
        foreach(string[] line in lines)
        {
            foreach(string cell in line)
            {
                temp.Add(cell);
            }
        }


        return temp.ToArray();
    }
    public void AddLine()
    {
        cells = new List<string>();
        for(int i = 0; i < lines[lines.Count -1].Length; i++)
        {
            cells.Add("none");
        }
        lines.Add(cells.ToArray());

        
    }
    public void AddLine(int index)
    {
        cells = new List<string>();
        for (int i = 0; i < lines[lines.Count - 1].Length; i++)
        {
            cells.Add("none");
        }
        lines.Insert(index,cells.ToArray());


    }
    public void RemoveLine(int index)
    {
        lines.RemoveAt(index);
    }
    public void Save()
    {
        File.WriteAllText(savePath, GetRawText());
    }
    public void Save(string path)
    {
        File.WriteAllText(path, GetRawText());
    }

    public Dictionary<string,string> GetDictionary(int cellKeyIndex,int cellValueIndex)
    {
        Dictionary<string, string> temp = new Dictionary<string, string>();
        foreach(string[] line in lines)
        {
            if (cellValueIndex > line.Length - 1 || cellKeyIndex > line.Length - 1) return null;
            temp.Add(line[cellKeyIndex], line[cellValueIndex]);
        }
        return temp;
    }
    

}


