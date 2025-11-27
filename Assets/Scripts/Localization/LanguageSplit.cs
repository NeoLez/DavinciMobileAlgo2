using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSplit
{
    public Dictionary<Languages, Dictionary<string, string>> LoadCSV(string sheet, string source) 
    {
        Dictionary<Languages, Dictionary<string, string>> codex = new();

        var langColumn = new Dictionary<int, Languages>();

        var idColumn = 0;

        var lines = sheet.Split('\n', System.StringSplitOptions.RemoveEmptyEntries);


        bool firstLine = true;

        foreach ( var line in lines )
        {
            var cells = line.Split(',');

            if (firstLine)
            {
                firstLine = false;

                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i].Contains("ID"))
                    {
                        idColumn = i;
                        continue;
                    }

                    try
                    {
                        langColumn[i] = (Languages)Enum.Parse(typeof(Languages), cells[i]);
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning($"SOURCE: {source}");
                        Debug.LogWarning($"{e.Message}");
                        continue;
                    }

                    if (!codex.ContainsKey(langColumn[i]))
                    {
                        codex[langColumn[i]] = new Dictionary<string, string>();
                    }
                }
            }

            for (int i = 0; i < cells.Length; i++)
            {
                //Ignoramos la columna de IDs
                if (i == idColumn) continue;

                //Ignoramos si tenemos una columna de mas
                if (!langColumn.ContainsKey(i)) continue;

                var lang = langColumn[i];

                var id = cells[idColumn];

                var textValue = cells[i];

                codex[lang][id] = textValue;
            }
        }

        return codex;
    }
}