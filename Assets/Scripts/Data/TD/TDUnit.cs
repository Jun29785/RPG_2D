using System;

public class TDUnit : TableBase
{
    public int Key;
    public string Name;

    public override void SetCSVData(string csvData)
    {
        base.SetCSVData(csvData);
        string[] data = csvData.Split(",");
        Key = Int32.Parse(data[0]);
        Name = data[1];
    }
}